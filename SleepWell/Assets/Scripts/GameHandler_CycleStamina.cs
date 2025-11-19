using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameHandler_CycleStamina : MonoBehaviour
{
	bool isNight = false;
	bool isDay = true;
	public GameObject iconDay;
	public GameObject iconNight;
	public int nightLength = 90;
	public int dayLength = 900;

	public GameObject GoToSleepButton;

	public GameObject levelText;

	//Day / Night Timer:
	float theTimer = 0;
	int theTime = 0;
	public TMP_Text timerText;

	//Sleeping
	public bool isSleeping = false;
	public GameObject iconSleeping;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		iconSleeping.SetActive(false);
		SwitchToDay();

		GoToSleepButton.SetActive(true);
	}


	void Update()
	{
		//inputs to break out of dream -- maybe a skillcheck bar?
		if (Input.GetKeyDown("q"))
		{
			StopSleeping();
		}
	}

	void FixedUpdate()
	{
		theTimer += 0.01f;
		if (theTimer >= 1)
		{
			theTime++;
			theTimer = 0;
		}
		else if (isNight && theTime >= nightLength)
		{
			SwitchToDay();
			theTime = 0;
		}
		else if (isDay && theTime >= dayLength)
		{
			SwitchToNight();
			theTime = 0;
			isSleeping = false;
		}
		UpdatesTimerDisplay();
	}
/* WEEKDAY DISPLAY
		void UpdatesLevelDisplay()
	{
	if (SceneManagement.HouseMainArea){levelText.text = "Thursday";}
	else if (){levelText.text = "Friday";}
	else if (){levelText.text = "Saturday";}
	else if (){levelText.text = "Sunday";}
	}
*/
//DAY / NIGHT CYCLE:
	void UpdatesTimerDisplay()
	{
		if (isDay){timerText.text = "DAY";}
		if (isNight && theTime <= 10){timerText.text = "00:00 AM";}
		else if (theTime > 10 && theTime <= 20){timerText.text = "1:00 AM";}
		else if (theTime > 20 && theTime <= 30){timerText.text = "2:00 AM";}
		else if (theTime > 30 && theTime <= 40){timerText.text = "3:00 AM";}
		else if (theTime > 40 && theTime <= 50){timerText.text = "4:00 AM";}
		else if (theTime > 50 && theTime <= 60){timerText.text = "5:00 AM";}
		else if (theTime > 60 && theTime <= 70){timerText.text = "6:00 AM";}
		else if (theTime > 70 && theTime <= 80){timerText.text = "7:00 AM";}
		else if (theTime > 80 && theTime <= 90){timerText.text = "8:00 AM";}
	}

	void SwitchToDay()
	{
		isNight = false;
		iconDay.SetActive(true);
		iconNight.SetActive(false);
		levelText.SetActive(true);

		isSleeping = false;
	}

	void SwitchToNight()
	{
		isNight = true;
		iconNight.SetActive(true);
		iconDay.SetActive(false);
		levelText.SetActive(false);
		StartSleeping();
	}


//SLEEPING:
	public void StartSleeping()
	{
		isSleeping = true;
		iconSleeping.SetActive(true);
		theTimer = 0;
		if (isDay){SwitchToNight();}

		GoToSleepButton.SetActive(false);

		SceneManager.LoadScene ("Dreaming");
	}

	void StopSleeping()
	{
		isSleeping = false;
		iconSleeping.SetActive(false);
		GoToSleepButton.SetActive(true);
	}
}
