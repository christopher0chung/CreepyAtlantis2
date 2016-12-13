using UnityEngine;
using System.Collections;

public class SubController : MonoBehaviour, IControllable {

    public SubControlScript myMovement;
    public TrapDoorScript myTDS;
    public ControllerAdapter [] myAdapters;

    void Awake()
    {
        myMovement = GetComponent<SubControlScript>();
        myTDS = GetComponentInChildren<TrapDoorScript>();

        gameObject.AddComponent<ControllerAdapter>();
        gameObject.AddComponent<ControllerAdapter>();
        myAdapters = GetComponents<ControllerAdapter>();
        myAdapters[0].Initialize(0);
        myAdapters[1].Initialize(1);

        GameStateManager.onSetControls += SetControllerAdapter;
        GameStateManager.onEndDialogue += SetControllerAdapter;

    }

    public void LeftStick(float upDown, float leftRight)
    {
        myMovement.moveLeftRight(leftRight);
    }

    public void RightStick(float upDown, float leftRight)
    {
        myMovement.rotateUpDown(upDown, leftRight);
    }

    public void AButton(bool pushRelease, int pNum)
    {
        if (pushRelease)
        {
            GameObject.Find("GameStateManager").GetComponent<GameStateManager>().SetControls(pNum, Controllables.character);
            GameObject.Find("GameStateManager").GetComponent<GameStateManager>().SubInteract(pNum, false);
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