using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_Swipe : MonoBehaviour {

	Vector3[] pos_input = new Vector3[10];
	Vector3 pos_inputScreen;
	Vector3[] pos_inputBefore = new Vector3[10];
	Vector3[] pos_init = new Vector3[10];
	Vector3 num_size;

	public Transform pos_rotatePoint;

	Vector2 pos_screenBefore;
	Vector2 pos_screenNew;

	bool[] flg_start = new bool[10];
	bool[] flg_move = new bool[10];
	int[] num_rotateDirection = new int[10];

	// Use this for initialization
	public void Start () {
		//サイズの取得
		if (GetComponent<BoxCollider2D> ()) {
			num_size = GetComponent<BoxCollider2D>().bounds.size;
		} else if(GetComponent<CircleCollider2D> ()){
			num_size = GetComponent<CircleCollider2D>().bounds.size;
		}else if(GetComponent<SpriteRenderer>()){
			num_size = GetComponent<SpriteRenderer> ().bounds.size;
		}

		//回転の中心を取得(Hieralkey上で何もなければ)
		if(pos_rotatePoint == null){
			pos_rotatePoint = transform;
		}
	}
	
	// Update is called once per frame
	public void Update () {
		pos_screenBefore = pos_screenNew;
		pos_screenNew = Camera.main.transform.position;

		int baf_i = 0;
		foreach (Touch t in Input.touches) {
			baf_i++;
			//タッチ位置を取得
			//１フレーム前のタッチ座標を格納
			pos_inputBefore[baf_i] = pos_input[baf_i];
			pos_inputScreen = t.position;
			pos_input[baf_i] = Camera.main.ScreenToWorldPoint (t.position);
				switch(t.phase){
				case TouchPhase.Began:
					if (!CheckHitPoint (pos_input[baf_i]))
						continue;
					Started ();
					pos_init[baf_i] = pos_input[baf_i];
					flg_start[baf_i] = true;
					//Debug.Log ("START");
					break;
				
				case TouchPhase.Moved:
					if (!flg_start [baf_i])
						continue;
					//Debug.Log ("FINGER MOVEING");
					Moving (t.deltaPosition);
					CheckRotate (t,baf_i);
					flg_move [baf_i] = true;
					break;
				
				case TouchPhase.Ended:
					if (!flg_start [baf_i])
						continue;

					if (!CheckMove (baf_i)) {
						SwipeE ();
						//Debug.Log ("SWIPE END");
					} else {
						TouchE ();
						//Debug.Log ("TOUCH END");
					}
					flg_start [baf_i] = false;
					flg_move [baf_i] = false;
					num_rotateDirection [baf_i] = 0;

					pos_input [baf_i] = Vector3.zero;
					pos_inputBefore [baf_i] = Vector3.zero;
					break;
				}

				if (flg_move[baf_i] && flg_start[baf_i]) {
					if(num_rotateDirection[baf_i] == 1){
						RightR (1);
					}else if(num_rotateDirection[baf_i] == -1){
						LeftR (-1);
					}
					break;
				}
		}
	}

	bool CheckHitPoint(Vector3 pos){
		//タッチ箇所がオブジェクトと重なっているかチェック
		if (pos.x <= transform.position.x + num_size.x / 2 && pos.x >= transform.position.x - num_size.x / 2 &&
			pos.y <= transform.position.y + num_size.y / 2 && pos.y >= transform.position.y - num_size.y / 2) {
			return true;
		}
		return false;
	}

	//タッチの許容範囲かどうかの計測
	bool CheckMove(int i){
		if(Mathf.Abs(pos_input[i].x - pos_init[i].x) <= 1 &&
			Mathf.Abs(pos_input[i].y - pos_init[i].y) <= 1){
			return true;
		}
		return false;
	}

	void CheckRotate(Touch t,int i){
		Vector2 baf_pos = Camera.main.WorldToScreenPoint(pos_rotatePoint.position);
		float baf_rotBef = Mathf.Atan2 (baf_pos.y - Camera.main.WorldToScreenPoint(pos_inputBefore[i]).y,
			baf_pos.x - Camera.main.WorldToScreenPoint(pos_inputBefore[i]).x) * Mathf.Rad2Deg;
		float baf_rotNew = Mathf.Atan2 (baf_pos.y - Camera.main.WorldToScreenPoint(pos_input[i]).y + (pos_screenNew.y - pos_screenBefore.y),
			baf_pos.x - Camera.main.WorldToScreenPoint(pos_inputBefore[i]).x) * Mathf.Rad2Deg;



		if(baf_rotNew - baf_rotBef > 2){
			num_rotateDirection[i] = 1;
		}
		else if(baf_rotNew - baf_rotBef < -2){
			num_rotateDirection[i] = -1;
		}
	}

	public virtual void Started(){}
	public virtual void Moving(Vector2 direc){}
	public virtual void TouchE(){}
	public virtual void SwipeE(){}
	public virtual void RightR(float dist){}
	public virtual void LeftR(float dist){}

}
