using UnityEngine;
using System.Collections;

public class SubController : MonoBehaviour, IControllable {

    public SubControlScript myMovement;
    public TrapDoorScript myTDS;
    private ControllerAdapter [] myAdapters;

    void Awake()
    {
        myMovement = GetComponent<SubControlScript>();
        myTDS = GetComponentInChildren<TrapDoorScript>();

        myAdapters[0] = gameObject.AddComponent<ControllerAdapter>();
        myAdapters[0].Initialize(0);
        myAdapters[1] = gameObject.AddComponent<ControllerAdapter>();
        myAdapters[1].Initialize(1);

        GameStateManager.onSetControls += SetControllerAdapter;
        GameStateManager.onEndDialogue += SetControllerAdapter;
    }

    public void LeftStick(float leftRight, float upDown)
    {
        myMovement.moveLeftRight(leftRight);
    }

    public void RightStick(float leftRight, float upDown)
    {
        myMovement.rotateUpDown(leftRight, upDown);
    }

    public void AButton(bool pushRelease)
    {
        if (pushRelease)
        {
            //LeftStick(0, 0);
            //RightStick(0, 0);
            //myTDS.ReleaseThePlayers();
        }
    }

    public void LeftBumper(bool pushRelease) { }

    public void RightBumper(bool pushRelease) { }

    public void SetControllerAdapter(int player, Controllables myControllable)
    {
        if (myControllable == Controllables.submarine)
            myAdapters[player].enabled = true;
        else
            myAdapters[player].enabled = false;
    }
}