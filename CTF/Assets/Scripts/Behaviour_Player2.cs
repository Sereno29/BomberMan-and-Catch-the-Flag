using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using UnityEngine.SceneManagement;

public class Behaviour_Player2 : MonoBehaviour {

    public float acceleration;                //Floating point variable to store the player's movement speed.
	public float maxSpeed;
	public bool quit; 
    private bool isFlagged;
    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
	private Animator anim;
    Transform t;
    float moveHorizontal;
    float moveVertical;
    Thread thread2;
    Vector2 velocity;

    // Use this for initialization
    void Start()
    {
    //Get and store a reference to the Rigidbody2D component so that we can access it.
       rb2d = GetComponent<Rigidbody2D> ();
	   anim = GetComponent<Animator> ();
       velocity = new Vector2(0, 0);
       quit = false;
       isFlagged = false;
       GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.7f, 0.9f);
       thread2 = new Thread(Run2);
       thread2.Start();
    }

    void Run2() {

       while(true) {
            //Use the two store floats to create a new Vector2 variable movement.
            Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
            //moveAngleTo(movement);

            //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
            if( (velocity.x >= maxSpeed && movement.x > 0) || (velocity.x < -maxSpeed && movement.x < 0) ) {
                movement.x = 0.0f;
            }
            if( (velocity.y >= maxSpeed && movement.y > 0) || (velocity.y < -maxSpeed && movement.y < 0) ) {
                movement.y = 0.0f;
            }
            velocity = movement * acceleration;
        }
    }
    
    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        
        //Store the current horizontal input in the float moveHorizontal.
        moveHorizontal = Input.GetAxis ("P2_Horizontal");
        
        //Store the current vertical input in the float moveVertical.
        moveVertical = Input.GetAxis ("P2_Vertical");
        rb2d.velocity = velocity;
		anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x) + Mathf.Abs(rb2d.velocity.y));
		anim.SetBool("isFrozen", false);
        
        if((moveHorizontal != 0) || (moveVertical!= 0)) {
            rotateTo(moveHorizontal, moveVertical);	
        }
        
        if(quit) {
            OnApplicationQuit();
        }
            
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Flag") {
            anim.SetBool("isFlagged", true);
            isFlagged = true;
        }
        
        if(collider.gameObject.tag == "Victory_Blue" && isFlagged == true)
        {
            SceneManager.LoadScene("YOU WIN", LoadSceneMode.Additive);
        }
        
        if(collider.gameObject.tag == "Player_1" && isFlagged == true)
        {
            anim.SetBool("isFlagged", false);
            isFlagged = false;
        }
    }
    
	void Upadate() {

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

    void OnApplicationQuit() {
        thread2.Abort();
    }
}
