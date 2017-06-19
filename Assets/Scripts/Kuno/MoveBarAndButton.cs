using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBarAndButton : MonoBehaviour {

	public Vector2 pos_clampMin;
	public Vector2 pos_clampMax;

	public float spd_comeMove;
	public float spd_backMove;

	public GameObject obj_button;
	public GameObject obj_block;

	public Sprite spr_newtoralButton;
	public Sprite spr_pushedButton;

	private BoxCollider2D com_boxClliderButton;
	private Rigidbody2D com_rigidbody;
	private SpriteRenderer com_spriteRenderer;

	private bool flg_push;

	enum Status{
		Newtoral,
		BlockCome,
	}

	Status enu_status;

	// Use this for initialization
	void Start () {
		com_boxClliderButton = obj_button.GetComponent<BoxCollider2D> ();
		com_rigidbody = obj_block.GetComponent<Rigidbody2D> ();
		com_spriteRenderer = obj_button.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		switch (com_boxClliderButton.IsTouchingLayers()) {	
		case false:
			com_spriteRenderer.sprite = spr_newtoralButton;
			Back ();
			break;
		case true:
			com_spriteRenderer.sprite = spr_pushedButton;
			Come ();
			break;
		}
	}

	void Come(){
		Vector2 baf_spd = Vector2.zero;

		if (pos_clampMax.x > pos_clampMin.x && obj_block.transform.localPosition.x < pos_clampMax.x) {
			baf_spd.x += spd_comeMove;
		} else 
			if(pos_clampMax.x < pos_clampMin.x && obj_block.transform.localPosition.x > pos_clampMax.x){
			baf_spd.x -= spd_comeMove;
		}

		if (pos_clampMax.y > pos_clampMin.y && obj_block.transform.localPosition.y < pos_clampMax.y) {
			baf_spd.y += spd_comeMove;
		} else 
			if(pos_clampMax.y < pos_clampMin.y && obj_block.transform.localPosition.y > pos_clampMax.y){
			baf_spd.y -= spd_comeMove;
		}

		obj_block.transform.Translate (baf_spd);
		//com_rigidbody.velocity = baf_spd;
	}

	void Back(){
		Vector2 baf_spd = Vector2.zero;

		if (pos_clampMax.x > pos_clampMin.x && obj_block.transform.localPosition.x > pos_clampMin.x) {
			baf_spd.x -= spd_backMove;
		} else 
			if(pos_clampMax.x < pos_clampMin.x && obj_block.transform.localPosition.x < pos_clampMin.x){
				baf_spd.x += spd_backMove;
			}

		if (pos_clampMax.y > pos_clampMin.y && obj_block.transform.localPosition.y > pos_clampMin.y) {
			baf_spd.y -= spd_backMove;
		} else 
			if(pos_clampMax.y < pos_clampMin.y && obj_block.transform.localPosition.y < pos_clampMin.y){
				baf_spd.y += spd_backMove;
			}

		obj_block.transform.Translate (baf_spd);
		//com_rigidbody.velocity = baf_spd;
	}

}
