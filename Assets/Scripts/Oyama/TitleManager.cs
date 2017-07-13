using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour {

    //「タッチでスタート！！」
    public SpriteRenderer m_startMassage; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //タッチでスタート！！の点滅
        m_startMassage.color = new Color(1, 1, 1, Mathf.PingPong(Time.time,1));

        if (Input.touchCount < 1) return;
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //シーン移動したい
            Debug.Log("in");
        }
    }
}
