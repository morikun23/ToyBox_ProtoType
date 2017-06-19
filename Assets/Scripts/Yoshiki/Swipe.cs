using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour {

    GameObject square;

    Player player;

    bool swipeFlg = false;

    private Vector3 touchStartPos;
    private Vector3 touchNowPos;

    // スクリーン座標をワールド座標に変換した位置座標
    private Vector3 Position;
    private Vector3 changeToPos;

    Vector3 Size;

    bool MOVE = false;

    // Use this for initialization
    void Start()
    {
        square = gameObject;
        player = square.GetComponent<Player>();
        Size = gameObject.transform.localScale;
    }

    // Update is called once per frame
    public void Update()
    {
        Swipes();
    }

    void Swipes()
    {
        Position = gameObject.transform.position;


        // Vector3でマウス位置座標を取得する
        changeToPos = Input.mousePosition;
        // Z軸修正
        changeToPos.z = 10f;
        // マウス位置座標をスクリーン座標からワールド座標に変換する
        touchNowPos = Camera.main.ScreenToWorldPoint(changeToPos);


        if (touchNowPos.x <= Position.x + Size.x  && touchNowPos.x >= Position.x - Size.y &&
            touchNowPos.y <= Position.y + Size.y  && touchNowPos.y >= Position.y - Size.y )
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                touchStartPos = Position;

                MOVE = true;
                Started();
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                MOVE = false;
                if (swipeFlg)
                {
                    SwipeE();
                }
                else
                {
                    TouchE();
                }
                swipeFlg = false;

            }


        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && swipeFlg)
        {
            MOVE = false;
            SwipeE();
            swipeFlg = false;
            
        }


        if (MOVE)
            Move();
    }

    void GetDirection()
    {
        float directionX = touchNowPos.x - touchStartPos.x;
        float directionY = touchNowPos.y - touchStartPos.y;
        string Direction = "NULL";

        if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
        {
            if (1 < directionX)
            {
                Direction = "right";
            }
            else if (-1 > directionX)
            {
                Direction = "left";
            }
        }
        else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
        {
            if (1 < directionY)
            {
                Direction = "up";
            }
            else if (-1 > directionY)
            {
                Direction = "down";
            }
        }


        switch (Direction)
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

    void Move()
    {
        GetDirection();

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
