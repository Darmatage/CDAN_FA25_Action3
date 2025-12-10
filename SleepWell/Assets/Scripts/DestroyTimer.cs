using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyTimer : MonoBehaviour
{
	public float destroyTime = 0.4f;
    
    void Start()
    {
        StartCoroutine(DetroyMe());
    }

	IEnumerator DetroyMe()
	{
		yield return new WaitForSeconds(destroyTime);
		Destroy(gameObject);
	}

}
