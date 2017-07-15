using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBase : K_Swipe {

	public bool flg_isApproach;

	public PlayerMove scr_playerMove;

	public void Start(){
		base.Start ();
		GameObject baf_obj;
		baf_obj = GameObject.FindGameObjectWithTag("Player");
		scr_playerMove = baf_obj.GetComponent<PlayerMove> ();
	}

	public override void TouchE ()
	{
		if (!flg_isApproach || scr_playerMove.enu_status != PlayerMove.Status.Neutoral)
			return;

		scr_playerMove.ShotWire(transform.position);

	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "Field"){
			flg_isApproach = true;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if(col.tag == "Field"){
			flg_isApproach = false;
		}
	}
		
}
