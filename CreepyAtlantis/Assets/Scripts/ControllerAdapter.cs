using UnityEngine;
using System.Collections;

public class ControllerAdapter : MonoBehaviour {

    public int charNum;
    private bool initialized;
    private IControllable myControllable;

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
            if (charNum == 0)
            {
                GameObject[] myP0s = GameObject.FindGameObjectsWithTag("Player0");
                foreach (GameObject myController in myP0s)
                {
                    myController.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLeftStick += LeftStick;
                    myController.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRightStick += RightStick;
                    myController.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitAButton += AButton;
                }
            }
            else if (charNum == 1)
            {
                GameObject[] myP1s = GameObject.FindGameObjectsWithTag("Player1");
                foreach (GameObject myController in myP1s)
                {
                    myController.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLeftStick += LeftStick;
                    myController.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRightStick += RightStick;
                    myController.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitAButton += AButton;
                }
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
                GameObject[] myP0s = GameObject.FindGameObjectsWithTag("Player0");
                foreach (GameObject myController in myP0s)
                {
                    myController.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLeftStick -= LeftStick;
                    myController.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRightStick -= RightStick;
                    myController.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitAButton -= AButton;
                }
            }
            else if (charNum == 1)
            {
                GameObject[] myP1s = GameObject.FindGameObjectsWithTag("Player1");
                foreach (GameObject myController in myP1s)
                {
                    myController.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLeftStick -= LeftStick;
                    myController.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRightStick -= RightStick;
                    myController.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitAButton -= AButton;
                }
            }
        }
        else
            return;
    }

    public void LeftStick(float upDown, float leftRight)
    {
        myControllable.LeftStick(upDown, leftRight);
    }

    public void RightStick(float upDown, float leftRight)
    {
        myControllable.RightStick(upDown, leftRight);
    }

    public void AButton(bool pushRelease, int pNum)
    {
        myControllable.AButton(pushRelease, pNum);
    }

    public void LeftBumper(bool pushRelease)
    {

    }

    public void RightBumper(bool pushRelease)
    {

    }
}
