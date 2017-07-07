//担当者：佐藤由樹
//概要　：プレイヤーtest移動用スクリプト


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_Move : MonoBehaviour {

    //移動量
    Vector3 X = new Vector3(0.1f, 0);
    //移動方向
    string direction;


	// Update is called once per frame
	void Update () {
		
        if (Input.GetKey(KeyCode.A))
        {
            direction = "left";
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction = "right";
        }
        GetDirection();

    }
    void GetDirection()
    {

        //呼び出すものを判別
        switch (direction)
        {

            case "right":
                transform.position += X;
                break;

            case "left":
                transform.position -= X;
                break;

        }
        //初期化
        direction = null;

    }
}
