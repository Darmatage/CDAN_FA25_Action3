using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door_Interactive : MonoBehaviour{

        public string NextLevel = "MainMenu";
        public GameObject msgPressE;
        public bool canPressE =false;

//return to map location #1:
  		private string thisLevel;
		private Vector2 doorReturnPos;       // load with location for player when they return
		public float offsetX = 0f;        // distance to left or right of the door for player to spawn
		public float offsetY = 0f;        // distance above or below the door for player to spawn

       void Start(){
              msgPressE.SetActive(false);
//return to map location #2:
			  thisLevel = SceneManager.GetActiveScene().name;
              doorReturnPos = new Vector2((transform.position.x + offsetX), (transform.position.y + offsetY)); 
        }

       void Update(){
              if ((canPressE == true) && (Input.GetKeyDown(KeyCode.E))){
                     EnterDoor();
              }
        }

        void OnTriggerEnter2D(Collider2D other){
              if (other.gameObject.tag == "Player"){ ;
                     msgPressE.SetActive(true);
                     canPressE =true;
              }
        }

        void OnTriggerExit2D(Collider2D other){
              if (other.gameObject.tag == "Player"){
                     msgPressE.SetActive(false);
                     canPressE = false;
              }
        }

      public void EnterDoor(){
//return to map location #3:
			GameHandler_PlayerReturn.lastDoorPosition = doorReturnPos;
			GameHandler_PlayerReturn.lastMap = thisLevel; 

            SceneManager.LoadScene (NextLevel);
      }

} 