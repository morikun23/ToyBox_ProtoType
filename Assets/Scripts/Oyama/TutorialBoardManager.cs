using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBoardManager : MonoBehaviour {

    //看板の総数
    //int m_totalBoardCount;

    //boardプレハブのpass
    //const string m_boardPass = "Prehubs/Oyama/TutorialBoard/PF_TutorialBoard";

    //看板たち
    List<TutorialBoard> m_boards = new List<TutorialBoard>();

    //ステージプレハブ
    public GameObject m_stage;


    // Use this for initialization
    void Start () {

        int ii = 0;
        foreach (Transform chid in m_stage.transform)
        {
            if (chid.tag == "TutorialBoard")
            {

                m_boards.Add(chid.GetComponent<TutorialBoard>());
                m_boards[ii].Init(ii);

                ii++;
            }
        }
        

        /*for(int i = 0; i < m_totalBoardCount; i++)
        {

            //GameObject board = Instantiate(Resources.Load<GameObject>(m_boardPass), new Vector2(0 + i, -3.36f), Quaternion.identity);

            board.GetComponent<TutorialBoard>().Init(i+1);

        }*/

    }
	
	// Update is called once per frame
	void Update () {
        for(int i = 0; i < m_boards.Count; i++)
        {
            m_boards[i].UpdateByMyFrame();
        }

    }
}
