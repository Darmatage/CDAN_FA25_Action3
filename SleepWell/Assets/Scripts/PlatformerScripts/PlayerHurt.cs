using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerHurt: MonoBehaviour {

      Animator anim;
      //public Rigidbody2D rb2D;
	  public SpriteRenderer playerArt;
	  public Color colorHurt;
	  public Color colorNormal;

      void Start(){
           anim = gameObject.GetComponentInChildren<Animator>();
           //rb2D = transform.GetComponent<Rigidbody2D>(); 
			playerArt = GetComponentInChildren<SpriteRenderer>();          
      }

      public void PlayerHit(){
            //anim.SetTrigger ("GetHurt");
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