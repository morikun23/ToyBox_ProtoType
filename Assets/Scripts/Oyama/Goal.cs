using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {


    //CircleCast用の半径の値
    const float m_rad = 0.4f;

    // Update is called once per frame
    void Update () {

        Ray ray = new Ray(transform.position, Vector3.back);
        //Debug.DrawRay(ray.origin, ray.direction, Color.red, 0.1f);
        RaycastHit2D hit = Physics2D.CircleCast(ray.origin, m_rad, ray.direction, 1, 1 << 8);


        if (hit.collider)
        {
            Debug.Log("goal");

        }
    }
}
