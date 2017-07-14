using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour {
    
    public GameObject ball;

	// Use this for initialization
	void Start () {
        StartCoroutine("generate");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator generate()
    {
		
		while (true) {
			// 毎フレームループします
			yield return new WaitForSeconds (2.0f);
			if (TimeManager.enu_status != TimeManager.Status.stop) {
				GameObject inst = Instantiate (ball, transform.position, Quaternion.identity);
				Destroy (inst, 5f);
			}
		}
        
    }
}
