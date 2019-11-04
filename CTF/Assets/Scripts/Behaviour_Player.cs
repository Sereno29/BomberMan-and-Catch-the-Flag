using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Behaviour_Player : MonoBehaviour {

    public float acceleration;                //Floating point variable to store the player's movement speed.
	public float maxSpeed;
	
    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
	private Animator anim;

    // Use this for initialization
    void Start()
    {
    //Get and store a reference to the Rigidbody2D component so that we can access it.
       rb2d = GetComponent<Rigidbody2D> ();
	   anim = GetComponent<Animator> ();
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
		 //moveAngleTo(movement);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
		
		if( (rb2d.velocity.x >= maxSpeed && movement.x > 0) || (rb2d.velocity.x < -maxSpeed && movement.x < 0) ) {
			movement.x = 0.0f;
		}
		if( (rb2d.velocity.y >= maxSpeed && movement.y > 0) || (rb2d.velocity.y < -maxSpeed && movement.y < 0) ) {
			movement.y = 0.0f;
		}
		
		rb2d.velocity = movement * acceleration;
	
		if((moveHorizontal != 0) || (moveVertical!= 0)) {
			rotateTo(moveHorizontal, moveVertical);	

		}

		anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x)+Mathf.Abs(rb2d.velocity.y));
		anim.SetBool("isFrozen", false);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Flag") {
            anim.SetBool("isFlagged", true);
        }
    }
	void Upadate() {
		// anim.SetFloat("Speed", 1);
		// anim.SetBool("isFlagged", false);
		// anim.SetBool("isFrozen", false);
	}
	
	private float angle;
	Quaternion rotation;
	private Vector3 direction;
	
	void rotateTo(float x, float y) {
		Vector3 movement = new Vector3();
		movement.Set(x, y, 0);
		direction =  movement; // get direction moviment
		angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // get angle
		rotation  = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 *  Time.fixedDeltaTime);
	}

}
