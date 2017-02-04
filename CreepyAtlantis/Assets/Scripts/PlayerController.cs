using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour, IControllable {

    public PlayerMovement myMovement;
    public PlayerAction myPA;
    public PlayerLightAngle myLA;

    public int charNum;

    private ControllerAdapter myAdapter;


    void Awake()
    {
        // Awake gets called before SceneLoaded

        myPA = GetComponent<PlayerAction>();
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

    public void LeftStick(float upDown, float leftRight, int pNum) {
        //if (leftRight > 0.25f)
        //    myMovement.MoveRight();
        //else if (leftRight < -0.25f)
        //    myMovement.MoveLeft();
        //else
        //    myMovement.MoveNeutral();

        //if (upDown > 0.25)
        //    myMovement.Boost(true);
        //else
        //    myMovement.Boost(false);

        myMovement.AltMovement(upDown, leftRight);
    }

    public void RightStick(float upDown, float leftRight, int pNum) {
        if (Mathf.Abs(leftRight) >.25f || Mathf.Abs(upDown) >.25f)
            myLA.LookAngleCalc(leftRight, upDown);
    }

    public void AButton(bool pushRelease, int pNum) {
        myPA.TryToInteract(pNum, pushRelease);
    }

    public void LeftBumper(bool pushRelease, int pNum) { }

    public void RightBumper(bool pushRelease, int pNum) { }


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
