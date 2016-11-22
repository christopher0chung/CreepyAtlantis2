using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour, IControllable {

    public PlayerMovement myMovement;
    public ToolbaseScript myToolBase;
    public PlayerLightAngle myLA;

    public int charNum;

    private ControllerAdapter myAdapter;


    void Awake()
    {
        // Awake gets called before SceneLoaded

        myToolBase = GetComponentInChildren<ToolbaseScript>();
        myMovement = GetComponent<PlayerMovement>();
        myLA = GetComponentInChildren<PlayerLightAngle>();

        if (gameObject.name == "Character0")
            charNum = 0;
        else
            charNum = 1;

        myAdapter = gameObject.AddComponent<ControllerAdapter>();
        myAdapter.Initialize(charNum);

        GameStateManager.onSetControls += SetControllerAdapter;
        GameStateManager.onEndDialogue += SetControllerAdapter;
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

    public void AButton(bool pushRelease, int pNum) {
        myToolBase.turnOn = pushRelease;
    }

    public void LeftBumper(bool pushRelease) { }

    public void RightBumper(bool pushRelease) { }


    public void SetControllerAdapter(int player, Controllables myControllable)
    {
        if (charNum == player)
        {
            if (myControllable == Controllables.character)
                myAdapter.enabled = true;
            else
                myAdapter.enabled = false;
        }
    }
}
