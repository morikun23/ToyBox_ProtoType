//担当者：佐藤由樹
//概要　：パーティクル用スクリプト


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_Aura : MonoBehaviour {
    
    //透明化に必要な変数
    SpriteRenderer spRenderer;

    //透明用変数
    float colorA = 0;
    UnityEngine.Color color;

    //光らせるフラグ
    bool colorFlg = false;

    //距離測定フラグ
    bool rangeT = false;

    private AudioSource sound01;

    int soundFlg = 0;

    // Use this for initialization
    void Start () {

        //取得
        spRenderer = GetComponent<SpriteRenderer>();
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(1.16f, 1.16f, 1);
        transform.localEulerAngles = new Vector3(0, 0, 0);
        sound01 = GetComponent<AudioSource>();
        
    }
	
	// Update is called once per frame
	void Update () {

        //判定距離内の場合
        if (rangeT)
            Range();
    }

    void Range()
    {
        //透明時
        if (colorA > 0 && colorFlg)
        {
            colorA -= (float)0.03;
        }
        //着色時
        else if (colorA < 5 && !colorFlg)
        {
            colorA += (float)0.03;
        }

        //代入
        color = spRenderer.color;
        color.a = colorA;
        spRenderer.color = color;


        if (colorA <= 0)
        {
            colorFlg = false;
        }
        else if (colorA >= 1)
        {
            colorFlg = true;
        }
    }

    //射程範囲内の時
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Field")
        {
            rangeT = true;
            if (soundFlg == 0)
            {
                sound01.PlayOneShot(sound01.clip);
                soundFlg++;
            }
        }
        
    }

    //射程範囲外の時
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Field")
        {
            //透明にする
            rangeT = false;
            color = spRenderer.color;
            color.a = (float)0.001;
            spRenderer.color = color;
            soundFlg = 0;
        }
    }

}
