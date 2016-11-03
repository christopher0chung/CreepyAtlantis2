using UnityEngine;
using System.Collections;

public class SubControlIO : MonoBehaviour, IControllable {

    public SubControlScript myMovement;
    public TrapDoorScript myTDS;

    void Start()
    {
        myMovement = GetComponent<SubControlScript>();
        myTDS = GetComponentInChildren<TrapDoorScript>();
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
            LeftStick(0, 0);
            RightStick(0, 0);
            myTDS.ReleaseThePlayers();
        }
    }

    public void LeftBumper(bool pushRelease) { }

    public void RightBumper(bool pushRelease) { }
}