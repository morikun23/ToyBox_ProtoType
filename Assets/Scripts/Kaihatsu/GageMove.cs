using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GageMove : MonoBehaviour {
    public Image image;

	
	// Update is called once per frame
	void Update () {

        if (image.fillAmount < 1)
        {
            image.fillAmount += 0.01f;
        }


	}
}
