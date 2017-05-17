using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GE_Controls : UI_Base {

    [SerializeField] private SpriteRenderer mySR;
    [SerializeField] private SpriteRenderer dPad;
    [SerializeField] private SpriteRenderer dPadDirUp;
    [SerializeField] private SpriteRenderer dPadDirDown;
    [SerializeField] private SpriteRenderer arrow;
    [SerializeField] private MeshRenderer a;
    [SerializeField] private MeshRenderer y;


    private bool aPressed;
    private bool yPressed;
    private bool dPadUpPressed;
    private bool dPadDownPressed;
    private bool leftStickInputing;
    private bool rightStickInputting;

    private float leftStickAng;
    private float rightStickAng;

    void Start()
    {
        EventManager.instance.Register<Button_GE>(LocalHandler);
        EventManager.instance.Register<Stick_GE>(LocalHandler);

        _Init();
        _GetSpeaker();
    }

    void LocalHandler(GameEvent e)
    {
        if (e.GetType() == typeof(Button_GE))
        {
            Button_GE b = (Button_GE)e;
            if ((b.thisPID == PlayerID.p1 && thisSpeaker == Speaker.Ops) || (b.thisPID == PlayerID.p2 && thisSpeaker == Speaker.Doc))
            {
                if (b.button == Button.Action)
                    aPressed = b.pressedReleased;
                else if (b.button == Button.Dialogue)
                    yPressed = b.pressedReleased;
                else if (b.button == Button.Choice1)
                    dPadUpPressed = b.pressedReleased;
                else if (b.button == Button.Choice2)
                    dPadDownPressed = b.pressedReleased;
            }
        }
        else if (e.GetType() == typeof(Stick_GE))
        {
            Stick_GE s = (Stick_GE)e;
            if ((s.thisPID == PlayerID.p1 && thisSpeaker == Speaker.Ops) || (s.thisPID == PlayerID.p2 && thisSpeaker == Speaker.Doc))
            {
                if (s.stick == Stick.Left)
                {
                    if (s.leftRight != 0 || s.upDown != 0)
                    {
                        leftStickInputing = true;
                        leftStickAng = Mathf.Atan2(s.upDown, s.leftRight);
                    }
                    else
                        leftStickInputing = false;
                }
                else
                {
                    if (s.leftRight != 0 || s.upDown != 0)
                    {
                        rightStickInputting = true;
                        rightStickAng = Mathf.Atan2(s.upDown, s.leftRight);
                    }
                    else
                        rightStickInputting = false;
                }
            }
        }
    }

    void Update()
    {
        ClearOut();
        if (aPressed)
        {
            mySR.enabled = true;
            a.enabled = true;
        }
        else if (yPressed)
        {
            mySR.enabled = true;
            y.enabled = true;
        }
        else if (dPadUpPressed)
        {
            mySR.enabled = true;
            dPad.enabled = true;
            dPadDirUp.enabled = true;
        }
        else if (dPadDownPressed)
        {
            mySR.enabled = true;
            dPad.enabled = true;
            dPadDirDown.enabled = true;
        }
        else if (leftStickInputing)
        {
            arrow.enabled = true;
            arrow.color = new Color(0, .5f, 0, 100f / 255);
            arrow.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90 + leftStickAng * Mathf.Rad2Deg));
        }
        else if (rightStickInputting)
        {
            arrow.enabled = true;
            arrow.color = new Color(1, 0, 0, 100f / 255);
            arrow.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90 + rightStickAng * Mathf.Rad2Deg));
        }
        else
            mySR.enabled = true;
    }

    void ClearOut()
    {
        mySR.enabled = false;
        dPad.enabled = false;
        dPadDirUp.enabled = false;
        dPadDirDown.enabled = false;
        arrow.enabled = false;
        a.enabled = false;
        y.enabled = false;
    }
}
