using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSight : MonoBehaviour {

	[SerializeField]
	private PlayerMove m_spr_playerMove;
	private GameObject m_obj_player;

	public float m_num_maxDistance;

	// Use this for initialization
	void Start () {
		m_obj_player = m_spr_playerMove.gameObject;
	}  
	
	// Update is called once per frame
	void Update () {
		
	}
}
