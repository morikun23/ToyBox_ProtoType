using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {


    //CircleCast用の半径の値
    const float m_rad = 0.4f;

    public SpriteRenderer m_filter;

    bool fade;

    float alpha = 0.015f;

    void Start()
    {
        m_filter.color = new Color(1, 1, 1, 0);
        fade = false;
    }

    // Update is called once per frame
    void Update () {

        Ray ray = new Ray(transform.position, Vector3.back);
        //Debug.DrawRay(ray.origin, ray.direction, Color.red, 0.1f);
        RaycastHit2D hit = Physics2D.CircleCast(ray.origin, m_rad, ray.direction, 1, 1 << 8);


        if (hit.collider)
        {
            Debug.Log("goal");

            fade = true;
            

        }

        if(fade)
            m_filter.color += new Color(0, 0, 0, alpha);
    }
}
