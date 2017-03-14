using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayDialogue : MonoBehaviour, IDialogue, IControllable {

    //--------------------
    // Public properties
    //--------------------

    public string Dialogue;

    public delegate void stateManager ();
    public stateManager currentState;


    //--------------------
    // Private properties
    //--------------------

    private Text outputText;

    private AudioSource lines;
    private AudioSource next;

    private ControllerAdapter[] myAdapters;
    private IObjective myO;

    public Color myColor;
    private bool colorFlip;

    private float timer;
    private bool timerFlip;

    private IDialogueEvent myEvent;

    private int charCounter;
    private float incCounter;

    private LinkToDialogueEvent myLink;

    private float dialogueAdvanceTime;
    private float nextCharTime;


    //--------------------
    // State machine
    //--------------------

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
            case (dialogueStates.stopped):
                currentState = Stopped;
                break;
        }
    }


    //--------------------
    // Scheduled functionality
    //--------------------

    void Awake ()
    {
        gameObject.AddComponent<ControllerAdapter>();
        gameObject.AddComponent<ControllerAdapter>();
        myAdapters = GetComponents<ControllerAdapter>();
        myAdapters[0].Initialize(0);
        myAdapters[1].Initialize(1);

        GameStateManager.onSetControls += SetControllerAdapter;
        GameStateManager.onEndDialogue += EndDialogue;

        myLink = GetComponent<LinkToDialogueEvent>();
    }

    void Start () {
        myEvent = transform.parent.GetComponent<IDialogueEvent>();
        outputText = GameObject.Find("Canvas").transform.Find("Subtitle").GetComponent<Text>();
        lines = GetAudio(soundType.line);
        next = GetAudio(soundType.sfx);

        if (GetComponent<IObjective>() != null)
        {
            myO = GetComponent<IObjective>();
            //Debug.Log("Objective found");
        }
    }

    void Update () {
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


    //--------------------
    // Specific functionality
    //--------------------

    public enum soundType { sfx, line }

    private AudioSource GetAudio (soundType myST)
    {
        if (myST == soundType.line)
        {
            if (Dialogue.Length == 0)
            {
                return null;
            }
            AudioSource a = gameObject.AddComponent<AudioSource>();
            a.clip = Resources.Load<AudioClip>("Dialogue/" + (string)this.gameObject.name);
            if (a.clip != null)
            {
                dialogueAdvanceTime = a.clip.length + 2;
                nextCharTime = a.clip.length / Dialogue.Length;
                a.volume = 1;
                a.loop = false;
                a.playOnAwake = false;
                a.Stop();
            }
            else
            {
                nextCharTime = .02f;
            }
            return a;
        }
        else
        {
            GameObject g = (GameObject)Instantiate(Resources.Load("Blank"), this.transform);
            AudioSource a = g.AddComponent<AudioSource>();
            if (a != null)
            {
                a.clip = Resources.Load<AudioClip>("SFX/NextSound");
                a.volume = .03f;
                a.pitch = .5f;
                a.loop = false;
                a.playOnAwake = false;
                a.Stop();
            }
            return a;
        }
    }

    private int PrintName()
    {
        for (int i = 1; i < Dialogue.Length; i++)
        {
            if (Dialogue[i] == ':')
            {
                //Debug.Log(Dialogue.Substring(0, i));
                return i;

            }
        }
        return 0;
    }


    //--------------------
    // IDialogue implementation
    //--------------------

    private void Speaking ()
    {
        if (!colorFlip)
        {
            colorFlip = true;
            outputText.color = myColor;

            // At the time of color flip, advances charCounter to name printed.
            charCounter = PrintName();
        }
        incCounter+= Time.deltaTime;
        if (incCounter >= nextCharTime)
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
        myEvent.NextLine();
        lines.Stop();
        StateChoices(dialogueStates.inactive);

        //if (lines != null)
        //{
        //    Destroy(lines);
        //}

        //if (next != null)
        //{
        //    Destroy(next.gameObject);
        //}

        if (myO != null)
        {
            myO.Trigger();
        }

        if (myLink != null)
        {
            myLink.Link();
        }
    }

    private void Inactive()
    {
        return;
    }

    private void Stopped()
    {
        currentState = null;
        colorFlip = false;
        timerFlip = false;
        outputText.text = "";
        lines.Stop();
    }

    public void DebugPrint()
    {
        Debug.Log(Dialogue);
    }


    //--------------------
    // IControllable implementation
    //--------------------

    public void LeftStick(float leftRight, float upDown, int pNum) { }

    public void RightStick(float leftRight, float upDown, int pNum) { }

    public void AButton(bool pushRelease, int pNum) { }

    public void YButton(bool pushRelease, int pNum)
    {
        if (pushRelease)
        {
            if (next != null)
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
            //Debug.Log("player" + player + " dialogue adapter enabled");
        }
    }
    public void EndDialogue(int player, Controllables myControllable)
    {
        if (myAdapters[player] != null)
        {
            myAdapters[player].enabled = false;
            //Debug.Log("player" + player + " dialogue adapter disabled");
        }
    }
}