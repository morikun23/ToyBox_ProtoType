using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RappidBlock : MonoBehaviour {

    //左右の移動量
    public float max_distance = 3f;

    //オブジェ
    public GameObject block;

    float xx, yy;

    public GameObject button;

    private BoxCollider2D boxClliderButton;
    private SpriteRenderer buttonSpriteRenderer;

    public Sprite spr_newtoralButton;
    public Sprite spr_pushedButton;

    public AudioClip s_button;

    //加速量
    public float moveAdd = 9;

    public GameObject ballPoint;
    public GameObject ball;

    // Use this for initialization
    void Start () {
        xx = block.transform.position.x;
        yy = block.transform.position.y;

        boxClliderButton = button.GetComponent<BoxCollider2D>();

        buttonSpriteRenderer = button.GetComponent<SpriteRenderer>();
        
    }
	
	// Update is called once per frame
	void Update () {
        
		if (TimeManager.enu_status != TimeManager.Status.stop) {
			block.transform.position = new Vector3 (xx + Mathf.PingPong (Time.time * moveAdd, max_distance), yy, 0);
		}

	    if (boxClliderButton.IsTouchingLayers(1 << LayerMask.NameToLayer("Catcher")) && buttonSpriteRenderer.sprite == spr_newtoralButton)
	    {
	        GameObject sound2 = new GameObject("Sound");
	        sound2.AddComponent<AudioSource>().clip = s_button;
	        sound2.GetComponent<AudioSource>().Play();

	        GameObject a_ball = Instantiate(ball, ballPoint.transform.position, Quaternion.identity);
	        Destroy(a_ball, 5);
	        buttonSpriteRenderer.sprite = spr_pushedButton;
	        
	    }
		
    }
}
