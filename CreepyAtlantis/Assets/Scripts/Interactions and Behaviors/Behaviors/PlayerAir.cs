using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAir : MonoBehaviour {

    public float airTankPercent;

    private AirMeter airBar;

	// Use this for initialization
	void Start () {
        airBar = transform.Find("AirUnit").GetComponentInChildren<AirMeter>();
	}
	
	// Update is called once per frame
	void Update () {
        airBar.SetAirBar(airTankPercent);
    }

    public void Consume (float amount)
    {
        airTankPercent -= amount;
        if (airTankPercent <= 0)
        {
            airTankPercent = 0;
        }
    }

    public void Supply (float amount)
    {
        airTankPercent += amount;
        if (airTankPercent >= 100)
        {
            airTankPercent = 100;
        }
    }

}
