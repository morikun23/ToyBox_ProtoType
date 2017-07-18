using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour {

    //フェード用の板
    public SpriteRenderer m_filter;

    //フェード用の減衰値
    public float m_alphaNum = 0.015f;

	// Use this for initialization
	void Start () {

        m_filter.color = new Color(0, 0, 0, 1);

	}
	
	// Update is called once per frame
	void Update () {

        if (m_filter.color.a > 0) m_filter.color -= new Color(0, 0, 0, m_alphaNum);

        if(m_filter.color.a <= 0 && Input.GetKeyDown(KeyCode.Space)) Application.LoadLevel(0);

        if (Input.touchCount < 1) return;
        if(m_filter.color.a <= 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //シーン移動したい
            Application.LoadLevel(0);
        }

	}
}
