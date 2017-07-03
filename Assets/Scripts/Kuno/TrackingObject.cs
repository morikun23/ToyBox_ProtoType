using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingObject : MonoBehaviour {

	public Vector3 m_pos_colBefore;
	public Vector3 m_pos_colNew;

	private float m_num_distance;
	//private GameObject m_obj_collider;

	bool m_flg_hit = false;
	private Collision2D m_col_object;

	private List<TrackingObject> m_spr_TrackingObject = new List<TrackingObject>();

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

		m_pos_colBefore = m_pos_colNew;
		m_pos_colNew = transform.position;

		if(m_enu_mode == Mode.TrackingRecive || m_enu_mode == Mode.Both){
			if (m_flg_hit) {

				for(int i = 0;i < m_spr_TrackingObject.Count - 1;i ++){

					m_num_distance = transform.position.y - m_col_object.contacts [i].point.y;

					if (m_spr_TrackingObject[i].m_pos_colBefore != m_spr_TrackingObject[i].m_pos_colNew &&
						m_num_distance > 0) {

//						transform.position = new Vector3 (
//							transform.position.x + (m_spr_TrackingObject[i].m_pos_colNew.x - m_spr_TrackingObject[i].m_pos_colBefore.x),
//							transform.position.y + (m_spr_TrackingObject[i].m_pos_colNew.y - m_spr_TrackingObject[i].m_pos_colBefore.y),
//							0
//						);

						transform.Translate (new Vector3 (
							(m_spr_TrackingObject[i].m_pos_colNew.x - m_spr_TrackingObject[i].m_pos_colBefore.x) * 1.3f,
							m_spr_TrackingObject[i].m_pos_colNew.y - m_spr_TrackingObject[i].m_pos_colBefore.y,
							0
						));
					}
				}
			}

			m_spr_TrackingObject.Clear();
		}
	}

	void OnCollisionStay2D(Collision2D col){
		m_col_object = col;

		int baf_cnt = 0;

		for (int i = 0; i < m_col_object.contacts.Length; i++) {
			TrackingObject baf_obj;

			if(baf_obj = m_col_object.contacts [i].collider.gameObject.GetComponent<TrackingObject> ()){
				if(baf_obj.m_enu_mode == Mode.TrackingSend || baf_obj.m_enu_mode == Mode.Both){
					baf_cnt++;
					m_flg_hit = true;
					m_spr_TrackingObject.Add(baf_obj);
				}
			}
		}
	}

	void OnCollisionExit2D(Collision2D col){
		m_flg_hit = false;
		m_col_object = null;
	}
}
