using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateText : MonoBehaviour {

	public PlayerMove scr_playerMove;
    public Text text;

//    public enum State{
//        Newtral,    //たってる
//        Jump,       //ジャンプ中
//        Motsu,      //もってる
//        Bura        //ぶらさがってる
//    };

    //public State player_state;

	void Start(){
	}

	// Update is called once per frame
	void Update () {

		switch (scr_playerMove.enu_status)
        {
		case PlayerMove.Status.Neutoral:
			if (!scr_playerMove.flg_air) {
				text.text = "「とぶ」";
			} else {
				text.text = "";
			}
            break;
		case PlayerMove.Status.BoxCarry:
            text.text = "「おろす」";
            break;
		case PlayerMove.Status.WireWated:
			text.text = "「はなす」";
			break;
		case PlayerMove.Status.WireFaced:
            text.text = "「おりる」";
            break;
        }

    }

}
