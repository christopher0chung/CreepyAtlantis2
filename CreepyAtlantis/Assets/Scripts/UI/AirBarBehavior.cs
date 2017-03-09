using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBarBehavior : MonoBehaviour
{

    private SpriteRenderer bar;
    private Transform mask;
    private float _percent;
    private float percent
    {
        get
        {
            return _percent;
        }
        set
        {
            if (value != _percent)
            {
                degrees = (int)(360 - 180 * (value / 100));
                mask.rotation = Quaternion.Euler(0, 0, degrees);
                if (value >= 50)
                    bar.color = Color.green;
                else if (value >= 30)
                    bar.color = Color.Lerp(Color.yellow, Color.green, (value - 30) / 20);
                else if (value >= 20)
                    bar.color = Color.Lerp(Color.red, Color.yellow, (value - 20) / 10);
                else if (value < 20)
                    bar.color = Color.red;

                _percent = value;
            }
        }
    }
    private int degrees;

    public void SetAirBar(float perc)
    {
        percent = perc;
    }

    void Awake()
    {
        mask = transform.GetChild(0);
        bar = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

}
