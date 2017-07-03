using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : Swipe
{


    bool click_flg = false; //ボタンがクリックされているか
    bool isBotton;
   Image SpriteButton;

    public Sprite OnButton;     //赤いボタン(ON)
    public Sprite OffButton;    //青いボタン(OFF)


    // Rayの当たったオブジェクトの情報を格納する
    RaycastHit hit = new RaycastHit();




    // Update is called once per frame
  /* public void Update()
    {
        StandBy();
        b_click();

    }*/

    void b_click(){

       
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);            

            if (Physics2D.OverlapPoint(clickPos))
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(clickPos, -Vector2.up,
                    float.MaxValue,LayerMask.NameToLayer("UI"));

                click_flg = true;
                //クリックされてる時ONにチェンジ
                SpriteButton.sprite = OnButton;
                Debug.Log("on" + click_flg);

            }
            //ボタンからマウスが出てるときはOFF.
            else
            {
                click_flg = false;
                //放されたときOFFにチェンジ
                SpriteButton.sprite = OffButton;
                Debug.Log("off" + click_flg);
            }

        }
       
    }


    void StandBy() {
        SpriteButton = gameObject.GetComponent<Image>();
        //クリックされてる時ONにチェンジ
        SpriteButton.sprite = OffButton;
    }

    public override void Started()
    {
        StandBy();
        Debug.Log("へい");
        click_flg = true;
        Debug.Log(SpriteButton);

    }
    public override void TouchE()
    {
        StandBy();

        click_flg = true;
        //クリックされてる時ONにチェンジ
        SpriteButton.sprite = OnButton;

    }
    public override void SwipeE()
    {
        StandBy();

        click_flg = false;
        //クリックされてる時ONにチェンジ
        SpriteButton.sprite = OffButton;
    }


}




