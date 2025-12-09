using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour{

    private GameHandler_CycleStamina gameHandler_CycleStamina;
    public AudioSource PickupSFX;
    public bool isSheepPickUp = true;
    public int newStamina = 5;

	public bool isBatteryPickUp = false;
	public int newBatteries = 3;	

      void Start(){
            gameHandler_CycleStamina = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler_CycleStamina>();
            //playerPowerupVFX = GameObject.FindWithTag("Player").GetComponent<playerVFX>();
      }

      public void OnTriggerEnter2D (Collider2D other){
            if (other.gameObject.tag == "Player"){
                  GetComponent<Collider2D>().enabled = false;
                  PickupSFX.Play();
                  StartCoroutine(DestroyThis());

                  if (isSheepPickUp == true) {
                        gameHandler_CycleStamina.AddEnergy(newStamina);
                        other.gameObject.GetComponent<PlayerMovePlatformer>().GetSheep();
                        //playerPowerupVFX.powerup();
                  }

				  if (isBatteryPickUp == true) {
                        //gameHandler_CycleStamina.AddBattery(newBatteries);
                        //other.gameObject.GetComponent<PlayerMovePlatformer>().GetSheep();
						Debug.Log("got battery: " + newBatteries);
						GameObject.FindWithTag("GameHandler").GetComponent<GameHandler_CycleStamina>().AddBatteryPublic(newBatteries);
                  }

            }
      }

      IEnumerator DestroyThis(){
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
      }

}
