using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D col){

		//Debug.Log (col.name);
		col.transform.root.parent = transform;
	}

	void OnTriggerExit2D(Collider2D col){
		Transform baf_obj = col.transform;
		bool baf_loop = false;

		if (baf_obj != null) {
			while(!baf_loop){
				if (baf_obj.parent == transform || baf_obj.parent == null) {
					baf_loop = true;
				} else {
					baf_obj = baf_obj.parent;
				}
			}
		}

		baf_obj.transform.parent = null;
	}

}
