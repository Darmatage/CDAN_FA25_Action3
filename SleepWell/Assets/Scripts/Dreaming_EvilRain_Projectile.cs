using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dreaming_EvilRain_Projectile : MonoBehaviour
{
	public int damage = 1;
	public GameObject rainArt;
	public AudioSource hitplayerSFX;
	Animator anim;
	public GameObject boomVFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		anim = GetComponentInChildren<Animator>();
        StartCoroutine(DestroyMe(5f));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
		{
			//Debug.Log("Rain Hurt the player");
			hitplayerSFX.Play();
			GetComponent<BoxCollider2D>().enabled = false;
			rainArt.SetActive(false);
			Instantiate(boomVFX, transform.position, Quaternion.identity); 
			GameObject.FindWithTag("GameHandler").GetComponent<GameHandler_PlayerGetHurt>().PlayerGetHurt(damage);
			other.gameObject.GetComponent<PlayerHurt>().PlayerHit();

			anim.SetBool("Boom", true);
			StartCoroutine(DestroyMe(0.5f));
		}

//LayerMask.LayerToName

//if it hits the ground before the player,does not hurt the plyer:
		else if (other.gameObject.layer == 6)
		{
			GetComponent<BoxCollider2D>().enabled = false;
			//rainArt.SetActive(false);

			//anim.SetBool("Boom", true);
			//StartCoroutine(DestroyMe(0.5f));
		} 

    }


	IEnumerator DestroyMe(float destroyTime)
	{
		
		yield return new WaitForSeconds(destroyTime);
		//rainArt.SetActive(false);
		Destroy(gameObject);
	}

}
