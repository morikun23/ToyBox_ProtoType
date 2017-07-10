using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBoardManager : MonoBehaviour {

    //看板の総数
    const int m_totalBoardCount = 1;

    //boardプレハブのpass
    const string m_boardPass = "Prehubs/Oyama/TutorialBoard/PF_TutorialBoard";




    // Use this for initialization
    void Start () {
		
        for(int i = 0; i < m_totalBoardCount; i++)
        {

            GameObject board = Instantiate(Resources.Load<GameObject>(m_boardPass), new Vector2(0 + i, -3.36f), Quaternion.identity);

            board.GetComponent<TutorialBoard>().Init(i+1);

        }

	}
	
	// Update is called once per frame
	/*void Update () {
		
	}*/
}
