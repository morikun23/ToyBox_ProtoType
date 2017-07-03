//担当者：海發裕都
//歯車の動作用スクリプト
//現在キー操作で左右に動くようになっている

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : Y_Swipe {


    public override void Right()
    {
        transform.Rotate(0, 0, -5);
    }
    public override void Left()
    {
        transform.Rotate(0, 0, 5);
    }


    // 歯車がキー入力で左右に動く


    void move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 0, 2);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0, -2);
        }
    }
 


}
