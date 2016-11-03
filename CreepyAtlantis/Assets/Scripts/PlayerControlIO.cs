using UnityEngine;
using System.Collections;

public class PlayerControlIO : MonoBehaviour, IControllable {

    public PlayerMovement myMovement;
    public ToolbaseScript myToolBase;
    public PlayerLightAngle myLA;
    
    void Start()
    {
        myToolBase = GetComponentInChildren<ToolbaseScript>();
        myMovement = GetComponent<PlayerMovement>();
        myLA = GetComponentInChildren<PlayerLightAngle>();
    }

    public void LeftStick(float leftRight, float upDown) {
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

    public void RightStick(float leftRight, float upDown) {
        if (Mathf.Abs(leftRight) >.25f || Mathf.Abs(upDown) >.25f)
            myLA.LookAngleCalc(leftRight, upDown);
    }

    public void AButton(bool pushRelease) {
        myToolBase.turnOn = pushRelease;
    }

    public void LeftBumper(bool pushRelease) { }

    public void RightBumper(bool pushRelease) { }
}
