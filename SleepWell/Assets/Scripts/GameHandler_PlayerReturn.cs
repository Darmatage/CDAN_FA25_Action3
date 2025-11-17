using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler_PlayerReturn : MonoBehaviour {

       public static string lastMap = "";
       public static Vector2 lastDoorPosition;    // where the Player entered a door
       private string thisLevel;
       private Transform player;
	   private Transform cameraMain;

       void Start() {
              thisLevel = SceneManager.GetActiveScene().name;
              player = GameObject.FindWithTag("Player").GetComponent<Transform>();
			  cameraMain =  GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
              if (thisLevel == lastMap){
                     player.position = lastDoorPosition;
					 cameraMain.position = new Vector3(lastDoorPosition.x, lastDoorPosition.y, -10);
              }
       }
}