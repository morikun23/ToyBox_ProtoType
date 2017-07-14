using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateText : MonoBehaviour {

    public Text text;

    public enum State{
        Newtral,    //たってる
        Jump,       //ジャンプ中
        Motsu,      //もってる
        Bura        //ぶらさがってる
    };

    public State player_state;


	// Update is called once per frame
	void Update () {

        switch (player_state)
        {
            case State.Newtral:
                text.text = "「とぶ」";
                break;
            case State.Jump:
                text.text = "「待機」";
                break;
            case State.Motsu:
                text.text = "「おろす」";
                break;
            case State.Bura:
                text.text = "「おりる」";
                break;
        }

    }

}
