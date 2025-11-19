using UnityEngine;

public class House_Grid_DayNight : MonoBehaviour
{

	public GameObject nightGrid;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nightGrid.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameHandler_CycleStamina.isNight == true)
		{
			nightGrid.SetActive(true);
		}
		else {
			nightGrid.SetActive(false);
		}
    }
}
