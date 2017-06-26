using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushedObject : MonoBehaviour {

	private Collision2D m_col_object;
	private SpriteRenderer m_spr_object;

	public GameObject m_obj_point;
	private Vector2 m_pos_point;

	private float m_num_sprWidth;

	private byte m_num_retunValue;

	public bool m_flg_Crushed = false;

	// Use this for initialization
	void Start () {
		//m_col_object = GetComponent<Collider> ();
		m_spr_object = GetComponent<SpriteRenderer> ();
		m_num_sprWidth = m_spr_object.bounds.size.x / 2;
	}
	
	// Update is called once per frame
	void Update () {
		m_pos_point = m_obj_point.transform.position;
		m_num_retunValue = 0;
		if(RayCast (Vector2.right)) m_num_retunValue += 1;
		if(RayCast (Vector2.left)) m_num_retunValue += 2;
		if(RayCast (Vector2.up)) m_num_retunValue += 4;
		if(RayCast (Vector2.down)) m_num_retunValue += 8;

		//Debug.Log (m_num_retunValue);

		if ((m_num_retunValue & 3) == 3 || (m_num_retunValue & 12) == 12) {
			m_flg_Crushed = true;
		} else {
			m_flg_Crushed = false;
		}

		if (m_flg_Crushed)
			Destroy (gameObject);
	}

	bool RayCast(Vector2 arg_direction){
		//Debug.DrawRay (m_pos_point,arg_direction * m_num_sprWidth,Color.magenta,0.01f);

		int layerMask = 1 << LayerMask.NameToLayer ("Ground");
		if (Physics2D.Raycast (m_pos_point, arg_direction, m_num_sprWidth / 2,layerMask)){
			return true;
		}
		return false;
	}
}
