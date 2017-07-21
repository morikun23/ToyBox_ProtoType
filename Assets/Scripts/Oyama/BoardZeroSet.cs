using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardZeroSet : MonoBehaviour {

	public GameObject m_button;
	Button m_buttonScript;

	// Use this for initialization
	void Start () {
		m_buttonScript = m_button.GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		m_buttonScript.SetZero ();

	}
}
