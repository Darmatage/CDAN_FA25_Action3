using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

      //public Animator anim;
      public Rigidbody2D rb;
      public float jumpForce = 6f;
      public LayerMask groundLayer;
      public LayerMask enemyLayer;
      public bool canJump = false;
      public int jumpTimes = 0;
      public bool isAlive = true;
      //public AudioSource JumpSFX;

      void Start(){
            //anim = gameObject.GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
      }

     void Update() {
           if ((Input.GetButtonDown("Jump")) && (canJump) && (isAlive == true)) {
                  Jump();
            }
      }

      public void Jump() {
            if (jumpTimes <2){
                  jumpTimes += 1;
                  rb.linearVelocity = Vector2.up * jumpForce;
                  // anim.SetTrigger("Jump");
                  // JumpSFX.Play();
            }  else {
                  canJump = false;
            }
      }

      public void OnCollisionEnter2D(Collision2D other){
            //if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            if (
                  (((1 << other.gameObject.layer) & groundLayer.value) != 0) ||
                  (((1 << other.gameObject.layer) & enemyLayer.value) != 0)
            ){
                  //Debug.Log("I am trouching ground!");
                  jumpTimes = 0;
                  canJump = true;
            }
      }
}