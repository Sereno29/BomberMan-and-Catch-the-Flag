using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag_Pick_Up : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Player") {
            Destroy (gameObject);
        }
    }
}
