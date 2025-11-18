using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameHandler_IntruderStatus : MonoBehaviour
{

	public static int numIntruderOutside = 0;
	public static int numIntruderInside = 0;

//need a list of current intruders, their location

	string[] peekRooms = {"bathoom", "kitchen", "workoutroom", "storagecloset", "guestbedoom", "garage"};
	string[] lookWindow = {"Outside_BedroomSouth", "Outside_BedroomWest", "Outside_LivingroomNorth", "Outside_LivingroomEast"};

//timers:
	float intruderOutsideTimer = 30f;
	public float intruderOutsideTime = 30f;
	float intruderInsideTimer = 40f;
	public float intruderInsideTime = 40f;
	public static bool isOutside = false;
	public static bool isInside = false;

	public bool playerAtTheWindowDoor = false;
	string currentSceneName;
	string intruderScene;

    void Start()
    {
		currentSceneName = SceneManager.GetActiveScene().name;
		CheckPlayerLocation();
	}

//Timers for palyer doom OUTSIDE of pek-a-boo system at doors and wndows,
//isOutside and isInside are the intruder status
    void FixedUpdate()
    {
		//Just for testing:
		if (Input.GetKeyDown("i"))
		{
			AddIntruder();
			//Debug.Log("1. Hit letter [i]");
		}

		//timers for active intruder when the plyer is not at the right place: 
		if (isOutside && !playerAtTheWindowDoor)
		{
			intruderOutsideTimer -= 0.01f;
			if (intruderOutsideTimer <= 0)
			{
				intruderOutsideTimer = intruderOutsideTime;
				isOutside = false;
				isInside = true;
				numIntruderOutside--;
				numIntruderInside++;
			}
		}
		else if (isInside && !playerAtTheWindowDoor){
			intruderInsideTimer -= 0.01f;
			if (intruderInsideTimer <= 0){
				intruderInsideTimer = intruderInsideTime;
				isOutside = false;
				isInside = false;
				playerLoses();
			} 
		}        
    }

//next, decide randomly which room, and use scenemanagement to id if we are in that room,
//and if so, spawn an enemy
//decide in a night that it is time to have an intruder, from day-night cycle sript



	public void AddIntruder()
	{
		numIntruderOutside++;
		isOutside = true;

		//set start location of intuder:
		int intruderLocationNum = Random.Range(0, lookWindow.Length);
		intruderScene = lookWindow[intruderLocationNum];
		//intruderScene = lookWindow[0];
		Debug.Log("enemy is in:" + intruderScene);

		CheckPlayerLocation();
	}

	void CheckPlayerLocation()
	{
		if (currentSceneName != null){
			if (currentSceneName == intruderScene)
			{
				playerAtTheWindowDoor = true;
				GameObject.FindWithTag("IntruderPlace").GetComponent<Intruder_SceneSystem>().StartIntruder();
			}
			else
			{
				playerAtTheWindowDoor = false;
			}
		}
	}

	public void CatchIntruder(string location)
	{
		if (location == "Outside"){
			numIntruderOutside--;
			isOutside = false;
			intruderOutsideTimer = intruderOutsideTime;
		}
		else if (location == "Intside"){
			numIntruderInside--;
			isInside = false;
			intruderInsideTimer = intruderInsideTime;
		}
	}

	public void IntruderFinishedStage()
	{
		if (isOutside)
		{
			numIntruderOutside--;
			isOutside = false;
			numIntruderInside++;
			isInside = true;
			Debug.Log("Intruder got into the house!");
		} 
		else
		{
			numIntruderInside--;
			isInside = false;
			playerLoses();
		}
	}



	void playerLoses()
	{
		Debug.Log("YOU LOSE");
	}

}
