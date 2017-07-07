//担当者：佐藤由樹
//概要　：入力判別用スクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_Lever : Y_Swipe
{

    //スワイプ用フラグ
    bool swipeFlg = false;

    //タッチ開始位置
    private Vector3 touchStartPos;
    //タッチ現在位置
    private Vector3 touchNowPos;

    //判定オブジェクトの位置
    private Vector3 position;

    //マウス座標
    private Vector3 changeToPos;

    //判定オブジェクトのサイズ
    //private float size;
    SpriteRenderer spRenderer;

    //アクション用フラグ
    bool move = false;

    //角度用変数
    Vector3 direction;
    double rad;


    public override void SwipeE()
    {
        gameObject.transform.eulerAngles = new Vector3(0, 0, 315);
    }

    public override void LeftR()
    {
        //指の位置がレバーの角度内なら
        if (gameObject.transform.eulerAngles.z < 360 && gameObject.transform.eulerAngles.z > 270)
            gameObject.transform.eulerAngles += new Vector3(0, 0, 5);

        //レバーが境界線をはみ出した場合
        if (gameObject.transform.eulerAngles.z <= 270 && gameObject.transform.eulerAngles.z > 260)
            gameObject.transform.eulerAngles = new Vector3(0, 0, 275);

        Debug.Log("左周り");
    }

    public override void RightR()
    {
        //指の位置がレバーの角度内なら
        if (gameObject.transform.eulerAngles.z < 360 && gameObject.transform.eulerAngles.z > 270)
            gameObject.transform.eulerAngles += new Vector3(0, 0, -5);

        //レバーが境界線をはみ出した場合
        if (gameObject.transform.eulerAngles.z < 10)
            gameObject.transform.eulerAngles = new Vector3(0, 0, 355);

        Debug.Log("右回り");
    }
}


