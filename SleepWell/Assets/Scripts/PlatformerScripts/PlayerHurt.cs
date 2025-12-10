using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerHurt: MonoBehaviour {

	Animator anim;
	//public Rigidbody2D rb2D;
	SpriteRenderer playerArt;
	public Color colorHurt;
	public Color colorNormal;

	CameraShake cameraShake;

	void Start(){
		anim = gameObject.GetComponentInChildren<Animator>();
		//rb2D = transform.GetComponent<Rigidbody2D>(); 
		playerArt = GetComponentInChildren<SpriteRenderer>();   
		cameraShake = GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>();       
	}

      public void PlayerHit(){
            //anim.SetTrigger ("GetHurt");
			cameraShake.ShakeCamera(0.15f, 0.3f);
			StartCoroutine(PlayerColorChange());
      }

/*
      public void playerDead(){
            rb2D.isKinematic = true;
            //anim.SetTrigger ("Dead");
      }
*/

	IEnumerator PlayerColorChange()
	{
		playerArt.color = colorHurt;
		yield return new WaitForSeconds (0.3f);
		playerArt.color = colorNormal;
	}

} 