using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour{

      private GameHandler_CycleStamina gameHandler_CycleStamina;
      //public playerVFX playerPowerupVFX;
      public bool isSheepPickUp = true;
     
      public int newStamina = 5;

      

      void Start(){
            gameHandler_CycleStamina = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler_CycleStamina>();
            //playerPowerupVFX = GameObject.FindWithTag("Player").GetComponent<playerVFX>();
      }

      public void OnTriggerEnter2D (Collider2D other){
            if (other.gameObject.tag == "Player"){
                  GetComponent<Collider2D>().enabled = false;
                  //GetComponent< AudioSource>().Play();
                  StartCoroutine(DestroyThis());

                  if (isSheepPickUp == true) {
                        gameHandler_CycleStamina.AddEnergy(newStamina);
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
