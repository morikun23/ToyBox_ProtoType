using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFllor : MonoBehaviour {

    public float moveLimit;
    float xx,yy;

    public bool xMove, yMove;

	// Use this for initialization
	void Start () {
        xx = transform.position.x;
        yy = transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {

        if(xMove && yMove)
        {
            transform.position = new Vector2(xx + Mathf.PingPong(Time.time + 1, moveLimit), yy + Mathf.PingPong(Time.time + 1, moveLimit));

        }
        else
        {
            if (xMove)
            {
                transform.position = new Vector2(xx + Mathf.PingPong(Time.time + 1, moveLimit), yy);
            }
            else if (yMove)
            {
                transform.position = new Vector2(xx, yy + Mathf.PingPong(Time.time + 1, moveLimit));
            }
            
        }

	}
}
