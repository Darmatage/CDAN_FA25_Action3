using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameHandler_IntruderStatus : MonoBehaviour{
	public static int numIntruderOutside = 0;
	public static int numIntruderInside = 0;

//need a list of current intruder, their location
	string[] lookWindow = {"Outside_BedroomSouth", "Outside_BedroomWest", "Outside_LivingroomEast", "Outside_LivingroomNorth"};
	string[] peekRooms = {"Room_Bathroom", "Room_Kitchen", "Room_Workoutroom", "Room_Storagecloset", "Room_Guestbedroom", "Room_Garage"};
	public AudioSource[] peekRoomSounds;

//timers:
	public static float intruderOutsideTimer = 5;
	public float intruderOutsideTime = 5f; // to set in inspector
	public static float intruderInsideTimer = 5f;
	public float intruderInsideTime = 5f; // to set in inspector
	public static bool isOutside = false;
	public static bool isInside = false;

	public AudioSource intruderSpawnSFX;
	public AudioSource intruderEnteredSFX;

//player dealing with intruder:
	public bool playerAtTheWindowDoor = false; //is the player at the correct window or door?
	string currentSceneName;
	public static string intruderScene; //which scene has the intruder

    void Start()
    {
		currentSceneName = SceneManager.GetActiveScene().name;
		CheckPlayerLocation();

		//set intruder times to a quarter of the length of the night: 
		int nightPortion = GetComponent<GameHandler_CycleStamina>().nightLength / 5;
		intruderOutsideTimer = nightPortion;
		intruderOutsideTime = nightPortion;
		intruderInsideTimer = nightPortion;
		intruderInsideTime = nightPortion;
	}

//Timers for palyer doom OUTSIDE of pek-a-boo system at doors and wndows,
//isOutside and isInside are the intruder status
    void FixedUpdate()
    {
		//keyboard suystem for adding an intruder, just for testing:
		//if (Input.GetKeyDown("i") && currentSceneName=="House_Main")
		if (Input.GetKeyDown("i"))
		{
			AddIntruder();
			Debug.Log("I Hit letter [i]");
		}
		//NOTE; GmeHandler_CyleStamina DOES have a way to trigger the intruder in the first half of each night 

		//Global Intruder Timers for active intruder when the plyer is not looking in the right place: 
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
				intruderEnteredSFX.Play();
				Debug.Log("Intruder got into the house!");
				AddIntruderInside();
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

//next, decide randomly which room:
//decide in a night that it is time to have an intruder, from day-night cycle script

	public void AddIntruder()
	{
		numIntruderOutside++;
		isOutside = true;

		intruderSpawnSFX.Play();

		//set start location of intuder:
		int intruderLocationNum = Random.Range(0, lookWindow.Length);
		intruderScene = lookWindow[intruderLocationNum];
		//intruderScene = lookWindow[0];
		Debug.Log("enemy is in:" + intruderScene);

		CheckPlayerLocation();
	}

	public void AddIntruderInside()
	{
		intruderEnteredSFX.Play();

		//set start location of intuder:
		int intruderLocationNum = Random.Range(0, peekRooms.Length);
		intruderScene = peekRooms[intruderLocationNum];
		Debug.Log("enemy is in:" + intruderScene);

		AudioSource roomSoundCrash = peekRoomSounds[intruderLocationNum];
		roomSoundCrash.Play();

		CheckPlayerLocation();
	}


//if the player isat the correct window or door, pause the global timers and start the intruder prefab: 
	void CheckPlayerLocation()
	{
		//Debug.Log("checking player location in " + currentSceneName);
		if (intruderScene != null){
			if (currentSceneName == intruderScene)
			{	
				Debug.Log("MATCH: current scene = " + currentSceneName + ", intruder scene = " + intruderScene);
				playerAtTheWindowDoor = true;
				GameObject.FindWithTag("IntruderPlace").GetComponent<Intruder_SceneSystem>().StartIntruder();
			}
			else
			{
				playerAtTheWindowDoor = false;
			}
		}
		else
		{
			playerAtTheWindowDoor = false;
		}
	}

//catch the intruder, stop the global intruder timers: 
	public void CatchIntruder()
	{
		if (isOutside){
			numIntruderOutside--;
			isOutside = false;
			intruderOutsideTimer = intruderOutsideTime;
		}
		else if (isInside){
			numIntruderInside--;
			isInside = false;
			intruderInsideTimer = intruderInsideTime;
		}

		GameHandler_CycleStamina.beatIntruder = true;
		GameHandler_CycleStamina.hasIntruder = false;
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
			AddIntruderInside();
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
		GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().GetTimeStamp();
		SceneManager.LoadScene("SceneLose");
	}

}
