using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameHandler_PlayerGetHurt : MonoBehaviour
{

	public  int playerHealth = 10;
    public TMP_Text textGameObject;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayHealth();
    }

    public void PlayerGetHurt(int damage)
    {
        playerHealth -= damage;
		DisplayHealth();
        if (playerHealth <= 0)
		{
			Debug.Log("You woke up from getting hurt");
		}
    }


	public void DisplayHealth()
	{
		textGameObject.text = "Dream Health: " + playerHealth;
	}
}
