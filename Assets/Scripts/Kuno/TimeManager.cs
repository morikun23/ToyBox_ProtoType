using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

	[Range(0.1f,2)]
	static public float m_num_timeScale = 1;

	public enum Status{
		neutoral,
		slow,
		stop
	}

	static public Status enu_status;
	private bool flg_stop;

	public List<Vector2> m_num_velocity = new List<Vector2>();
	public List<float> m_num_gravity = new List<float>();
	public List<GameObject> m_obj = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Time.timeScale = m_num_timeScale;
		Time.fixedDeltaTime = m_num_timeScale * 0.02f;

		if(flg_stop == true){
			enu_status = Status.stop;
		}else if (m_num_timeScale != 1) {
			enu_status = Status.slow;
		} else {
			enu_status = Status.neutoral;
		}
	}

	public void TimeStop(){
		flg_stop = true;
	}

	public void TimeStart(){
		flg_stop = false;
	}
}
