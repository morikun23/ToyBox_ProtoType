using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    GameObject player,goal;
    public Sprite goalsprite;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        goal = GameObject.Find("Goal");
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position,player.transform.position) < 0.5f)
        {
            goal.GetComponent<SpriteRenderer>().sprite = goalsprite;
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
	}
}
