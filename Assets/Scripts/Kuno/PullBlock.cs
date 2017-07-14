using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBlock : GimmickBase {

	Rigidbody2D com_rigidbody;
	private float num_mass;

	public Collider2D col_toPlayer;
	public Collider2D col_toGround;

	Vector2 num_velocity;
	bool flg_stop = false;

	// Use this for initialization
	public void Start(){
		base.Start ();
		com_rigidbody = GetComponent<Rigidbody2D> ();
		com_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
		num_mass = com_rigidbody.mass;
	}

	public void Update(){
		base.Update ();
		if (TimeManager.enu_status != TimeManager.Status.stop && com_rigidbody) {
			if (flg_stop) {
				com_rigidbody.gravityScale = 1;
				com_rigidbody.velocity = num_velocity;
				flg_stop = false;
			}
		} else {
			if (!flg_stop) {
				flg_stop = true;
				num_velocity = com_rigidbody.velocity;
				com_rigidbody.velocity = Vector2.zero;
				com_rigidbody.gravityScale = 0;
			}
		}
	}

	public void AttachParent(GameObject parent){
		Destroy (com_rigidbody);
		if (parent.transform.parent != null) {
			parent.transform.parent = null;
		}
		transform.parent = parent.transform;
		col_toGround.enabled = false;
		col_toPlayer.enabled = false;
	}
	public void AttachParent(GameObject parent,Vector2 localPos){
		Destroy (com_rigidbody);
		if (parent.transform.parent != null) {
			parent.transform.parent = null;
		}
		transform.parent = parent.transform;
		transform.localPosition = localPos;
		col_toGround.enabled = false;
		col_toPlayer.enabled = false;
	}

	public void RemoveParent(){
		Rigidbody2D baf_rig = gameObject.AddComponent<Rigidbody2D> ();
		com_rigidbody = baf_rig;
		com_rigidbody.mass = num_mass;
		com_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
		transform.parent = null;

		col_toGround.enabled = true;
		col_toPlayer.enabled = true;
	}

	public bool CheckParent(){
		if (transform.parent != null) {
			return true;
		} else {
			return false;
		}
	}


}
