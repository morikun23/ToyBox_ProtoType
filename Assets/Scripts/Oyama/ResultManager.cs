﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.touchCount < 1) return;
        if(Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //シーン移動したい
        }

	}
}
