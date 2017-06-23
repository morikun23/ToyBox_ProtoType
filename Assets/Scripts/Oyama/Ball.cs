using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);

            foreach (Transform child in other.gameObject.transform)
            {
                child.transform.GetComponent<Animator>().enabled = true;
            }

            Destroy(other.gameObject, 1);
        }

    }
}
