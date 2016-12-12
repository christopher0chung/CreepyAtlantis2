using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAir : MonoBehaviour {

    public float airTankPercent;

    private AirBarBehavior airBar;

    //public Text myAirText;

    public Color goodColor;
    public Color badColor;

	// Use this for initialization
	void Start () {
        airBar = transform.Find("AirBarUnit").GetComponent<AirBarBehavior>();
	}
	
	// Update is called once per frame
	void Update () {

        airBar.SetAirBar(airTankPercent);
        //myAirText.text = ("Air - " + Mathf.Round(airTankPercent).ToString() + "%");
        //myAirText.color = Color.Lerp(badColor, goodColor, airTankPercent / 100);
	
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
            //myAirText.text = ("Air: " + Mathf.Round(airTankPercent).ToString() + "%");
            //myAirText.color = Color.Lerp(badColor, goodColor, airTankPercent / 100);
        }
    }

}
