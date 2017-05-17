using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GE_Air : GameEvent
{
    public Speaker speaker;
    public float howMuch;

    public GE_Air (Speaker s, float f)
    {
        speaker = s;
        howMuch = f;
    }
}

public class UI_GE_Air : UI_Base {

    [HideInInspector]
    private SpriteRenderer mySR;
    private float airAmt;

    [Header("Air Color and threshold values")]
    public Color fullColor;
    public float midColorThres;
    public Color midColor;
    public float lowColorThresh;
    public Color lowColor;

    private Color newColor;

    void Start()
    {
        EventManager.instance.Register<GE_Air>(LocalHandler);

        _Init();
        mySR = GetComponent<SpriteRenderer>();

        _GetSpeaker();
    }

    void LocalHandler(GameEvent e)
    {
        if (e.GetType() == typeof(GE_Air))
        {
            GE_Air a = (GE_Air)e;
            if (a.speaker == thisSpeaker)
            {
                airAmt = a.howMuch;
                if (a.howMuch > midColorThres)
                    newColor = fullColor;
                else if (a.howMuch <= midColorThres && a.howMuch > lowColorThresh)
                    newColor = midColor;
                else if (a.howMuch <= lowColorThresh)
                    newColor = lowColor;
            }
        }
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(airAmt, 1, 1), .08f);
        mySR.color = Color.Lerp(mySR.color, newColor, .08f);
    }
}
