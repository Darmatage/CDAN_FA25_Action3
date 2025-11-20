using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameHandler_CycleStamina : MonoBehaviour
{
	public static bool isNight = false;
	public GameObject iconDay;
	public GameObject iconNight;
	public int nightLength = 90;
	public int dayLength = 60;

	public GameObject GoToSleepButton;

	//Day / Night Timer:
	float theTimer = 0;
	public static int theTime = 0;
	public static int dayTime= 0;
	public static int nightTime= 0;
	public TMP_Text timerText;
	public TMP_Text text_DayOfWeek;

	public static int dayOfWeek= 1;
	public GameObject dayOfWeekBG;

	//Sleeping
	public static bool isSleeping = false;
	public GameObject iconSleeping;


	//bed stuff
	public bool atBed = false;
	private string thisLevel;
	private Vector2 bedReturnPos;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		thisLevel = SceneManager.GetActiveScene().name;
		if (GameObject.FindWithTag("Bed")!=null){
			Transform theBed = GameObject.FindWithTag("Bed").GetComponent<Transform>();
			bedReturnPos = new Vector2((theBed.position.x), (theBed.position.y)); 
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
	}


	void Update()
	{
		//inputs to break out of dream -- maybe a skillcheck bar?
		if (Input.GetKeyDown("q") && isSleeping)
		{
			StopSleeping();
		}

		if (Input.GetKeyDown("s") && !isSleeping && atBed)
		{
			StartSleeping();
		}

		if (isNight && atBed){
			GoToSleepButton.SetActive(true);
		}
		else
		{
			GoToSleepButton.SetActive(false);
		} 

	}

	void FixedUpdate()
	{
		theTimer += 0.05f;
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
	}

	void SwitchToNight()
	{
		isNight = true;
		iconNight.SetActive(true);
		iconDay.SetActive(false);
		dayOfWeekBG.SetActive(false);
		
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

	void StopSleeping()
	{
		isSleeping = false;
		iconSleeping.SetActive(false);
		if (isNight){
			GoToSleepButton.SetActive(true);
		}
		SceneManager.LoadScene ("House_Main");
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