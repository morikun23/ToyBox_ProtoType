using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_DUMMY : MonoBehaviour {

    public Button button = new Button();
    public enum State
    {
        neutral,
        slow,
    }
    

	// Update is called once per frame
	void Update () {
        button.b_click();
	}
}
