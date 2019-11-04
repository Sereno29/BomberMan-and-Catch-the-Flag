using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Verde : MonoBehaviour
{
     void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Player") {
            Destroy (gameObject);
        }
    }
}
