using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class FlashlightControl : MonoBehaviour
{
	public Color redColor = Color.red;

	void Update ()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
		transform.position = mousePosition; 
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

}

   
