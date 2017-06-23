using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand2 : MonoBehaviour {

    LineRenderer renderer;
    public GameObject Player;

    Player2 pl;
    [SerializeField]
    Sprite Idle,HangOn;

    public bool hang;
    SpriteRenderer sprite;
    Rigidbody2D rigid;

    GameObject grip;
    

    // Use this for initialization
    void Start () {
        gameObject.AddComponent<LineRenderer>();

        renderer = gameObject.GetComponent<LineRenderer>();
        // 線の幅
        renderer.SetWidth(0.1f, 0.1f);
        // 頂点の数
        renderer.SetVertexCount(2);

        renderer.material.color = Color.black;

        sprite = GetComponent<SpriteRenderer>();

        pl = Player.GetComponent<Player2>();

        hang = false;

        rigid = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        // 頂点を設定
        renderer.SetPosition(0, Player.transform.position);
        renderer.SetPosition(1, transform.position);

        if(hang == false)
        {
            sprite.sprite = Idle;
        }
        else
        {
            sprite.sprite = HangOn;
            transform.position = grip.transform.position;
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Grip" && pl.isExtend() == true)
        {
            if (hang == false)
            {
                hang = true;
                transform.rotation = other.gameObject.transform.FindChild("GripPoint").gameObject.transform.rotation;
                grip = other.gameObject.transform.FindChild("GripPoint").gameObject;
                
                pl.Act();
                return;
            }
        }

        if (other.gameObject.tag == "Strain" && pl.isExtend() == true)
        {
            if (hang == false)
            {
                grip = gameObject;
                hang = true;
                pl.Attract();
                return;
            }
        }

        if (hang == false)
        {
            if (other.gameObject.tag != "Player" && pl.isExtend() == true)
            {
                pl.Exit();
            }
        }


    }

}
