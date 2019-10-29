using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Player : MonoBehaviour {

    public float speed = 20;                //Floating point variable to store the player's movement speed.
	public float maxSpeed = 10;
	
    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
	
    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D> ();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis ("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis ("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		 moveAngleTo(movement);
		

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
		
		if( (rb2d.velocity.x > maxSpeed && movement.x > 0) || (rb2d.velocity.x < -maxSpeed && movement.x < 0) ) {
			movement.x = 0.0f;
		}
		if( (rb2d.velocity.y > maxSpeed && movement.y > 0) || (rb2d.velocity.y < -maxSpeed && movement.y < 0) ) {
			movement.y = 0.0f;
		}
		
		rb2d.AddForce(movement * speed);
		
    }
	
	void moveAngleTo(Vector2 movement) {
		float currentRot = -(rb2d.rotation % 360);
		Debug.Log("currenRot " + currentRot);
		float revSpped = 50.0f;
		// if vector of movement is pointing left
		if(movement.x < 0 && currentRot != 180) {
			if( currentRot > 180 ) {
				rb2d.MoveRotation(currentRot + revSpped * Time.fixedDeltaTime);
			}
			else
				rb2d.MoveRotation(currentRot - revSpped * Time.fixedDeltaTime);
		}
		
		// if vector of movement is pointing right
		if(movement.x > 0 && currentRot != 0) {
			if( currentRot > 180 ) {
				rb2d.MoveRotation(currentRot - revSpped * Time.fixedDeltaTime);
			}
			else
				rb2d.MoveRotation(currentRot + revSpped * Time.fixedDeltaTime);
		}
	}
}