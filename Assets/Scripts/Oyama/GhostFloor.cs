using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFloor : MonoBehaviour {

    //何秒消えて出るか
    public float ghostTime = 2;

    //スプライトれんだらー
    SpriteRenderer spriteRenderer;

    //コライダー
    BoxCollider2D coll;

    //フラグ
    bool flag;

    //減衰値
    float alphaAdd = 0.1f;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        flag = false;

        Ghost();
    }
	
	// Update is called once per frame
	void Update () {

        if (flag)
        {
            if (spriteRenderer.color.a >= 0)
                spriteRenderer.color -= new Color(0, 0, 0, alphaAdd);
        }
        else {
            if (spriteRenderer.color.a <= 1)
                spriteRenderer.color += new Color(0, 0, 0, alphaAdd);
        }

        if (spriteRenderer.color.a < 0.3f) coll.enabled = false;
        else coll.enabled = true;

    }

    void Ghost()
    {
        
        flag = !flag;

        Invoke("Ghost",ghostTime);
    }

}
