using UnityEngine;
using System.Collections;

public class ControllerAdapter : MonoBehaviour {

    public int charNum;
    private bool _initialized;
    private bool initialized
    {
        get
        {
            return _initialized;
        }
        set
        {
            if (value != _initialized)
            {
                _initialized = value;
                if (_initialized)
                    OnEnable();
            }
        }
    }
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
    }

    public void OnEnable ()
    {
        //Debug.Log("OnEnable run on " + gameObject.name + " and initialized is " + initialized);
        if (initialized)
        {
            //EventManager.instance.Register<Device_GE>(TestOutput);
            //EventManager.instance.Register<Test_GE>(NewTest);

            EventManager.instance.Register<Stick_GE>(OutPut);
            EventManager.instance.Register<Button_GE>(OutPut);

            ////Debug.Log("Actual OnEnable in Controller Adapter");
            //if (charNum == 0)
            //{
            //    GameObject p0 = GameObject.FindGameObjectWithTag("Player0");
            //    p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLeftStick += LeftStick;
            //    p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRightStick += RightStick;
            //    p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitAButton += AButton;
            //    p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitYButton += YButton;
            //    p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRBumper += RightBumper;
            //    p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLBumper += LeftBumper;
            //}
            //else if (charNum == 1)
            //{
            //    GameObject p1 = GameObject.FindGameObjectWithTag("Player1");
            //    p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLeftStick += LeftStick;
            //    p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRightStick += RightStick;
            //    p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitAButton += AButton;
            //    p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitYButton += YButton;
            //    p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRBumper += RightBumper;
            //    p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLBumper += LeftBumper;
            //}
        }
        else
            return;
    }

    public void OnDisable()
    {
        //Debug.Log("OnDisable run on " + gameObject.name);
        if (initialized)
        {
            //EventManager.instance.Register<Device_GE>(TestOutput);

            EventManager.instance.Unregister<Stick_GE>(OutPut);
            EventManager.instance.Unregister<Button_GE>(OutPut);

            //if (charNum == 0)
            //{
            //    GameObject p0 = GameObject.FindGameObjectWithTag("Player0");
            //    p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLeftStick -= LeftStick;
            //    p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRightStick -= RightStick;
            //    p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitAButton -= AButton;
            //    p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitYButton -= YButton;
            //    p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRBumper -= RightBumper;
            //    p0.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLBumper -= LeftBumper;
            //}
            //else if (charNum == 1)
            //{
            //    GameObject p1 = GameObject.FindGameObjectWithTag("Player1");
            //    p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLeftStick -= LeftStick;
            //    p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRightStick -= RightStick;
            //    p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitAButton -= AButton;
            //    p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitYButton -= YButton;
            //    p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRBumper -= RightBumper;
            //    p1.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLBumper -= LeftBumper;
            //}
        }
        else
            return;
    }

    //private void NewTest(GameEvent e)
    //{
    //    Test_GE myE = (Test_GE)e;
    //    Debug.Log(myE.myA + " " + myE.myB + " " + myE.myC + " " + myE.myD);
    //}

    //private void TestOutput(GameEvent e)
    //{
    //    Device_GE thisE = (Device_GE)e;
    //    Debug.Log((float)thisE.thisDev.LeftStickUp);
    //}

    private void OutPut(GameEvent e)
    {
        if (e.GetType() == typeof(Stick_GE))
        {           
            Stick_GE firedSGE = (Stick_GE)e;

            if ((int)firedSGE.thisPID == charNum)
            {
                if (firedSGE.stick == Stick.Left)
                {
                    LeftStick((float)firedSGE.upDown, (float)firedSGE.leftRight, (int)firedSGE.thisPID);
                }
                else if (firedSGE.stick == Stick.Right)
                {
                    RightStick((float)firedSGE.upDown, (float)firedSGE.leftRight, (int)firedSGE.thisPID);
                }
            }
        }
        else if (e.GetType() == typeof(Button_GE))
        {
            Button_GE firedBGE = (Button_GE)e;

            if ((int)firedBGE.thisPID == charNum)
            {
                if (firedBGE.button == Button.Action)
                {
                    AButton(firedBGE.pressedReleased, (int)firedBGE.thisPID);
                }
                else if (firedBGE.button == Button.Dialogue)
                {
                    YButton(firedBGE.pressedReleased, (int)firedBGE.thisPID);
                }
                else if (firedBGE.button == Button.Choice1)
                {
                    LeftBumper(firedBGE.pressedReleased, (int)firedBGE.thisPID);
                }
                else if (firedBGE.button == Button.Choice2)
                {
                    RightBumper(firedBGE.pressedReleased, (int)firedBGE.thisPID);
                }
            }
        }
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
