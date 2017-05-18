using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GE_SubMove : GameEvent
{
    public float lR;
    public float uD;
    public GE_SubMove(float leftRight, float upDown)
    {
        lR = leftRight;
        uD = upDown;
    }
}

public class UI_GE_SubMove : UI_Base {

    [SerializeField] private SpriteRenderer mySR;
    [SerializeField] private SpriteRenderer arrow;

    private bool shipInput;
    private float ang;

    // Use this for initialization
    void Start () {
        EventManager.instance.Register<GE_SubMove>(LocalHandler);

        _Init();
        _GetSpeaker();
    }

    private void LocalHandler(GameEvent e)
    {
        GE_SubMove s = (GE_SubMove)e;
        if (s.lR == 0 && s.uD == 0)
        {
            shipInput = false;
        }
        else
        {
            shipInput = true;
            ang = Mathf.Atan2(s.uD, s.lR) * Mathf.Rad2Deg;
        }

    }

    void Update () {
		if (shipInput)
        {
            mySR.enabled = false;
            arrow.enabled = true;
            arrow.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, ang));
        }
        else
        {
            mySR.enabled = true;
            arrow.enabled = false;
        }
	}
}
