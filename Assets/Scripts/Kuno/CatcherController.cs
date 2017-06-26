using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherController : MonoBehaviour {

	//スポーン時に必要な値
	public GameObject obj_initPos;		//キャッチャーの戻る場所
	public float spd_expansion;		//プレーヤーから伸びるスピード
	public GameObject obj_Lead;		//キャッチャーとつながるオブジェクト(つまりプレーヤー)

	//その他変数
	private float num_distance = 0;		//現在のプレーヤーからの距離
	private bool flg_click;				//クリック中か？

	private bool flg_hit;				//物に当たっているかどうか
	private Vector2 pos_contact;		//当たった場所の座標
	private int cnt_live = 60;			//自動消滅までのカウント
	private Rigidbody2D com_rigidbody;	//このオブジェクトのRigidbody

	public PullBlock scr_pullBlock;

	//状態管理用enum
	public enum Status{
		Neutoral,
		Shot,
		HitPullBlock,
		HitApproachBlock,
		HitNomalBlock,
		Cancel,
		Enter,
		Break
	}
	public Status enu_status;

	// Use this for initialization
	void Start () {
		com_rigidbody = GetComponent<Rigidbody2D> (); 
		com_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	// Update is called once per frame
	void FixedUpdate () {
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
	}


	void OnCollisionEnter2D(Collision2D col){
		if (enu_status != Status.Shot)
			return;

		flg_hit = true;
		pos_contact = col.contacts [0].point;

		switch (col.gameObject.tag) {
		case "ApproachBlock":
			enu_status = Status.HitApproachBlock;
			break;
		case "PullBlock":
			scr_pullBlock = col.gameObject.GetComponent<PullBlock> ();
			scr_pullBlock.AttachParent (gameObject);
			enu_status = Status.HitPullBlock;
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
	}

	//前方へ進行する
	void AddPosition(){
		transform.Translate(spd_expansion,0,0);
		cnt_live--;
		num_distance += spd_expansion;

		if(cnt_live == 0){
			if (PlayerCheck () != null) {
				PlayerMove baf_playerMove = PlayerCheck ();
				baf_playerMove.flg_shoted = false;
				baf_playerMove.enu_status = PlayerMove.Status.Neutoral;
				Destroy (gameObject);
			}
		}
	}

	void HitApproachBlock(){
		com_rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

		//ひっかけたのがプレーヤーなら、プレーヤーを状態遷移させる
		if(PlayerCheck() != null){
			PlayerMove spr_playerMove = PlayerCheck ();
			spr_playerMove.enu_status = PlayerMove.Status.WireConnected;
		}

	}

	void HitPullBlock(){
		com_rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
		enu_status = Status.Cancel;
	}

	void HitNomalBlock(){
		com_rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
		enu_status = Status.Cancel;
	}

	void Cancel(){
		com_rigidbody.isKinematic = false;
		transform.Translate(-spd_expansion,0,0);
		num_distance -= spd_expansion;

		if(num_distance < 0){
			if (PlayerCheck () != null) {
				PlayerMove baf_playerMove = PlayerCheck ();

				baf_playerMove.flg_shoted = false;
				if (scr_pullBlock != null) {
					scr_pullBlock.AttachParent (obj_Lead, new Vector2 (0, 1));
					baf_playerMove.enu_status = PlayerMove.Status.BoxCarry;
					baf_playerMove.scr_pullBlock = scr_pullBlock;
				} else {
					baf_playerMove.enu_status = PlayerMove.Status.Neutoral;
				}

				Destroy (gameObject);
			}
		}
	}

	void Break(){
		
	}

	//Obj_Leadに引っかかっているのがプレーヤーかどうか調べる(まあプレーヤーなんだけど)
	PlayerMove PlayerCheck(){
		if(obj_Lead.gameObject.tag == "Player")
			return obj_Lead.GetComponent<PlayerMove>();

		return null;

	}
}
