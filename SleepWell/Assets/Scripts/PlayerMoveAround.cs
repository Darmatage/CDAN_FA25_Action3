using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMoveAround : MonoBehaviour {

      private Animator anim;
      //public AudioSource WalkSFX;
      private Rigidbody2D rb2D;
      private bool FaceRight = false; // determine which way player is facing.
      public static float runSpeed = 10f;
      public float startSpeed = 10f;
      public bool isAlive = true;

      void Start(){
           anim = gameObject.GetComponentInChildren<Animator>();
           rb2D = transform.GetComponent<Rigidbody2D>();
      }

      void Update(){
            //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
            //NOTE: Vertical axis: [w] / up arrow, [s] / down arrow
            Vector3 hvMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
           if (isAlive == true){
                  transform.position = transform.position + hvMove * runSpeed * Time.deltaTime;

				if (Input.GetAxis("Horizontal") != 0){
					anim.SetBool ("WalkSide", true);
				} 
				else{
					anim.SetBool ("WalkSide", false);
				}

				if (Input.GetAxis("Vertical") < 0){
					anim.SetBool ("WalkFront", true);
				} 
				else{
					anim.SetBool ("WalkFront", false);
				}
				
				if (Input.GetAxis("Vertical") > 0){
					anim.SetBool ("WalkBack", true);
				} 
				else{
					anim.SetBool ("WalkBack", false);
				}
				  
				//walking sound effect:
				/*  
				if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0)){
                	if (!WalkSFX.isPlaying){
                  		WalkSFX.Play();
                  	}
                  } else {
                  	WalkSFX.Stop();
                 }
				 */

                  // Turning. Reverse if input is moving the Player right and Player faces left.
                 if ((hvMove.x <0 && !FaceRight) || (hvMove.x >0 && FaceRight)){
                        playerTurn();
                  }
            }
      }

      private void playerTurn(){
            // NOTE: Switch player facing label
            FaceRight = !FaceRight;

            // NOTE: Multiply player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
      }

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Bed")
		{
			GameObject.FindWithTag("GameHandler").GetComponent<GameHandler_CycleStamina>().atBed = true;
		}

		if (other.gameObject.tag == "Desk")
		{
			GameObject.FindWithTag("GameHandler").GetComponent<GameHandler_CycleStamina>().atDesk = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Bed")
		{
			GameObject.FindWithTag("GameHandler").GetComponent<GameHandler_CycleStamina>().atBed = false;
		}

		if (other.gameObject.tag == "Desk")
		{
			GameObject.FindWithTag("GameHandler").GetComponent<GameHandler_CycleStamina>().atDesk = false;
		}
	}

}