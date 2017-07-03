//担当者：佐藤由樹
//概要　：入力判別用スクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_Lever : MonoBehaviour
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
    private float size;
    SpriteRenderer spRenderer;

    //アクション用フラグ
    bool move = false;

    //角度用変数
    Vector3 direction;
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
        //触れている指の数が０以下なら中断
        if (Input.touchCount < 1) return;

        Touch touchInfo = Input.GetTouch(0);

        //deltapositonは、どれだけ指が動いたかのベクトル
        if(Mathf.Abs(touchInfo.deltaPosition.x) > 5)
        {
            Debug.Log("Swiped");
            swipeFlg = true;
        }

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
                gameObject.transform.eulerAngles = new Vector3(0, 0, 315);

            }


        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && swipeFlg)
        {
            move = false;

            //スワイプ終了
            SwipeE();

            //初期化
            swipeFlg = false;

            gameObject.transform.eulerAngles = new Vector3(0, 0, 315);
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
        //座標取得
        direction = touchNowPos - transform.position;

        //角度取得
        rad = Mathf.Atan2(direction.y, direction.x);

        //左移動
        if (rad < 2 && rad > 0.8)
        {
            //指の位置がレバーの角度内なら
            if (gameObject.transform.eulerAngles.z < 360 && gameObject.transform.eulerAngles.z > 270)
                gameObject.transform.eulerAngles += new Vector3(0, 0, 5);
            
            //レバーが境界線をはみ出した場合
            if (gameObject.transform.eulerAngles.z <= 270 && gameObject.transform.eulerAngles.z > 260)
                gameObject.transform.eulerAngles = new Vector3(0, 0, 275);

            Debug.Log("左周り");

        }//右移動
        else if (rad < 0.7 && rad > -1)
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


