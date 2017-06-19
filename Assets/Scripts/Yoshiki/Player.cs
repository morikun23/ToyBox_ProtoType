using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Swipe {

    Vector3 X = new Vector3(0.1f, 0);
    Vector3 Y = new Vector3(0, 0.1f);

    public override void Started()
    {
        Debug.Log("開始");
    }

    public override void Left()
    {
        //gameObject.transform.position -= X;
        Debug.Log("左移動");
    }
    public override void Right()
    {
        //gameObject.transform.position += X;
        Debug.Log("右移動");
    }
    public override void Up()
    {
        //gameObject.transform.position += Y;
        Debug.Log("上移動");
    }
    public override void Down()
    {
        //gameObject.transform.position -= Y;
        Debug.Log("下移動");
    }
    public override void TouchE()
    {
        Debug.Log("タッチ");
    }
    public override void SwipeE()
    {
        Debug.Log("スワイプ");
    }


}
