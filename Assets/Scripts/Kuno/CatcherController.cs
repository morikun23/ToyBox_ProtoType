using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherController : MonoBehaviour {

	//スポーン時に必要な値
	public GameObject obj_initPos;		//キャッチャーの戻る場所
	public float spd_expansion;			//プレーヤーから伸びるスピード
	public PlayerMove scr_playerMove;	//キャッチャーとつながるオブジェクト(つまりプレーヤー)
	public Vector3 pos_end;				//このＭＨの終着点

	//その他変数
	private float num_distance = 0;		//現在のプレーヤーからの距離
	private bool flg_click;				//クリック中か？

	private bool flg_hit;				//物に当たっているかどうか
	private Vector2 pos_contact;		//当たった場所の座標
	private int cnt_live = 60;			//自動消滅までのカウント
	private Rigidbody2D com_rigidbody;	//このオブジェクトのRigidbody

	public PullBlock scr_pullBlock;

	private GameObject obj_armCollider;
	public GameObject pre_armCollider;
	LineRenderer lir_;
	GameObject obj_player;

	bool flg_stay = false;
	public bool flg_waitDestroy = false;

	//状態管理用enum
	public enum Status{
		Neutoral,
		Shot,
		HitPullBlock,
		HitStayBlock,
		HitApproachBlock,
		HitNomalBlock,
		Cancel,
		Enter,
		Break
	}
	public Status enu_status;

	public TimeManager scr_timeManager;

    private AudioSource Catcher_SE; //キャッチャーのSE

    // Use this for initialization
    void Start () {
		com_rigidbody = GetComponent<Rigidbody2D> (); 
		com_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

		gameObject.AddComponent<LineRenderer>();

		lir_ = gameObject.GetComponent<LineRenderer>();
		obj_player = GameObject.Find ("CatcherPos");
		// 線の幅
		lir_.SetWidth(0.1f, 0.1f);
		// 頂点の数
		lir_.SetVertexCount(2);

		lir_.material.color = Color.black;

		GameObject baf_obj = GameObject.Find ("TimeManager");
		scr_timeManager = baf_obj.GetComponent<TimeManager> ();

        Catcher_SE = GetComponent<AudioSource>();


    }

	// Update is called once per frame
	void Update () {
		switch (enu_status) {
		case Status.Neutoral:
			SetPositionToInit ();
			break;
		case Status.Shot:
			AddPosition ();
			break;
		case Status.HitApproachBlock:
			HitApproachBlock ();
			break;
		case Status.HitStayBlock:
			HitStayBlock ();
			break;
		case Status.HitPullBlock:
			HitPullBlock ();
			break;
		case Status.HitNomalBlock:
			HitNomalBlock ();
			break;
		case Status.Cancel:
			Cancel();
			break;
		}

		// 頂点を設定
		lir_.SetPosition(0, obj_player.transform.position);
		lir_.SetPosition (1, transform.position);
	}


	void OnCollisionEnter2D(Collision2D col){
		if (enu_status != Status.Shot)
			return;

		flg_hit = true;
		pos_contact = col.contacts [0].point;

        //最初の無音部分をキングクリムゾンします
        Catcher_SE.time = 0.2f;
        Catcher_SE.Play();


        switch (col.gameObject.tag) {
		case "ApproachBlock":
			enu_status = Status.HitApproachBlock;
			break;
		case "PullBlock":
			scr_pullBlock = col.gameObject.GetComponent<PullBlock> ();
			scr_pullBlock.AttachParent (gameObject);
			enu_status = Status.HitPullBlock;
			break;
		case "StayBlock":
			enu_status = Status.HitStayBlock;
			break;
		case "NomalBlock":
			enu_status = Status.HitNomalBlock;
			break;
		case "MoveBar":
			//enu_status = Status.HitNomalBlock;
			break;
		}


	}

	void OnCollisionExit2D(Collision2D col){
		flg_hit = false;
	}


	//ポジションを初期座標に固定する
	void SetPositionToInit(){
		transform.position = obj_initPos.transform.position;
		enu_status = Status.Shot;
		flg_stay = false;
	}

	//前方へ進行する
	void AddPosition(){
		scr_timeManager.TimeStop ();

		transform.Translate(spd_expansion * Time.unscaledDeltaTime,0,0);
		cnt_live--;
		num_distance += spd_expansion;

		if(cnt_live == 0){
			scr_playerMove.flg_shoted = false;
			scr_playerMove.enu_status = PlayerMove.Status.Neutoral;
			scr_timeManager.TimeStart();
			Destroy (gameObject);
		}
	}

	void HitApproachBlock(){

		if (num_distance < 0.3f) {
			flg_waitDestroy = true;
		} else {
			com_rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
			scr_playerMove.enu_status = PlayerMove.Status.WireFaced;

			GameObject baf_player = scr_playerMove.gameObject;
			com_rigidbody.isKinematic = false;

			float baf_atan = Mathf.Atan2 (transform.position.y - scr_playerMove.transform.position.y,transform.position.x - scr_playerMove.transform.position.x);
			//baf_atan = Mathf.Rad2Deg * baf_atan;

			float baf_cos = Mathf.Cos (baf_atan) * spd_expansion * Time.unscaledDeltaTime;
			float baf_sin = Mathf.Sin (baf_atan) * spd_expansion * Time.unscaledDeltaTime;
			baf_player.transform.Translate(baf_cos,baf_sin,0);

			num_distance -= spd_expansion;
		}
	}

	void HitPullBlock(){
		com_rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
		scr_playerMove.enu_status = PlayerMove.Status.WirePulled;
		enu_status = Status.Cancel;
	}

	void HitStayBlock(){
		com_rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
		scr_playerMove.enu_status = PlayerMove.Status.WireWated;
		scr_timeManager.TimeStart();
		if (!flg_stay) {
			flg_stay = true;
			//コライダーを作る
			obj_armCollider = Instantiate (pre_armCollider, transform.position, Quaternion.identity) as GameObject;
			obj_armCollider.transform.rotation = Quaternion.FromToRotation (Vector3.up, obj_player.transform.position - obj_armCollider.transform.position);
			obj_armCollider.transform.position = (obj_player.transform.position + obj_armCollider.transform.position) / 2;
			obj_armCollider.transform.localScale = new Vector3 (obj_armCollider.transform.localScale.x, (obj_player.transform.position - obj_armCollider.transform.position).magnitude * 2, obj_armCollider.transform.localScale.z);
		}

		flg_waitDestroy = true;

	}

	void HitNomalBlock(){
		com_rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
		enu_status = Status.Cancel;
	}

	void Cancel(){
		com_rigidbody.isKinematic = false;
		transform.Translate(-spd_expansion * Time.unscaledDeltaTime,0,0);
		num_distance -= spd_expansion;
		scr_playerMove.enu_status = PlayerMove.Status.WireCollected;

		if(num_distance < 0){
			
			scr_playerMove.flg_shoted = false;
			if (scr_pullBlock != null) {
				scr_pullBlock.AttachParent (scr_playerMove.gameObject, new Vector2 (0, 1));
				scr_playerMove.enu_status = PlayerMove.Status.BoxCarry;
				scr_playerMove.scr_pullBlock = scr_pullBlock;
			} else {
				scr_playerMove.enu_status = PlayerMove.Status.Neutoral;
			}
			scr_timeManager.TimeStart();
			Destroy (gameObject);
		}
	}

	public void BreakCollider(){
		Destroy (obj_armCollider);
		enu_status = Status.Cancel;
	}

}
