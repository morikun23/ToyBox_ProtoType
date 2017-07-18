using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	Rigidbody2D rig_;
	Vector2 num_velocity;
	bool flg_stop = false;

	void Start(){
		rig_ = GetComponent<Rigidbody2D> ();
	}

	void Update(){
		if (TimeManager.enu_status != TimeManager.Status.stop) {
			if (flg_stop) {
				rig_.constraints = RigidbodyConstraints2D.None;
				rig_.velocity = num_velocity;
				flg_stop = false;
			}
		} else {
			if (!flg_stop) {
				flg_stop = true;
				num_velocity = rig_.velocity;
				rig_.constraints = RigidbodyConstraints2D.FreezeAll;
			}
		}
	}

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);

            GameObject sound2 = new GameObject("Sound");
            sound2.AddComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sounds/SE/SE_BreakWall");
            sound2.AddComponent<AudioSource>().spatialBlend = 1;
            sound2.GetComponent<AudioSource>().Play();

            foreach (Transform child in other.gameObject.transform)
            {
                child.transform.GetComponent<Animator>().enabled = true;
            }

            Destroy(other.gameObject, 1);
        }

    }
}
