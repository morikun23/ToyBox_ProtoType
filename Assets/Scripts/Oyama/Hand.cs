using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    LineRenderer renderer;
    public GameObject Player;
    public bool hang;
    Player pl;
    public Material unko;

    // Use this for initialization
    void Start () {
        gameObject.AddComponent<LineRenderer>();

        renderer = gameObject.GetComponent<LineRenderer>();
        // 線の幅
        renderer.SetWidth(0f, 0f);
        // 頂点の数
        renderer.SetVertexCount(2);

        renderer.material = unko;

        hang = false;

        pl = Player.GetComponent<Player>();

        
    }
	
	// Update is called once per frame
	void Update () {
        // 頂点を設定
        renderer.SetPosition(0, Player.transform.position);
        renderer.SetPosition(1, transform.position);
        

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("うんこ");

        if (other.gameObject.tag == "RED" && pl.isHanging == true)
        {
            hang = true;
            Player.GetComponent<Player>().HangOn();

        }
        else if (other.gameObject.tag != "Player" && pl.isHanging == true)
        {
            Player.GetComponent<Player>().HangOn();

        }

        
    }

    public bool GetHangFlag()
    {
        return hang;
    }
}
