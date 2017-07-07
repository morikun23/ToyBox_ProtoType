//担当者：佐藤由樹
//概要　：入力判別用スクリプト
//参考  ：https://gist.github.com/Buravo46/8367810

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_Swipe : MonoBehaviour {

    //UI?
    GameObject square;

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
    private　Vector3 size;

    //アクション用フラグ
    bool move = false;

    //角度用変数
    Vector3 rotate;
    double rad;

    public void Start()
    {
        //サイズの取得
        size = gameObject.transform.localScale;
    }

    public void Update()
    {
        //タッチ、スワイプ位置判断
        Swipes();
    }

    void Swipes()
    {
        //オブジェクト位置取得
        position = gameObject.transform.position;


        // マウス位置座標を取得する
        changeToPos = Input.mousePosition;
        // Z軸修正
        changeToPos.z = 10f;
        // マウス位置座標をスクリーン座標からワールド座標に変換する
        touchNowPos = Camera.main.ScreenToWorldPoint(changeToPos);

        //範囲内にマウスがいるか判定
        if (touchNowPos.x <= position.x + size.x  && touchNowPos.x >= position.x - size.y &&
            touchNowPos.y <= position.y + size.y  && touchNowPos.y >= position.y - size.y )
        {
            //タッチ
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //位置取得
                touchStartPos = position;

                move = true;

                //開始
                Started();
            }
            //指を離す
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                move = false;

                if (swipeFlg)
                {
                    //スワイプ終了
                    SwipeE();
                }
                else
                {
                    //タッチ終了
                    TouchE();
                }
                swipeFlg = false;

            }


        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && swipeFlg)
        {
            move = false;

            //スワイプ終了
            SwipeE();

            //初期化
            swipeFlg = false;
            
        }

        //アクション
        if (move)
            Move();
    }

    void Move()
    {
        //上下左右判定
        GetDirection();
        GetRotate();

    }

    void GetDirection()
    {
        //x座標判定用
        float directionX = touchNowPos.x - touchStartPos.x;
        //y座標判定用
        float directionY = touchNowPos.y - touchStartPos.y;
        //判定引数
        string direction = "NULL";

        //xが長い場合
        if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
        {
            //右にいる場合
            if (1 < directionX)
            {
                direction = "right";
            }
            //左にいる場合
            else if (-1 > directionX)
            {
                direction = "left";
            }
        }
        //yが長い場合
        else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
        {
            //上にいる場合
            if (1 < directionY)
            {
                direction = "up";
            }
            //下にいる場合
            else if (-1 > directionY)
            {
                direction = "down";
            }
        }

        //呼び出すものを判別
        switch (direction)
        {
            case "up":
                Up();
                swipeFlg = true;
                break;

            case "down":
                Down();
                swipeFlg = true;
                break;

            case "right":
                Right();
                swipeFlg = true;
                break;

            case "left":
                Left();
                swipeFlg = true;
                break;

        }

    }

    void GetRotate()
    {
        Debug.Log("rotate");
        //座標取得
        rotate = touchNowPos - transform.position;

        //角度取得
        rad = Mathf.Atan2(rotate.y, rotate.x);

        //左移動
        if (rad < 2 && rad > 0.8)
        {

            LeftR();

        }//右移動
        else if (rad < 0.7 && rad > -1)
        {

            RightR();

        }
    }

    public virtual void Started()
    {

    }

    public virtual void Up()
    {

    }
    public virtual void Down()
    {

    }
    public virtual void Left()
    {

    }
    public virtual void Right()
    {

    }
    public virtual void TouchE()
    {

    }
    public virtual void SwipeE()
    {

    }

    public virtual void RightR()
    {

    }

    public virtual void LeftR()
    {

    }
}
