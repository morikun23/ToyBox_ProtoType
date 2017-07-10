using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBoard : MonoBehaviour {

    //管理番号　マネージャーによって割り当てられる
    int m_myNumber;

    //ウィンドウ内に表示されるチュートリアル用の画像
    Sprite m_sprite;

    //上記スプライトにたどり着くためのパス
    const string m_SPpass = "Images/Oyama/TutorialWindow/SP_TutorialWindow_";

    //主人公が近づいた時に表示するウィンドウの大元
    GameObject m_window;

    //CircleCast用の半径の値
    float rad = 0.4f;

    /// <summary>
    /// 初期化関数
    /// m_myNumberへ数字を代入
    /// 代入された数字をもとに自分が貼り付けるスプライトをリソースから持ってきて貼る
    /// </summary>
    /// <param name="arg_number"></param>
	public void Init (int arg_number) {
        m_window = transform.FindChild("PF_TutorialWindow").gameObject;
        m_window.SetActive(false);
        
        m_myNumber = arg_number;

        m_sprite = Resources.Load<Sprite>(m_SPpass + m_myNumber);
        SetSprite(m_sprite);
	}

    // Update is called once per frame
    void Update()
    {

        Ray ray = new Ray(transform.position, Vector3.back);
        //Debug.DrawRay(ray.origin, ray.direction, Color.red, 0.1f);
        RaycastHit2D hit = Physics2D.CircleCast(ray.origin, rad,ray.direction, 1);

        if (hit.collider)
        {
            Debug.Log("in");
            if (hit.transform.gameObject.tag == "Player")
            {
                m_window.SetActive(true);
            }

        }
        else {
            m_window.SetActive(false);
        }

        Debug.DrawRay(ray.origin, ray.direction, Color.red, 0.1f);
    }

    /// <summary>
    /// スプライトをセットする
    /// </summary>
    /// <param name="arg_sprite"></param>
    void SetSprite(Sprite arg_sprite)
    {
        transform.FindChild("PF_TutorialWindow/BaseWindow/TutorialImage").GetComponent<Image>().sprite = arg_sprite;
    }
}
