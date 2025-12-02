using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

      private GameObject target;
      public float camSpeed = 4.0f;
	  public float verticalOffset = 0;
	  float verticalOffsetMain;

      void Start(){
            target = GameObject.FindWithTag("Player");
			verticalOffsetMain = verticalOffset;
      }

      void FixedUpdate () {
            
			//Vector2 pos = Vector2.Lerp ((Vector2)transform.position, (Vector2)target.transform.position, camSpeed * Time.fixedDeltaTime);
            Vector2 playerPosWithOffset = new Vector2(target.transform.position.x, target.transform.position.y + verticalOffset); 
			Vector2 pos = Vector2.Lerp ((Vector2)transform.position, playerPosWithOffset, camSpeed * Time.fixedDeltaTime);

			transform.position = new Vector3 (pos.x, pos.y, transform.position.z);
      }

	public void CameraCenter()
	{
		verticalOffset = 0;
		StartCoroutine(RetunToNormal());
	}

	IEnumerator RetunToNormal()
	{
		yield return new WaitForSeconds(0.75f);
		verticalOffset = verticalOffsetMain;
	}

}
