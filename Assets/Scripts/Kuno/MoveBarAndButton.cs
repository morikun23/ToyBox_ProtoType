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

	Vector2 num_velocity;
	bool flg_stop = false;

	enum Status{
		Newtoral,
		BlockCome,
	}

	Status enu_status;

    public AudioClip m_sound;
    bool soundFlag;

	// Use this for initialization
	void Start () {
		com_boxClliderButton = obj_button.GetComponent<BoxCollider2D> ();
		com_rigidbody = obj_block.GetComponent<Rigidbody2D> ();
		com_spriteRenderer = obj_button.GetComponent<SpriteRenderer> ();

        soundFlag = false;
	}

    // Update is called once per frame
    void Update() {
        if (TimeManager.enu_status != TimeManager.Status.stop) {
            if (flg_stop) {
                com_rigidbody.constraints = RigidbodyConstraints2D.None;
                com_rigidbody.velocity = num_velocity;
                flg_stop = false;
            }
        } else {
            if (!flg_stop) {
                flg_stop = true;
                num_velocity = com_rigidbody.velocity;
                com_rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        if (soundFlag) {
            AudioSource.PlayClipAtPoint(m_sound, Camera.main.transform.position);
            soundFlag = false;
        }

		switch (com_boxClliderButton.IsTouchingLayers()) {	
		case false:
                if (com_spriteRenderer.sprite != spr_newtoralButton) soundFlag = true;

            com_spriteRenderer.sprite = spr_newtoralButton;
			Back ();
			break;
		case true:

                if (com_spriteRenderer.sprite != spr_pushedButton) soundFlag = true;
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

		//obj_block.transform.Translate (baf_spd);
		com_rigidbody.velocity = baf_spd;
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

		//obj_block.transform.Translate (baf_spd);
		com_rigidbody.velocity = baf_spd;
	}

}
