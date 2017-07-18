using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFllor : MonoBehaviour {

    public float moveLimit;
    float xx,yy;

    public GameObject button;

    private BoxCollider2D boxClliderButton;
    private SpriteRenderer buttonSpriteRenderer;

    public Sprite spr_newtoralButton;
    public Sprite spr_pushedButton;

    public GameObject Floor;

    //うごくかどうか
    bool act;

	public float spd_moveX,spd_moveY;

    public AudioClip s_floor,s_button;

	// Use this for initialization
	void Start () {
        xx = Floor.transform.position.x;
        yy = Floor.transform.position.y;

        act = false;

        boxClliderButton = button.GetComponent<BoxCollider2D>();
        //com_rigidbody = block.GetComponent<Rigidbody2D>();
        buttonSpriteRenderer = button.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

		if (TimeManager.enu_status == TimeManager.Status.stop)
			return;

        if (act)
        {
            
            Floor.transform.position = new Vector2(xx + Mathf.PingPong(Time.time * spd_moveX, moveLimit), yy + Mathf.PingPong(Time.time * spd_moveY, moveLimit));
        }
        if(boxClliderButton.IsTouchingLayers() && buttonSpriteRenderer.sprite == spr_newtoralButton)
        {
            GameObject sound = new GameObject("Sound");
            sound.transform.parent = Floor.transform;
            sound.transform.position = Floor.transform.position;
            sound.AddComponent<AudioSource>().clip = s_floor;
            sound.GetComponent<AudioSource>().volume = 0.3f;
            sound.GetComponent<AudioSource>().spatialBlend = 1;
            sound.GetComponent<AudioSource>().loop = true;
            sound.GetComponent<AudioSource>().Play();

            GameObject sound2 = new GameObject("Sound");
            sound2.AddComponent<AudioSource>().clip = s_button;
            sound2.GetComponent<AudioSource>().Play();
            
            buttonSpriteRenderer.sprite = spr_pushedButton;
            act = true;
        }
    }

    //うごけ
    public void Action()
    {
        act = true;
    }
}
