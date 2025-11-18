using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameHandler_IntruderStatus : MonoBehaviour
{

	public int numIntruderOutside = 0;
	public int numIntruderInside = 0;

//need a list of current intruders, their location

	//public string[] peekRooms = {bathoom, kitchen, workoutroom, storagecloset, guestbedoom, garage};
	//public string[] lookWindow = {bedroomSouth, bedroomWest, livingroomNorth, livingroomEast};

//timers:
	float intruderOutsideTimer = 5f;
	public float intruderOutsideTime = 5f;
	float intruderInsideTimer = 10f;
	public float intruderInsideTime = 10f;
	bool isOutside = false;
	bool isInside = false;

    void Start()
    {
        
    }

//Timers
    void FixedUpdate()
    {
		if (isOutside)
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
			else if (isInside)
			{
				intruderInsideTimer -= 0.01f;
				if (intruderInsideTimer <= 0){
					intruderInsideTimer = intruderInsideTime;
					isOutside = false;
					isInside = false;
					//player loses
				} 
			}
		}
        
    }


//decide in a night that it is time to have an intruder, from day-night cycle sript

	public void AddIntruder()
	{
		numIntruderOutside++;
		isOutside = true;
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

//next, decide randomly which room, and use scenemanagement to id if we are in that room,
//and if so, spawn an enemy


}
