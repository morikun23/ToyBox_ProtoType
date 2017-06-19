using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBlock : MonoBehaviour {

	Rigidbody2D com_rigidbody;
	private float num_mass;

	public BoxCollider2D col_toPlayer;
	public BoxCollider2D col_toGround;

	// Use this for initialization
	void Start () {
		com_rigidbody = GetComponent<Rigidbody2D> ();
		com_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
		num_mass = com_rigidbody.mass;
	}
	
	// Update is called once per frame
	void Update () {
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
