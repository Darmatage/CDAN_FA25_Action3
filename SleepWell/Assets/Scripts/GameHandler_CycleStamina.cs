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
	public int nightLength = 60;

	public GameObject GoToSleepButton;

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
		UpdatesTimerDisplay();
	}

//DAY / NIGHT CYCLE:
	void UpdatesTimerDisplay()
	{
		if (isDay){timerText.text = "DAY";}
		timerText.text = "TIME: " + theTime;
	}

	void SwitchToDay()
	{
		isNight = false;
		iconDay.SetActive(true);
		iconNight.SetActive(false);
	}

	void SwitchToNight()
	{
		isNight = true;
		iconNight.SetActive(true);
		iconDay.SetActive(false);
		StartSleeping();
	}


//SLEEPING:
	public void StartSleeping()
	{
		isSleeping = true;
		iconSleeping.SetActive(true);
		SwitchToNight();
		theTime = 0;
	}

	void StopSleeping()
	{
		isSleeping = false;
		iconSleeping.SetActive(false);
	}


}
