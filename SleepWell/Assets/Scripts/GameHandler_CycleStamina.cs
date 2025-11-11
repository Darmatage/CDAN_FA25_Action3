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
	public GameObject iconDay;
	public GameObject iconNight;
	public int dayLength = 10;
	public int nightLength = 20;

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

		if (!isNight && theTime >= dayLength)
		{
			SwitchToNight();
			theTime = 0;
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
	void StartSleeping()
	{
		isSleeping = true;
		iconSleeping.SetActive(true);
	}

	void StopSleeping()
	{
		isSleeping = false;
		iconSleeping.SetActive(false);
	}


}
