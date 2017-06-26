using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
	public float spd_move = 3f;
	public float spd_jump = 3f;
	private Rigidbody2D com_rigidBody;

	public GameObject obj_catcher;
	public GameObject obj_catcherPos;

	public float rot_mouse;

	public enum Status{
		Neutoral,
		WireShoted,
		WireConnected,
		BoxCarry
	}

	public Status enu_status;

	public bool flg_shoted;
	private GameObject obj_shotChatcher;

	public PullBlock scr_pullBlock;

	private float num_velocityY;
	private bool flg_hitGround;

	// Use this for initialization
	void Start () {
		com_rigidBody = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		GroundJudge ();

		switch (enu_status) {
		case Status.Neutoral:
			NeutoralMove ();
			break;
		case Status.WireShoted:
			WireShoted ();
			break;
		case Status.WireConnected:
			ConnectedMove ();
			break;
		case Status.BoxCarry:
			BoxCarry ();
			break;
		}
	}

	void AddPosition(Vector2 vec){
		Rigidbody2D baf_rigidbody = GetComponent<Rigidbody2D> ();
		baf_rigidbody.velocity = vec;
	}

	void NeutoralMove(){
		//値を変動させるためのバッファ用変数定義
		Rigidbody2D baf_rigidbody = GetComponent<Rigidbody2D> ();
		float baf_x;
		//if (flg_hitGround) {
		//	baf_x = baf_rigidbody.velocity.x;
		//} else {
			baf_x = 0;
		//}

		float baf_y = baf_rigidbody.velocity.y;

		if (baf_rigidbody.constraints == RigidbodyConstraints2D.FreezeAll) {
			baf_y = num_velocityY;
			baf_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
		}

		//矢印キーで移動
		if (Input.GetButton ("Horizontal")) {
			baf_x = Input.GetAxis ("Horizontal") * spd_move;
			if (Input.GetAxis ("Horizontal") >= 0) {
				GetComponent<SpriteRenderer> ().flipX = true;
			} else {
				GetComponent<SpriteRenderer> ().flipX = false;
			}
		}
		if (Input.GetButtonDown ("Vertical")) {
			baf_y = spd_jump;
		}

		AddPosition (new Vector2(baf_x,baf_y));

		//クリックでワイヤー射出
		if(Input.GetMouseButtonDown(0)){
			ShotWire ();
		}
	}

	void ConnectedMove(){
		
	}

	void WireShoted(){
		//値を変動させるためのバッファ用変数定義
		Rigidbody2D baf_rigidbody = GetComponent<Rigidbody2D> ();
		baf_rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
		num_velocityY = baf_rigidbody.velocity.y;
	}

	void BoxCarry(){
		//値を変動させるためのバッファ用変数定義
		Rigidbody2D baf_rigidbody = GetComponent<Rigidbody2D> ();
		float baf_x = 0;
		float baf_y = baf_rigidbody.velocity.y;
		baf_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

		//矢印キーで移動
		if (Input.GetButton ("Horizontal")) {
			baf_x = Input.GetAxis ("Horizontal") * spd_move;
			if (Input.GetAxis ("Horizontal") >= 0) {
				GetComponent<SpriteRenderer> ().flipX = true;
			} else {
				GetComponent<SpriteRenderer> ().flipX = false;
			}
		}
		if (Input.GetButtonDown ("Vertical")) {
			baf_y = spd_jump / 2;
		}


		AddPosition (new Vector2(baf_x,baf_y));

		//クリックで持っているブロックを置き、ニュートラルに戻る
		if(Input.GetMouseButtonDown(0)){
			scr_pullBlock.RemoveParent ();
			enu_status = Status.Neutoral;
		}
	}


	void ShotWire(){
		if (flg_shoted)
			return;

		//マウスの方向に射出
		Vector3 baf_mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		float baf_rotation = Mathf.Atan2(baf_mouse.y - transform.position.y,baf_mouse.x - transform.position.x);

		obj_shotChatcher = Instantiate (obj_catcher,transform.position,Quaternion.identity);
		CatcherController scr_catcherController = obj_shotChatcher.GetComponent<CatcherController>();
		obj_shotChatcher.transform.eulerAngles = new Vector3 (0,0,baf_rotation * Mathf.Rad2Deg);
		scr_catcherController.obj_initPos = obj_catcherPos;
		scr_catcherController.obj_Lead = this.gameObject;

		flg_shoted = true;
		enu_status = Status.WireShoted;
	}

	void GroundJudge(){
		RaycastHit2D baf_ray = Physics2D.Raycast (new Vector3(transform.position.x,transform.position.y - 0.5f,transform.position.z), Vector2.down, 0.1f);

		if (baf_ray) {
			flg_hitGround = true;
			//Debug.Log (baf_ray.collider.gameObject.name);
		} else {
			flg_hitGround = false;
		}
	}

	void SightControll(){
	}

}
