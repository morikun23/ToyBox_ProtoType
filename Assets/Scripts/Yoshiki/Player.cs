//担当者：佐藤由樹
//概要　：入力判別後呼び出されるスクリプト


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Swipe {

    //移動量
    Vector3 X = new Vector3(0.1f, 0);
    Vector3 Y = new Vector3(0, 0.1f);

    //開始時
    public override void Started()
    {
        Debug.Log("開始");
    }
    //左の時
    public override void Left()
    {
        gameObject.transform.position -= X;
        Debug.Log("左移動");
    }
    //右の時
    public override void Right()
    {
        gameObject.transform.position += X;
        Debug.Log("右移動");
    }
    //上の時
    public override void Up()
    {
        gameObject.transform.position += Y;
        Debug.Log("上移動");
    }
    //下の時
    public override void Down()
    {
        gameObject.transform.position -= Y;
        Debug.Log("下移動");
    }
    //タッチ終了の時
    public override void TouchE()
    {
        Debug.Log("タッチ");
    }
    //スワイプ終了の時
    public override void SwipeE()
    {
        Debug.Log("スワイプ");
    }

    

}
