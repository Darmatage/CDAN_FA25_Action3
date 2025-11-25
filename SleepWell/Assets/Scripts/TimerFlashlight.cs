
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerFlashlight : MonoBehaviour {
       public float timerMax = 10f;       //set the number of seconds here
       private static float theTimer = 0f;
       public bool doTheThing = false;

       public Image timerDisplay;

	   bool isOn = false;
	   bool isFlashlightScene = false;

	   private FlashlightControl flashlight;
	   public GameObject flashlightButton;

	void Start(){
           
		//check if this is a flashlight-using scene:
		string sceneName = SceneManager.GetActiveScene().name;
		if (
			(sceneName != "House_Main")
			&& (sceneName != "Dreaming")
			&& (sceneName != "Accounting")
		){isFlashlightScene = true;}
		else {isFlashlightScene = false;}

		if (isFlashlightScene){
		   	flashlight = GameObject.FindWithTag("Flashlight").GetComponent<FlashlightControl>();
			flashlightButton.SetActive(true);
			timerDisplay.gameObject.SetActive(true);
			theTimer = timerMax;
		}
		else
		{
			flashlightButton.SetActive(false);
		}
	}

      void Update(){
            //test functionality. Normally set=true by external script.
            if (Input.GetKeyDown("f")){
				GetBattery();
            }
      }

       void FixedUpdate(){
            if (doTheThing == true){
                  theTimer -= 0.02f;
                  Debug.Log("Battery: " + theTimer);
                  timerDisplay.gameObject.SetActive(true);
                  timerDisplay.fillAmount = theTimer / timerMax;

                  if (theTimer <= 0){
                        //theTimer = timerMax;
                        Debug.Log("I do the thing!");       //can be replaced with the desired commands
                        doTheThing = false;
						isOn = false;
						flashlight.hasBatteryOn = false;
                    }
              }
       }

	//public function to be accessed by other scripts to activate the timer.
	public void TimeToDoTheThing(){
		if (!isOn && (theTimer >= 0.1f)&&(GameHandler_CycleStamina.isNight == true)){
			doTheThing = true;
			isOn = true;
			flashlight.hasBatteryOn = true;
			//other commands when turning on timer can go here.
		}
		else {
			doTheThing = false;
			isOn = false;
			flashlight.hasBatteryOn = false;
			//other commands when turning off timer can go here.
		}
	}

	public void GetBattery()
	{
		theTimer = timerMax;
		timerDisplay.fillAmount = theTimer / timerMax;
	}
}