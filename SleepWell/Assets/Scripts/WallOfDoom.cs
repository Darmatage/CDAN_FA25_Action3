using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class WallOfDoom : MonoBehaviour {
       //NOTE: this script moves right-ward by default, but turn on isVertical to move upward;
       public float moveDelay = 1f;
       public float moveRate = 100f;
       public bool isVertical = true;
       public bool startMove = false;
       public float moveTimer = 0;
       private Rigidbody2D rb2D;
       public Vector2 forceVector;
       //public GameObject startDoomEffect; //uncomment to spawn a spritesheet or particles on move start
        Animator anim; //uncomment for animated wall (rotating spike wheels, roiling fire or lava, etc)
       //public AudioSource startSFX;

       void Start(){
              rb2D = gameObject.GetComponent<Rigidbody2D>();
              anim = gameObject.GetComponentInChildren<Animator>();
       }

       void FixedUpdate(){
              moveTimer += 0.01f;
              if (moveTimer >= moveDelay){
                     startMove = true;
                     //GameObject startMove = Instantiate (startDoomEffect, transform.position, transform.rotation);
                     //anim.SetBool("deathmotion", true);
                     //startSFX.Play();
              }

              if (startMove == true){
                     float moveForce = moveRate * Time.fixedDeltaTime;
                     if (isVertical == false){
                            forceVector = new Vector2(moveForce, 0);
                     } else {
                            forceVector = new Vector2(0, moveForce);
                     }
                     rb2D.linearVelocity = forceVector;
              }
       }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Debug.Log("Boss ate player");
			GameObject.FindWithTag("GameHandler").GetComponent<GameHandler_PlayerGetHurt>().PlayerGetHurt(100);
		}
	}


} 