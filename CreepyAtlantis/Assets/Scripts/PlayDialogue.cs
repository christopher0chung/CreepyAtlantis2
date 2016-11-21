using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayDialogue : MonoBehaviour, IDialogue, IControllable {

    public string Dialogue;

    public Text outputText;

    public delegate void stateManager ();
    public stateManager currentState;

    private AudioSource lines;
    private AudioSource next;

    public void StateChoices (dialogueStates dS)
    {
        switch (dS)
        {
            case (dialogueStates.speaking):
                lines.Play();
                currentState = Speaking;
                break;
            case (dialogueStates.spoken):
                currentState = Spoken;
                break;
            case (dialogueStates.cleanup):
                currentState = Cleanup;
                break;
            case (dialogueStates.inactive):
                currentState = Inactive;
                break;
        }
    }

    private IDialogueEvent myEvent;

    private int charCounter;
    private int incCounter;

	// Use this for initialization
	void Start () {
        myEvent = transform.parent.GetComponent<IDialogueEvent>();
        outputText = GameObject.Find("Canvas").transform.Find("Subtitle").GetComponent<Text>();
        lines = GetComponent<AudioSource>();
        next = transform.Find("NextSound").GetComponent<AudioSource>();
        //StateChoices(dialogueStates.speaking);
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        if (currentState == Speaking)
    //            StateChoices(dialogueStates.spoken);
    //        else if (currentState == Spoken)
    //            StateChoices(dialogueStates.cleanup);
    //    }
    //}

    // Update is called once per frame
    void FixedUpdate () {
        if (currentState != null)
            currentState();
	}

    private void Speaking ()
    {
        Debug.Log("In speaking");
        incCounter++;
        if (incCounter >= 3)
        {
            incCounter = 0;
            if (charCounter < Dialogue.Length)
                charCounter++;
        }
        if (charCounter >= Dialogue.Length)
            StateChoices(dialogueStates.spoken);

        outputText.text = Dialogue.Substring(0, charCounter);
    }

    private void Spoken ()
    {
        outputText.text = Dialogue;
    }

    private void Cleanup ()
    {
        outputText.text = "";
        //Debug.Log("in clean up");
        myEvent.NextLine();
        lines.Stop();
        next.Play();
        StateChoices(dialogueStates.inactive);
    }

    private void Inactive()
    {
        return;
    }

    public void DebugPrint()
    {
        Debug.Log(Dialogue);
    }




    public void HookUpControls(int player, Controllables theControllable)
    {
        // If 'yes', then see if the controls should be hooked up for the char or not
        if (theControllable == Controllables.dialogue)
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
            UnhookControls(player);
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












    public void LeftStick(float leftRight, float upDown) { }

    public void RightStick(float leftRight, float upDown) { }

    public void AButton(bool pushRelease)
    {
        if (pushRelease)
        {
            if (currentState == Speaking)
                StateChoices(dialogueStates.spoken);
            else if (currentState == Spoken)
                StateChoices(dialogueStates.cleanup);
        }
    }

    public void LeftBumper(bool pushRelease) { }

    public void RightBumper(bool pushRelease) { }
}

public enum dialogueStates { speaking, spoken, cleanup, inactive };
