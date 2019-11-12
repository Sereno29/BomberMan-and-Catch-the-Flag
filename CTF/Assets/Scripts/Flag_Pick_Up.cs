using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag_Pick_Up : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Player_1" || collider.gameObject.tag == "Player_2") {
            Destroy (gameObject);
        }
    }
}
