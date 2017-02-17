using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirMeter : MonoBehaviour {

    [SerializeField] private float airPercent;

    private float calculatedPercentage;

    [SerializeField] private float _displayedPercentage;
    private float displayedPercentage
    {
        get
        {
            return _displayedPercentage;
        }

        set
        {
            if (value != _displayedPercentage)
            {
                _displayedPercentage = value;
                if (_displayedPercentage == 100)
                {
                    displayTopOff = true;
                }
                else
                {
                    crossInterval = true;
                }
            }
        }
    }

    private Image myAirBarImage;

    private bool _crossInterval;
    private bool crossInterval
    {
        get
        {
            return _crossInterval;
        }
        set
        {
            if (value != _crossInterval)
            {
                _crossInterval = value;
                // when turning true, stop any running coroutines and start animating
                // when turning false from inside the coroutine, stop all coroutines
                if (_crossInterval)
                {
                    StopAllCoroutines();
                    StartCoroutine("AnimateAirBar", displayedPercentage);
                }
                else
                {
                    StopAllCoroutines();
                }
            }
        }
    }

    private bool _displayTopOff;
    private bool displayTopOff
    {
        get
        {
            return _displayTopOff;
        }
        set
        {
            if (value != _displayTopOff)
            {
                _displayTopOff = value;
                // when turning true, stop any running coroutines and start animating
                // when turning false from inside the coroutine, stop all coroutines
                if (_displayTopOff)
                {
                    StopAllCoroutines();
                    StartCoroutine("DisplayAirBar");
                }
                else
                {
                    StopAllCoroutines();
                }
            }
        }
    }

    private float rate = 2;


    void Start()
    {
        myAirBarImage = GetComponent<Image>();
        myAirBarImage.color = new Color(0, 1, 0, 0);
    }

	void Update () {

        calculatedPercentage = 10 * ((int)(airPercent / 10) + 1);
        displayedPercentage = Mathf.Clamp(calculatedPercentage, 0, 100);
	}

    public void SetAirBar (float airP)
    {
        airPercent = airP;
    }

    private IEnumerator AnimateAirBar (int newValue)
    {
        float fadeVal = 0;
        bool fadeInComplete = false;
        bool fadeHoldComplete = false;
        bool fadeOutComplete = false;
        float barVal = ((float)newValue / 200);

        while (crossInterval)
        {
            if (!fadeInComplete)
            {
                fadeVal = Mathf.Lerp(fadeVal, 1, rate * Time.deltaTime);
                if (1 - fadeVal <= .01f)
                {
                    fadeVal = 1;
                    fadeInComplete = true;
                }
            }
            else if (!fadeHoldComplete)
            {
                myAirBarImage.fillAmount = Mathf.Lerp(myAirBarImage.fillAmount, barVal, 2 * rate * Time.deltaTime);
                if (Mathf.Abs(myAirBarImage.fillAmount - barVal) < .01f)
                {
                    myAirBarImage.fillAmount = barVal;
                    fadeHoldComplete = true;
                }
            }
            else if (!fadeOutComplete)
            {
                fadeVal = Mathf.Lerp(fadeVal, 0, rate * Time.deltaTime);
                if (fadeVal <= .01f)
                {
                    fadeVal = 0;
                    myAirBarImage.color = new Color(0, 1, 0, fadeVal);
                    fadeInComplete = true;
                    crossInterval = false;
                    yield return null;
                }
            }
            if (myAirBarImage.fillAmount >= .25f)
            {
                myAirBarImage.color = new Color(0, 1, 0, fadeVal);
            }
            else if (myAirBarImage.fillAmount < .25f && myAirBarImage.fillAmount >= .1f)
            {
                myAirBarImage.color = Color.Lerp(new Color(0, 1, 0, fadeVal), new Color(1, 1, 0, fadeVal), (fadeVal -.1f) / .15f);
            }
            else if (myAirBarImage.fillAmount < .1f && myAirBarImage.fillAmount >= 0f)
            {
                myAirBarImage.color = Color.Lerp(new Color(1, 1, 0, fadeVal), new Color(1, 0, 0, fadeVal), (fadeVal) / .1f);
            }

            yield return null; 
        }
    }

    private IEnumerator DisplayAirBar ()
    {
        float fadeVal = 0;
        bool fadeInComplete = false;
        bool fadeHoldComplete = false;
        bool fadeOutComplete = false;

        myAirBarImage.fillAmount = .5f;

        while (displayTopOff)
        {
            if (!fadeInComplete)
            {
                fadeVal = Mathf.Lerp(fadeVal, 1, rate * Time.deltaTime);
                if (1 - fadeVal <= .01f)
                {
                    fadeVal = 1;
                    fadeInComplete = true;
                }
            }
            else if (!fadeHoldComplete)
            {
                yield return new WaitForSeconds(.15f);
                fadeHoldComplete = true;
            }
            else if (!fadeOutComplete)
            {
                fadeVal = Mathf.Lerp(fadeVal, 0, rate * Time.deltaTime);
                if (fadeVal <= .01f)
                {
                    fadeVal = 0;
                    myAirBarImage.color = new Color(0, 1, 0, fadeVal);
                    fadeInComplete = true;
                    displayTopOff = false;
                    yield return null;
                }
            }

            myAirBarImage.color = new Color(0, 1, 0, fadeVal);

            yield return null;
        }
    }
}
