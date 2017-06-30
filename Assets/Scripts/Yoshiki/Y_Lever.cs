//担当者：佐藤由樹
//概要　：入力判別用スクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_Lever : MonoBehaviour
{

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
    private Quaternion rotation;

    //マウス座標
    private Vector3 changeToPos;

    //判定オブジェクトのサイズ
    private float size;
    SpriteRenderer spRenderer;

    //アクション用フラグ
    bool move = false;

    //角度用変数
    Vector3 direction;
    float angleX;
    float angleY;
    double rad;

    void Start()
    {
        //サイズの取得
        spRenderer = gameObject.GetComponent<SpriteRenderer>();
        size = spRenderer.bounds.size.y;
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
        if (Physics2D.OverlapPoint(touchNowPos))
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(touchNowPos, -Vector2.up,
                float.MaxValue, LayerMask.NameToLayer("UI"));
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
        //判定
        GetDirection();

    }


    void GetDirection()
    {
        direction = touchNowPos - transform.position;

        rad = Mathf.Atan2(direction.y, direction.x);

        if (rad < 2 && rad > 0.8)
        {
            if (gameObject.transform.rotation.z < 5 && gameObject.transform.rotation.z > -95)
            {
                gameObject.transform.Rotate(new Vector3(0, 0, 5));
            }
            Debug.Log("右");
        }
        else if (rad < 0.7 && rad > -1)
        {
            if (gameObject.transform.rotation.z > -90 && gameObject.transform.rotation.z > -5)
            {
                gameObject.transform.Rotate(new Vector3(0, 0, -5));
            }
            Debug.Log("左");
        }
        Debug.Log("タッチ");
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
}


