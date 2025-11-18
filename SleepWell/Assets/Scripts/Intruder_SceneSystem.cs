using UnityEngine;

public class Intruder_System : MonoBehaviour
{

	public Transform[] path1;
	public Transform[] path2;
	public Transform[] path3;
	public Transform[] path4;

	public GameObject intruder;
	GameObject currentIntruder;

	private float theTimer = 5;
	private int pathCount = 4;
	public bool isIntruder = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
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



}
