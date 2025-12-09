using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door_MoveOutOfBounds : MonoBehaviour{

      public string NextLevel = "MainMenu";

	  void Update()
	{
		if (Input.GetKeyDown("e"))
		{
			StopPeeking();
		}
	}

      public void OnTriggerEnter2D(Collider2D other){
            if (other.gameObject.tag == "Player"){
                  StopPeeking();
            }
      }

	  public void StopPeeking()
	{
		SceneManager.LoadScene (NextLevel);
	}

} 