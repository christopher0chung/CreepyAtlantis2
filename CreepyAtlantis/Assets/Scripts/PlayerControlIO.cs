using UnityEngine;
using System.Collections;

public class PlayerControlIO : MonoBehaviour, IControllable {

    public PlayerMovement myMovement;
    public ToolbaseScript myToolBase;
    public PlayerLightAngle myLA;

    public int charNum;
    
    void Awake()
    {
        myToolBase = GetComponentInChildren<ToolbaseScript>();
        myMovement = GetComponent<PlayerMovement>();
        myLA = GetComponentInChildren<PlayerLightAngle>();

        GameStateManager.onHookCtrl += HookUpControls;
        GameStateManager.onUnhookCtrl += UnhookControls;
        GameStateManager.onEndDialogue += HookUpControls;

        if (gameObject.name == "Character0")
            charNum = 0;
        else
            charNum = 1;

    }

    public void LeftStick(float upDown, float leftRight) {
        if (leftRight > 0.25f)
            myMovement.MoveRight();
        else if (leftRight < -0.25f)
            myMovement.MoveLeft();
        else
            myMovement.MoveNeutral();

        if (upDown > 0.25)
            myMovement.Boost(true);
        else
            myMovement.Boost(false);
    }

    public void RightStick(float upDown, float leftRight) {
        if (Mathf.Abs(leftRight) >.25f || Mathf.Abs(upDown) >.25f)
            myLA.LookAngleCalc(leftRight, upDown);
    }

    public void AButton(bool pushRelease) {
        myToolBase.turnOn = pushRelease;
    }

    public void LeftBumper(bool pushRelease) { }

    public void RightBumper(bool pushRelease) { }

    public void HookUpControls (int player, Controllables theControllable)
    {
        // If HookUpControls gets called becaues of initial hookUp or from a swap...
        // First see if it applies to this char.
        if (player == charNum)
        {
            // If 'yes', then see if the controls should be hooked up for the char or not
            if (theControllable == Controllables.character)
            {
                if (player == 0)
                {
                    GameObject[] myP0s = GameObject.FindGameObjectsWithTag("Player0");
                    foreach (GameObject myController in myP0s)
                    {
                        myController.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLeftStick += LeftStick;
                        myController.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRightStick += RightStick;
                        myController.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitAButton += AButton;
                    }
                }
                else if (player == 1)
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
            {
                UnhookControls(charNum);
            }  
        }
    }

    public void UnhookControls(int player)
    {
        if (player == 0)
        {
            GameObject[] myP0s = GameObject.FindGameObjectsWithTag("Player0");
            foreach (GameObject myController in myP0s)
            {
                myController.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitLeftStick -= LeftStick;
                myController.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitRightStick -= RightStick;
                myController.GetComponent<MultiplayerWithBindingsExample.Player>().onXmitAButton -= AButton;
            }
        }
        else
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
}
