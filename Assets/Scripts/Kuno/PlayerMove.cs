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

    Animator m_anime;

	public enum Status{
		Neutoral,
		WireShoted,
		WireFaced,
		WirePulled,
		WireWated,
		WireCollected,
		BoxCarry,
	}

	public Status enu_status;

	public bool flg_shoted;
	private GameObject obj_shotChatcher;

	public PullBlock scr_pullBlock;

	private float num_velocityY;

	private Collision2D m_col_object;
	private SpriteRenderer m_spr_object;

	public GameObject m_obj_point;
	private Vector2 m_pos_point;

	private float m_num_sprWidth;

	public Collider2D col_object;
	public PhysicsMaterial2D phy_neutoral;
	public PhysicsMaterial2D phy_move;

	public TimeManager scr_timeManager;


	//値を変動させるためのバッファ用変数定義
	private Rigidbody2D baf_rigidbody;
	private float baf_x;
	private float baf_y;
	public bool flg_air;

	// Use this for initialization
	void Start () {
		com_rigidBody = GetComponent<Rigidbody2D> ();

		m_spr_object = GetComponent<SpriteRenderer> ();
		m_num_sprWidth = m_spr_object.bounds.size.x / 2;

        m_anime = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate () {

		switch (enu_status) {
		case Status.Neutoral:
			NeutoralMove ();
			break;
		case Status.WireShoted:
			WireShoted ();
			break;
		case Status.WirePulled:
			ConnectedMove ();
			break;
		case Status.WireFaced:
			ConnectedMove ();
			break;
		case Status.WireWated:
			ConnectedMove ();
			break;
		case Status.WireCollected:
			ConnectedMove ();
			break;
		case Status.BoxCarry:
			BoxCarry ();
			break;
		}
	}

	void AddPositionX(float x){
		Rigidbody2D baf_rigidbody = GetComponent<Rigidbody2D> ();
		//baf_rigidbody.velocity = vec;
		transform.Translate (new Vector3(x * Time.deltaTime,0,0));
	}
	void AddPositionY(float y){
		Rigidbody2D baf_rigidbody = GetComponent<Rigidbody2D> ();
		if(TimeManager.m_num_timeScale != 0){
			baf_rigidbody.AddForce(new Vector2(0,y * 1 / TimeManager.m_num_timeScale));
		}
		//transform.Translate (vec);
	}

	void NeutoralMove(){
		baf_rigidbody = GetComponent<Rigidbody2D> ();
		baf_x = 0;
		baf_y = baf_rigidbody.velocity.y;

		if (baf_rigidbody.constraints == RigidbodyConstraints2D.FreezeAll) {
			baf_y = num_velocityY;
			baf_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        }

		m_pos_point = m_obj_point.transform.position;
		if (RayCast (Vector3.down)) {
			col_object.sharedMaterial = phy_neutoral;
            
			flg_air = false;
        } else {
			col_object.sharedMaterial = phy_move;
			flg_air = true;
        }

        m_anime.SetBool("RunFlag", false);
        m_anime.SetBool("JumpFlag", flg_air);
    }

	public void Run(int axis){
		//矢印キーで移動
		//if (Input.GetButton ("Horizontal")) {

		if (enu_status != Status.Neutoral && enu_status != Status.BoxCarry)
			return;
		if (CheckWall (new Vector2(axis,0)))
			return;

		baf_x = axis * spd_move;
		if (axis >= 0) {
			GetComponent<SpriteRenderer> ().flipX = true;
		} else {
			GetComponent<SpriteRenderer> ().flipX = false;
		}

        if(enu_status == Status.BoxCarry)
            m_anime.SetBool("HoldRunFlag", true);
        if (enu_status == Status.Neutoral)
            m_anime.SetBool("RunFlag", true);
		AddPositionX (baf_x);
	//} else {
		//baf_x = 0;
		
	//}
	}

	public void Jump(){
		if (enu_status != Status.Neutoral)
			return;
		//if (Input.GetButtonDown ("Vertical")) {
			baf_y = spd_jump;
			AddPositionY (baf_y);
			m_anime.SetBool("RunFlag", false);
			m_anime.SetBool("JumpFlag", true);
		//}
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
        m_anime.SetBool("HoldFlag", true);

        //値を変動させるためのバッファ用変数定義
        Rigidbody2D baf_rigidbody = GetComponent<Rigidbody2D> ();
		float baf_x = 0;
		float baf_y = baf_rigidbody.velocity.y;
		baf_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

		m_pos_point = m_obj_point.transform.position;
		if (RayCast (Vector3.down)) {
			col_object.sharedMaterial = phy_neutoral;
		} else {
			col_object.sharedMaterial = phy_move;
		}

        m_anime.SetBool("HoldRunFlag", false);
    }

	public void SetBox(){
		//クリックで持っているブロックを置き、ニュートラルに戻る
		//if(Input.GetKeyDown(KeyCode.Mouse0)){
			scr_pullBlock.RemoveParent ();
			m_anime.SetBool("HoldFlag", false);
			m_anime.SetBool("HoldRunFlag", false);
			enu_status = Status.Neutoral;
		//}
	}

	public void ShotWire(Vector3 objPos){
		if (flg_shoted)
			return;

		//マウスの方向に射出
		//Vector3 baf_mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		//float baf_rotation = Mathf.Atan2(baf_mouse.y - obj_catcherPos.transform.position.y,baf_mouse.x - obj_catcherPos.transform.position.x);

		//引数のposに射出
		float baf_rotation = Mathf.Atan2(objPos.y - obj_catcherPos.transform.position.y,objPos.x - obj_catcherPos.transform.position.x);

		obj_shotChatcher = Instantiate (obj_catcher,transform.position,Quaternion.identity);
		CatcherController scr_catcherController = obj_shotChatcher.GetComponent<CatcherController>();
		obj_shotChatcher.transform.eulerAngles = new Vector3 (0,0,baf_rotation * Mathf.Rad2Deg);
		scr_catcherController.obj_initPos = obj_catcherPos;
		scr_catcherController.scr_playerMove = GetComponent<PlayerMove> ();

		flg_shoted = true;

        m_anime.SetBool("RunFlag", false);
        m_anime.SetBool("JumpFlag", false);

        enu_status = Status.WireShoted;
	}

	void SightControll(){
	}

	bool RayCast(Vector2 arg_direction){

		Vector3 baf_vec = new Vector3 (m_pos_point.x + m_num_sprWidth - 0.06f,m_pos_point.y,0);
		Debug.DrawRay (baf_vec,arg_direction * m_num_sprWidth,Color.magenta,0.01f);
		int layerMask = 1 << LayerMask.NameToLayer ("Ground") | 1 << LayerMask.NameToLayer ("BlockToPlayer");
		if (Physics2D.Raycast (baf_vec, arg_direction, m_num_sprWidth,layerMask)){
			return true;
		}

		baf_vec = new Vector3 (m_pos_point.x - m_num_sprWidth + 0.06f,m_pos_point.y,0);
		Debug.DrawRay (baf_vec,arg_direction * m_num_sprWidth,Color.magenta,0.01f);
		layerMask = 1 << LayerMask.NameToLayer ("Ground") | 1 << LayerMask.NameToLayer ("BlockToPlayer");
		if (Physics2D.Raycast (baf_vec, arg_direction, m_num_sprWidth,layerMask)){
			return true;
		}
			
		return false;
	}

	bool CheckWall(Vector2 arg_direction){
		Vector3 baf_vec = new Vector3 (m_pos_point.x + arg_direction.x * m_num_sprWidth,m_pos_point.y - m_num_sprWidth / 2,0);
		Debug.DrawRay (baf_vec,arg_direction * m_num_sprWidth,Color.magenta,0.01f);
		int layerMask = 1 << LayerMask.NameToLayer ("Ground") | 1 << LayerMask.NameToLayer ("BlockToPlayer");
		if (Physics2D.Raycast (baf_vec, arg_direction, baf_x,layerMask)){
			return true;
		}

		return false;
	}
}
