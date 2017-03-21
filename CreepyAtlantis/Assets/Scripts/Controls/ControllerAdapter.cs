﻿using UnityEngine;
using System.Collections;

public class ControllerAdapter : MonoBehaviour {

    public int charNum;
    private bool initialized;
    private IControllable myControllable;

    private void Awake()
    {
        GameStateManager.onPreLoadLevel += OnDisable;
    }

    public void Initialize (int myPlayerNum)
    {
        charNum = myPlayerNum;

        myControllable = GetComponent<IControllable>();

        initialized = true;
        OnEnable();
    }

    public void OnEnable ()
    {
        //Debug.Log("OnEnable run on " + gameObject.name + " and initialized is " + initialized);
        if (initialized)
        {
            //Debug.Log("Actual OnEnable in Controller Adapter");
            if (charNum == 0)
            {
                GameObject p0 = GameObject.FindGameObjectWithTag("Player0");
                p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLeftStick += LeftStick;
                p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRightStick += RightStick;
                p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitAButton += AButton;
                p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitYButton += YButton;
                p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRBumper += RightBumper;
                p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLBumper += LeftBumper;
            }
            else if (charNum == 1)
            {
                GameObject p1 = GameObject.FindGameObjectWithTag("Player1");
                p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLeftStick += LeftStick;
                p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRightStick += RightStick;
                p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitAButton += AButton;
                p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitYButton += YButton;
                p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRBumper += RightBumper;
                p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLBumper += LeftBumper;
            }
        }
        else
            return;
    }

    public void OnDisable()
    {
        //Debug.Log("OnDisable run on " + gameObject.name);
        if (initialized)
        {
            if (charNum == 0)
            {
                GameObject p0 = GameObject.FindGameObjectWithTag("Player0");
                p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLeftStick -= LeftStick;
                p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRightStick -= RightStick;
                p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitAButton -= AButton;
                p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitYButton -= YButton;
                p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRBumper -= RightBumper;
                p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLBumper -= LeftBumper;
            }
            else if (charNum == 1)
            {
                GameObject p1 = GameObject.FindGameObjectWithTag("Player1");
                p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLeftStick -= LeftStick;
                p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRightStick -= RightStick;
                p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitAButton -= AButton;
                p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitYButton -= YButton;
                p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRBumper -= RightBumper;
                p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLBumper -= LeftBumper;
            }
        }
        else
            return;
    }

    public void LeftStick(float upDown, float leftRight, int pNum)
    {
        //Debug.Log(leftRight);
        myControllable.LeftStick(upDown, leftRight, pNum);
    }

    public void RightStick(float upDown, float leftRight, int pNum)
    {
        myControllable.RightStick(upDown, leftRight, pNum);
    }

    public void AButton(bool pushRelease, int pNum)
    {
        myControllable.AButton(pushRelease, pNum);
    }

    public void YButton(bool pushRelease, int pNum)
    {
        myControllable.YButton(pushRelease, pNum);
    }

    public void LeftBumper(bool pushRelease, int pNum)
    {
        myControllable.LeftBumper(pushRelease, pNum);
    }

    public void RightBumper(bool pushRelease, int pNum)
    {
        myControllable.RightBumper(pushRelease, pNum);
    }
}