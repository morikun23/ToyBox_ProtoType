using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : Y_Swipe
{


    bool click_flg = false; //ボタンがクリックされているか
    bool isBotton;
   Image SpriteButton;

    public Sprite OnButton;     //赤いボタン(ON)
    public Sprite OffButton;    //青いボタン(OFF)

    private AudioSource Button_SE; //ボタンのSE

   
      

    // Rayの当たったオブジェクトの情報を格納する
    RaycastHit hit = new RaycastHit();




    // Update is called once per frame
  /* public void Update()
    {
        StandBy();
        b_click();
    }*/
  
         
   public void b_click(){

       
        if (Input.GetKey(KeyCode.Mouse0)){
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);            
            if (Physics2D.OverlapPoint(clickPos)){
                RaycastHit2D hitInfo = Physics2D.Raycast(clickPos, -Vector2.up,
                    float.MaxValue,LayerMask.NameToLayer("UI"));
                click_flg = true;
                //クリックされてる時ONにチェンジ
                SpriteButton.sprite = OnButton;              
            }
            //ボタンからマウスが出てるときはOFF.
            else{
                click_flg = false;
                //放されたときOFFにチェンジ
                SpriteButton.sprite = OffButton;
            }

        }
       
    }
    

    void StandBy() {
        SpriteButton = gameObject.GetComponent<Image>();

        //SEのデータ取得
        Button_SE = GetComponent<AudioSource>();

    }
    
    public override void Started()
    {
        Button_SE.PlayOneShot(Button_SE.clip);
    }
    public override void TouchE()
    {
        StandBy();
        if (click_flg == false)
        {
            click_flg = true;
            //ONにチェンジ
            SpriteButton.sprite = OnButton;
            Debug.Log("タッチエンド=" + click_flg);
        }
        else if (click_flg == true)
        {
            StandBy();
            click_flg = false;
            //ONにチェンジ
            SpriteButton.sprite = OffButton;
            Debug.Log("タッチエンド=" + click_flg);
        }

    }
    public override void SwipeE()
    {
        StandBy();
        click_flg = false;
        //Offにチェンジ
        SpriteButton.sprite = OffButton;
    }  
}




