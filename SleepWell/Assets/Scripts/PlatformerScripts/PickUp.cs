using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour{

      //private GameHandler gameHandler;
      //public playerVFX playerPowerupVFX;
      public bool isSheepPickUp = true;
     
      public int newStamina = 5;

      

      void Start(){
            //gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
            //playerPowerupVFX = GameObject.FindWithTag("Player").GetComponent<playerVFX>();
      }

      public void OnTriggerEnter2D (Collider2D other){
            if (other.gameObject.tag == "Player"){
                  GetComponent<Collider2D>().enabled = false;
                  //GetComponent< AudioSource>().Play();
                  StartCoroutine(DestroyThis());

                  if (isSheepPickUp == true) {
                        GameHandler_CycleStamina.playerStamina += newStamina;
                        other.gameObject.GetComponent<PlayerMovePlatformer>().GetSheep();
                        //playerPowerupVFX.powerup();
                  }

            }
      }

      IEnumerator DestroyThis(){
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
      }

}
