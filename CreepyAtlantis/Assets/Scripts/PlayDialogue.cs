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

    public ControllerAdapter[] myAdapters;

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

    void Awake ()
    {
        gameObject.AddComponent<ControllerAdapter>();
        gameObject.AddComponent<ControllerAdapter>();
        myAdapters = GetComponents<ControllerAdapter>();
        myAdapters[0].Initialize(0);
        myAdapters[1].Initialize(1);

        GameStateManager.onSetControls += SetControllerAdapter;
        GameStateManager.onEndDialogue += SetControllerAdapter;
    }

    void Start () {
        myEvent = transform.parent.GetComponent<IDialogueEvent>();
        outputText = GameObject.Find("Canvas").transform.Find("Subtitle").GetComponent<Text>();
        lines = GetComponent<AudioSource>();
        next = transform.Find("NextSound").GetComponent<AudioSource>();
        //StateChoices(dialogueStates.speaking);
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (currentState != null)
            currentState();
	}

    private void Speaking ()
    {
        //Debug.Log("In speaking");
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

    public void LeftStick(float leftRight, float upDown) { }

    public void RightStick(float leftRight, float upDown) { }

    public void AButton(bool pushRelease, int pNum)
    {
        if (pushRelease)
        {
            next.Play();
            if (currentState == Speaking)
                StateChoices(dialogueStates.spoken);
            else if (currentState == Spoken)
                StateChoices(dialogueStates.cleanup);
        }
    }

    public void LeftBumper(bool pushRelease) { }

    public void RightBumper(bool pushRelease) { }

    public void SetControllerAdapter(int player, Controllables myControllable)
    {
     
        if (myControllable == Controllables.dialogue)
            myAdapters[player].enabled = true;
        else
            myAdapters[player].enabled = false;
    }
}

public enum dialogueStates { speaking, spoken, cleanup, inactive };
