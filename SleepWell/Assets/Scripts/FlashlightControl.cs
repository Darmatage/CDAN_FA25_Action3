using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal; // Important namespace for Light2D


public class FlashlightControl : MonoBehaviour
{
	
	public Color redColor = Color.red;
	public Light2D[] lights; 
	private float[] lightFullIntensity= {0,0,0,0}; 

//get value from the UI battery control:
	public bool hasBatteryOn = false;

	void Start()
	{
		//capture the value of the intensity for each light
		for (int i = 0; i < lights.Length; i++)
		{
			lightFullIntensity[i] = lights[i].intensity;
		}
	}

	void Update ()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
		transform.position = mousePosition; 

		if (hasBatteryOn && (GameHandler_CycleStamina.isNight == true))
		{
			FlashLightOn();
		}
		else
		{
			FlashLightOff();
		} 
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("flahlight hit: " + other.gameObject.name);
		if (other.gameObject.tag == "Intruder")
		{
			other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
			GameObject.FindWithTag("GameHandler").GetComponent<GameHandler_IntruderStatus>().CatchIntruder();
			StartCoroutine(RemoveIntruder(other.gameObject));
		}
	}

	IEnumerator RemoveIntruder(GameObject intruder)
	{
		intruder.GetComponentInChildren<SpriteRenderer>().color = redColor;
		yield return new WaitForSeconds(1f);
		Destroy(intruder);
	}

	public void FlashLightOn()
	{
		Debug.Log("turned on flashlight");
		GetComponent<Collider2D>().enabled = true;
		for (int i = 0; i < lights.Length; i++)
		{
			lights[i].intensity = lightFullIntensity[i];
		}
	}
	public void FlashLightOff()
	{
		Debug.Log("turned off flashlight");
		GetComponent<Collider2D>().enabled = false;
		for (int i = 0; i < lights.Length; i++)
		{
			lights[i].intensity = 0;
		}
	}

}

   
