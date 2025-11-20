using UnityEngine;

public class Intruder_Character : MonoBehaviour
{
	public GameObject[] intruders;
	int intruderNum = 0;

    void Start()
    {
    	ChooseIntruder();
    }

    void ChooseIntruder()
    {
        intruderNum = Random.Range(0, intruders.Length);
		for (int i=0; i < intruders.Length; i++)
		{
			if (i == intruderNum){
				intruders[i].SetActive(true);
				}
			else {
				intruders[i].SetActive(false);
				}
		}
    }
}
