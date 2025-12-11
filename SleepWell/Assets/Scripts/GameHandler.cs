using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour{

		//public static int playerStat1;
		// public TMP_Text textGameObject;

        void Start () { 
			//UpdateScore ();
		}

        void FixedUpdate(){

                // Stat tester:
                //if (Input.GetKey("p")){
                //       Debug.Log("Player Stat = " + playerStat1);
                //}
        }

        // void UpdateScore () {
        //        textGameObject.text = "Score: " + score; }

        public void StartGame(){
			ResetAllStats();
			SceneManager.LoadScene("House_Main");
        }

        public void OpenCredits(){
			SceneManager.LoadScene("Credits");
        }

        public void RestartGame(){
			Time.timeScale = 1f;
			SceneManager.LoadScene("MainMenu");
        }

        public void QuitGame(){
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
        }


		public void ResetAllStats()
	{
		GameHandler_CycleStamina.isNight = false;
		GameHandler_CycleStamina.theTime = 0;
		GameHandler_CycleStamina.dayTime= 0;
		GameHandler_CycleStamina.nightTime= 0;
		GameHandler_CycleStamina.dayOfWeek= 1;
		GameHandler_CycleStamina.isSleeping = false;
		GameHandler_CycleStamina.isWorking = false;
		GameHandler_CycleStamina.hasIntruder = false;
		GameHandler_CycleStamina.playerEnergy = 100f;

		GameHandler_IntruderStatus.isOutside = false;
		GameHandler_IntruderStatus.isInside = false;
		GameHandler_IntruderStatus.intruderScene = null;

		GameHandler_PlayerReturn.lastMap = "";
	}


}


