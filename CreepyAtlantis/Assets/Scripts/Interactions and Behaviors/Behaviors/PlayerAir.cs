using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerAir : MonoBehaviour {

    private float _lastVal;
    private float _airTankPercent;
    public float airTankPercent
    {
        get
        {
            return _airTankPercent;
        }
        set
        {
            _airTankPercent = value;
            if (_airTankPercent < 30 && _lastVal >= 30)
                airLowAlert.Invoke();
            if (_airTankPercent < 10 && _lastVal >= 10)
                airLowWarning.Invoke();
            if (_airTankPercent <= 0 && _lastVal > 0)
                airZero.Invoke();
            _lastVal = _airTankPercent;
        }
    }

    private AirMeter airBar;

    public UnityEvent airLowAlert;
    public UnityEvent airLowWarning;
    public UnityEvent airZero;

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
