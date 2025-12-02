using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Dreaming_EvilRain : MonoBehaviour {

	public GameObject[] evilRains;
	public Transform rainRangeUpperLeft;
	public Transform rainRangeLowerRight;
	bool isRaining = false;
	public float timeToNextRain = 0.3f;
	float rainTimer=0;

	//follow the player:
	private GameObject target;
	public float rainSpeed = 4.0f;
	public float heightAbovePlayer = 10f;

	void Start(){
		StartCoroutine(StartRain());
		//follow the player
		target = GameObject.FindWithTag("Player");
	}

	void FixedUpdate () {
		//follow player, at offset
		Vector2 playerPosOffset = new Vector2(target.transform.position.x, target.transform.position.y + heightAbovePlayer);
		Vector2 pos = Vector2.Lerp ((Vector2)transform.position, playerPosOffset, rainSpeed * Time.fixedDeltaTime);
		transform.position = new Vector3 (pos.x, pos.y, transform.position.z);

		//rain timer:
		rainTimer += 0.01f;
		if (rainTimer >= timeToNextRain){
			rainTimer = 0;
			rainEvil();
		}
	}

	//spawn rain at random location
	void rainEvil(){
		//rndomize rAin location:
		float posX = Random.Range(rainRangeUpperLeft.position.x,rainRangeLowerRight.position.x);
		float posY = Random.Range(rainRangeUpperLeft.position.y,rainRangeLowerRight.position.y);
		Vector2 currentPos = new Vector2(posX, posY);
		//rndomize rain object:
		int rainNum = Random.Range(0, evilRains.Length);
		GameObject currentRain = evilRains[rainNum];
		
		Instantiate (currentRain, currentPos, Quaternion.identity); 
	} 

	//delay evil rain start
	IEnumerator StartRain()
	{
		yield return new WaitForSeconds(2f);
		isRaining = true;
	}


}
