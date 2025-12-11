using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class Intruder_SceneSystem : MonoBehaviour{
	public Transform[] path1;
	public Transform[] path2;
	public Transform[] path3;
	public Transform[] path4;
	private Transform[] thePath;

	public GameObject intruder;
	private GameObject theIntruder;
	GameObject currentIntruder;

	private float timeBetweenIntrude = 5;
	private int pathCount = 4;
	public bool isIntruder = false;
	float intrudeScale = 1.5f;

// Day / Night Lighting:
	public Light2D lightFrontWall;
	public Light2D lightOutside;


	public bool isInteriorRoom = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

	void Update()
	{
		if (GameHandler_CycleStamina.isNight == true)
		{
			lightFrontWall.intensity = 0.2f;
			lightOutside.intensity = 0.1f;
		}
		else
		{
			lightFrontWall.intensity = 1f;
			lightOutside.intensity = 5f;
		}
	}

    /*
    void FixedUpdate()
    {
        if (isIntruder)
		{
			theTimer -= 0.01f;
			if (theTimer <= 0)
			{
				theTimer = 5;
				isIntruder = false;
				//send a messaga to the gamhandlrthat the enemy passed? ;
				Destroy (currentIntruder);
			}
		}
    }
	*/

//spawn an intruder, activated by the GameHandler_IntruderStatus:
	public void StartIntruder()
	{
		int startNum = 0;

		if (!isInteriorRoom){
			startNum = Random.Range(1,5);
		}
		else
		{
			startNum = Random.Range(1,3);
		}

		if (startNum == 1){thePath = path1;} 
		else if (startNum == 2){thePath = path2;}
		else if (startNum == 3){thePath = path3;}		
		else {thePath = path4;}

		theIntruder = Instantiate(intruder, thePath[0].position, Quaternion.identity);
		StartCoroutine(MoveIntruder(theIntruder));
		Debug.Log("intruder added to scene, located at " + thePath[0].position);
	}

	IEnumerator MoveIntruder(GameObject thisIntruder){
		Transform intT = thisIntruder.transform;
		SpriteRenderer intruderSprite = thisIntruder.GetComponentInChildren<SpriteRenderer>();
		yield return new WaitForSeconds(timeBetweenIntrude);

		if (thisIntruder != null){
			intT.position = thePath[1].position;
			intT.localScale = new Vector3(intT.localScale.x  * intrudeScale, intT.localScale.y * intrudeScale, intT.localScale.z);
			intruderSprite.sortingOrder = 25;
			yield return new WaitForSeconds(timeBetweenIntrude);
		}

		if (thisIntruder != null){
			intT.position = thePath[2].position;
			intT.localScale = new Vector3(intT.localScale.x  * intrudeScale, intT.localScale.y * intrudeScale, intT.localScale.z);
			intruderSprite.sortingOrder = 35;
			yield return new WaitForSeconds(timeBetweenIntrude);
		}

		if (thisIntruder != null){
			intT.transform.position = thePath[3].position;
			intT.localScale = new Vector3(intT.localScale.x  * intrudeScale, intT.localScale.y * intrudeScale, intT.localScale.z);
			intruderSprite.sortingOrder = 45;
			yield return new WaitForSeconds(timeBetweenIntrude);
		}

		//jump scare!
		if (thisIntruder != null){
			intT.localScale = new Vector3(intT.localScale.x  * intrudeScale, intT.localScale.y * intrudeScale, intT.localScale.z);
			GameObject.FindWithTag("GameHandler").GetComponent<GameHandler_IntruderStatus>().IntruderFinishedStage();
			Destroy (thisIntruder);
		}
	}

}
