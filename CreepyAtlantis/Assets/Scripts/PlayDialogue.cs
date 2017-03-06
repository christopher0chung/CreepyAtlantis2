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
    public ObjectivesTracker myOT;

    public Color myColor;
    private bool colorFlip;

    private float timer;
    public float dialogueAdvanceTime;
    private bool timerFlip;

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

    private LinkToDialogueEvent myLink;

    void Awake ()
    {
        gameObject.AddComponent<ControllerAdapter>();
        gameObject.AddComponent<ControllerAdapter>();
        myAdapters = GetComponents<ControllerAdapter>();
        myAdapters[0].Initialize(0);
        myAdapters[1].Initialize(1);

        GameStateManager.onSetControls += SetControllerAdapter;
        GameStateManager.onEndDialogue += SetControllerAdapter;

        myOT = GameObject.Find("GameStateManager").GetComponent<ObjectivesTracker>();

        myLink = GetComponent<LinkToDialogueEvent>();
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
        {
            currentState();
            timer += Time.deltaTime;
            if (dialogueAdvanceTime != 0 && timer>= dialogueAdvanceTime && !timerFlip)
            {
                currentState = Cleanup;
            }
        }

	}

    private void Speaking ()
    {
        if (!colorFlip)
        {
            colorFlip = true;
            outputText.color = myColor;
        }
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
        timerFlip = true;
        outputText.text = "";
        //Debug.Log("in clean up");
        myEvent.NextLine();
        lines.Stop();
        next.Play();
        StateChoices(dialogueStates.inactive);
        myOT.ObjectivesUpdate(gameObject.name);
        if (myLink != null)
        {
            myLink.Link();
        }
    }

    private void Inactive()
    {
        return;
    }

    public void DebugPrint()
    {
        Debug.Log(Dialogue);
    }

    public void LeftStick(float leftRight, float upDown, int pNum) { }

    public void RightStick(float leftRight, float upDown, int pNum) { }

    public void AButton(bool pushRelease, int pNum) { }

    public void YButton(bool pushRelease, int pNum)
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

    public void LeftBumper(bool pushRelease, int pNum) { }

    public void RightBumper(bool pushRelease, int pNum) { }

    public void SetControllerAdapter(int player, Controllables myControllable)
    {
     if (myAdapters[player] != null)
        {
            if (myControllable == Controllables.dialogue)
                myAdapters[player].enabled = true;
            else
                myAdapters[player].enabled = false;
        }
    }
}
