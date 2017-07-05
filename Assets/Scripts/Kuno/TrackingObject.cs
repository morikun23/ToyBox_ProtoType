using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingObject : MonoBehaviour {

	public Vector3 m_pos_colBefore;
	public Vector3 m_pos_colNew;

	private float m_num_distance;

	bool m_flg_hit = false;
	private Collision2D m_col_object;

	private TrackingObject m_spr_TrackingObject;

	void Start(){
		m_pos_colBefore = Vector3.zero;
		m_pos_colNew = Vector3.zero;
	}

	public enum Mode{
		TrackingSend,
		TrackingRecive,
		Both
	}

	public Mode m_enu_mode;

	void Update(){

//		m_pos_colBefore = m_pos_colNew;
//		m_pos_colNew = transform.position;
//
//		if(m_enu_mode == Mode.TrackingRecive || m_enu_mode == Mode.Both){
//			if (m_flg_hit) {
//
//				for(int i = 0;i < m_spr_TrackingObject.Count - 1;i ++){
//
//					transform.parent = m_spr_TrackingObject [i].transform;
//				}
//			}
//
//			m_spr_TrackingObject.Clear();
//		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if ((m_enu_mode == Mode.TrackingRecive || m_enu_mode == Mode.Both) &&
			col.gameObject.GetComponent<TrackingObject>().m_enu_mode == Mode.TrackingSend) {
			m_col_object = col;
			transform.parent = col.transform;
		}
	}

	void OnCollisionExit2D(Collision2D col){

		//m_flg_hit = false;
		m_col_object = null;
		transform.parent = null;
	}
}
