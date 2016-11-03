using UnityEngine;
using System.Collections;

public class SubControlIO : MonoBehaviour, IControllable {

    public SubControlScript myMovement;

    void Start()
    {
        myMovement = GetComponent<SubControlScript>();
    }

    public void LeftStick(float leftRight, float upDown)
    {
        myMovement.moveLeftRight(leftRight);
    }

    public void RightStick(float leftRight, float upDown)
    {
        if (Mathf.Abs(leftRight) > .3f || Mathf.Abs(upDown) > .3f)
            myMovement.rotateUpDown(leftRight, upDown);
    }

    public void AButton(bool pushRelease)
    {
    }

    public void LeftBumper(bool pushRelease) { }

    public void RightBumper(bool pushRelease) { }
}