using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameHandler_CycleStamina : MonoBehaviour
{
	//public static int playerStamina = 100;
	public static bool isNight = false;
	public GameObject iconDay;
	public GameObject iconNight;
	public int nightLength = 90;
	public int dayLength = 40;

//Dreaming and Working Scene access buttons:
	public GameObject GoToSleepButton;
	public GameObject GoToWorkButton;


	//Day / Night Timer:
	public float theTimer = 0;
	public static int theTime = 0;
	public static int dayTime= 0;
	public static int nightTime= 0;
	public TMP_Text timerText;
	public TMP_Text text_DayOfWeek;

	public static int dayOfWeek= 1;
	public GameObject dayOfWeekBG;


	//sleeping/ bed stuff
	public static bool isSleeping = false;
	public GameObject iconSleeping;
	public bool atBed = false;
	private string thisLevel;
	private Vector2 bedReturnPos;

	//working/ desk stuff
	public static bool isWorking = false;
	public bool atDesk = false;
	private Vector2 deskReturnPos;

	//intruder stuff:
	private int intruderSpawnTime;
	public static bool hasIntruder = false;

//player energy stuff:
	public static float playerEnergy = 100f;
	public float playerEnergyMax = 100f;
	bool canLoseEnergy = true; //set false while sleeping! 
	float theEnergyTimer = 1;
	public Image energyBarDisplay;
	float dayEnergyLossRate = 0.005f; // every five seconds
	float nightEnergyLossRate = 0.01f; // every one second
	float energyLossRate = 0.006f;

//flashlight:
	public GameObject flashLightTimer;

	void Start()
	{

		thisLevel = SceneManager.GetActiveScene().name;
		//return to bed system:
		if (GameObject.FindWithTag("Bed")!=null){
			Transform theBed = GameObject.FindWithTag("Bed").GetComponent<Transform>();
			bedReturnPos = new Vector2((theBed.position.x), (theBed.position.y)); 
		}

		//return to desk system:
		if (GameObject.FindWithTag("Desk")!=null){
			Transform theDesk = GameObject.FindWithTag("Desk").GetComponent<Transform>();
			deskReturnPos = new Vector2((theDesk.position.x), (theDesk.position.y)); 
		}


		//don't lose energy in the Dreaming scene
		if (thisLevel =="Dreaming"){
			canLoseEnergy = false; 
		}
		else
		{
			canLoseEnergy = true;
		}

		if (!isNight){
			iconSleeping.SetActive(false);
			iconNight.SetActive(false);
			iconDay.SetActive(true);
			GoToSleepButton.SetActive(false);
		}
		else
		{
			iconSleeping.SetActive(true);
			iconNight.SetActive(true);
			iconDay.SetActive(false);
			GoToSleepButton.SetActive(false); // ned to be atBed -- see Update()
		} 

		//player energy:
		//playerEnergy = playerEnergyMax;
		energyBarDisplay.gameObject.SetActive(true);
		DisplayEnergy();

		//Flashlight display:
		if (!isNight || isWorking || isSleeping || thisLevel =="House_Main")
		{
			flashLightTimer.SetActive(false);
		}
		else
		{
			flashLightTimer.SetActive(true);
		}

	}


	void Update()
	{
		//Test inputs to break out of dream -- maybe a wackmolsystem? skillcheck bar?
		//SLEEP
		if (Input.GetKeyDown("q") && isSleeping){StopSleeping();}
		if (Input.GetKeyDown("s") && !isSleeping && atBed && isNight){StartSleeping();}
		if (isNight && atBed){GoToSleepButton.SetActive(true);}
		else{GoToSleepButton.SetActive(false);} 

		//WORK
		if (Input.GetKeyDown("l") && isWorking){StopWorking();}
		if (Input.GetKeyDown("k") && !isWorking && atDesk && !isNight){StartWorking();}
		if (!isNight && atDesk){GoToWorkButton.SetActive(true);}
		else{GoToWorkButton.SetActive(false);} 

	}

	void FixedUpdate()
	{
		//player slowly loses energy
			if (canLoseEnergy){
				theEnergyTimer -= energyLossRate;
				if (theEnergyTimer <= 0){
					theEnergyTimer = 1;
					playerEnergy --;
					DisplayEnergy();
					//Debug.Log("Player Energy = " + playerEnergy);
				}
				if (playerEnergy <= 0){
					playerEnergy = 0;
					Debug.Log("Player lost all energy-- you should have slept!");
				}
			}

		//day night timer:
		theTimer += 0.01f;
		if (theTimer >= 1)
		{
			theTime++;
			theTimer = 0;
		}

		if (!isNight)
		{
			dayTime = (theTime / (dayLength/12));
		}
		else 
		{
			nightTime = (theTime /(nightLength/12));
		}

		//trigger an intruder during the night!
		if (!hasIntruder && isNight && theTime >= intruderSpawnTime)
		{
			hasIntruder = true;
			GetComponent<GameHandler_IntruderStatus>().AddIntruder();
		}
		
		//switch day and night:
		if (isNight && theTime >= nightLength)
		{
			SwitchToDay();
			theTime = 0;
			dayTime = 0;
			isSleeping = false;
		}
		else if (!isNight && theTime >= dayLength)
		{
			SwitchToNight();
			theTime = 0;
			nightTime = 0;
			GoToSleepButton.SetActive(true);
		}
		UpdatesTimerDisplay();

		if (isSleeping)
		{
			GoToSleepButton.SetActive(false);
		}
	}

//DAY / NIGHT CYCLE:
	void UpdatesTimerDisplay()
	{
		
		if (!isNight){ 
			//7am - 6pm
			if (dayTime < 5) {timerText.text = "" + (dayTime + 7) + ":00 AM";}
			else if (dayTime == 5){timerText.text = "" + (dayTime + 7) + ":00 PM";}
			else {timerText.text = "" + (dayTime - 5) + ":00 PM";}

		}
		if (isNight){ 
			//7pm - 6am
			//if (nightTime < 5) {timerText.text = "" + (nightTime + 7) + ":00 PM";}
			//else {timerText.text = "" + (nightTime + 7) + ":00 AM";}
			if (nightTime < 5) {timerText.text = "" + (nightTime + 7) + ":00 PM";}
			else if (nightTime == 5){timerText.text = "" + (nightTime + 7) + ":00 AM";}
			else {timerText.text = "" + (nightTime - 5) + ":00 AM";}
		}

		//WEEKDAY DISPLAY
		if (dayOfWeek ==1){text_DayOfWeek.text = "Sunday";}
		else if (dayOfWeek ==2){text_DayOfWeek.text = "Monday";}
		else if (dayOfWeek ==3){text_DayOfWeek.text = "Tuesday";}
		else if (dayOfWeek ==4){text_DayOfWeek.text = "Wednesday";}
		else if (dayOfWeek ==5){text_DayOfWeek.text = "Thursday";}
		else if (dayOfWeek ==6){text_DayOfWeek.text = "Friday";}
		else if (dayOfWeek ==7){text_DayOfWeek.text = "Saturday";}

	}

	void SwitchToDay()
	{
		
		dayOfWeek++;
		if (dayOfWeek >= 8)
		{
			dayOfWeek=1;
		}
		isNight = false;
		iconDay.SetActive(true);
		iconNight.SetActive(false);
		dayOfWeekBG.SetActive(true);

		if (isSleeping){
			StopSleeping();
		}

		hasIntruder = false;
		energyLossRate = dayEnergyLossRate;
		flashLightTimer.SetActive(false);
	}

	void SwitchToNight()
	{
		
		isNight = true;
		iconNight.SetActive(true);
		iconDay.SetActive(false);
		dayOfWeekBG.SetActive(false);

		if (isWorking){
			StopWorking();
		}

		intruderSpawnTime = Random.Range(0, nightLength/2); //spawn the player in the first half of the night
		energyLossRate = nightEnergyLossRate;
		flashLightTimer.SetActive(true);
		
	}


//SLEEPING:
	public void StartSleeping()
	{
		isSleeping = true;
		iconSleeping.SetActive(true);
		//theTimer = 0;
		//if (isDay){SwitchToNight();}

		GoToSleepButton.SetActive(false);

		GameHandler_PlayerReturn.lastDoorPosition = bedReturnPos;
		GameHandler_PlayerReturn.lastMap = thisLevel; 

		SceneManager.LoadScene ("Dreaming");
	}

	public void StopSleeping()
	{
		isSleeping = false;
		iconSleeping.SetActive(false);
		if (isNight){
			GoToSleepButton.SetActive(true);
		}
		SceneManager.LoadScene ("House_Main");
	}

//SLEEPING:
	public void StartWorking()
	{
		isWorking = true;
		//iconSleeping.SetActive(true);
		//theTimer = 0;
		//if (isDay){SwitchToNight();}

		GoToWorkButton.SetActive(false);

		GameHandler_PlayerReturn.lastDoorPosition = deskReturnPos;
		GameHandler_PlayerReturn.lastMap = thisLevel; 

		SceneManager.LoadScene ("Working");
	}

	public void StopWorking()
	{
		isWorking = false;
		//iconSleeping.SetActive(false);
		if (!isNight){
			GoToWorkButton.SetActive(true);
		}
		SceneManager.LoadScene ("House_Main");
	}

	public void AddEnergy(float energy)
	{
		playerEnergy += energy;
		if (playerEnergy > playerEnergyMax)
		{
			playerEnergy = playerEnergyMax;
		}
		DisplayEnergy();
	}
	void DisplayEnergy()
	{
		energyBarDisplay.fillAmount = playerEnergy / playerEnergyMax;
	}

	public void PlayerEnergyLose()
	{
		SceneManager.LoadScene("EndLose_NoStamina");
	} 


	public void AddBatteryPublic(float newBatteries1)
	{
		StartCoroutine(AddBattery(newBatteries1));
	}

	IEnumerator AddBattery(float newBatteries2)
	{
		flashLightTimer.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		flashLightTimer.GetComponent<TimerFlashlight>().GetBattery(newBatteries2);
		yield return new WaitForSeconds(0.5f);
		flashLightTimer.SetActive(false);
	}

}



		/*	
			if (theTime <= 10){timerText.text = "10:00 PM";}
			else if (theTime > 10 && theTime <= 20){timerText.text = "11:00 PM";}
			else if (theTime > 20 && theTime <= 30){timerText.text = "12:00 AM";}
			else if (theTime > 30 && theTime <= 40){timerText.text = "1:00 AM";}
			else if (theTime > 40 && theTime <= 50){timerText.text = "2:00 AM";}
			else if (theTime > 50 && theTime <= 60){timerText.text = "3:00 AM";}
			else if (theTime > 60 && theTime <= 70){timerText.text = "4:00 AM";}
			else if (theTime > 70 && theTime <= 80){timerText.text = "5:00 AM";}
			else if (theTime > 80 && theTime <= 90){timerText.text = "6:00 AM";}
			*/