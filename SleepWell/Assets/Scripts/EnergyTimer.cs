using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnergyTimer : MonoBehaviour {
       public float timerMax = 10f;       //set the number of seconds here
       private float theTimer = 0f;
       public bool doTheThing = false;

       public Image timerBarDisplay;

      void Start(){
           timerBarDisplay.gameObject.SetActive(true);
           theTimer = timerMax;
      }

      void Update(){
            //test functionality. Normally set=true by external script.
            if (Input.GetKeyDown("8")){
                  doTheThing = true;
            }
      }

       void FixedUpdate(){
            if (doTheThing == true){
                  theTimer -= 0.001f;
                  Debug.Log("time: " + theTimer);
                  timerBarDisplay.gameObject.SetActive(true);
                  timerBarDisplay.fillAmount = theTimer / timerMax;

                  if (theTimer <= 0){
                        theTimer = timerMax;
                        Debug.Log("I do the thing!");       //can be replaced with the desired commands
                        doTheThing = false;
                    }
              }
       }

       //public function to be accessed by other scripts to activate the timer.
       public void TimeToDoTheThing(){
              doTheThing = true;
              //other commands when turnign on timer can go here.
       }
}
