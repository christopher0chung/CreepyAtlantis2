using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayDialogue : MonoBehaviour, IDialogue, IControllable {

    //--------------------
    // Public properties
    //--------------------

    public Speaker theSpeaker;

    public string Dialogue;

    //public delegate void stateManager ();
    //public stateManager currentState;

    //--------------------
    // Private properties
    //--------------------

    private Text speakerText;
    private Text outputText;

    private AudioSource lines;
    private AudioSource next;

    private Color refColor;

    private ControllerAdapter[] myAdapters;
    private IObjective myO;

    private bool colorFlip;

    private IDialogueEvent myEvent;

    private LinkToDialogueEvent myLink;

    private float dialogueAdvanceTime;
    private float nextCharInterval;

    private float startTalkTime;
    private float audioLength;

    private FSM<PlayDialogue> _fsm;

    void Awake()
    {
        gameObject.AddComponent<ControllerAdapter>();
        gameObject.AddComponent<ControllerAdapter>();
        myAdapters = GetComponents<ControllerAdapter>();
        myAdapters[0].Initialize(0);
        //Debug.Log(gameObject.name + " run Initialize for 0");
        myAdapters[1].Initialize(1);
        //Debug.Log(gameObject.name + " run Initialize for 1");

        GameStateManager.onSetControls += SetControllerAdapter;
        GameStateManager.onEndDialogue += EndDialogue;
        GameStateManager.onPreLoadLevel += UnSub;

        myLink = GetComponent<LinkToDialogueEvent>();
    }

    //------------------------------------------------
    //Event System
    //------------------------------------------------

    private void UnSub()
    {
        GameStateManager.onSetControls -= SetControllerAdapter;
        GameStateManager.onEndDialogue -= EndDialogue;
        GameStateManager.onPreLoadLevel -= UnSub;
    }

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

    //------------------------------------------------
    // Basic
    //------------------------------------------------

    private void Start()
    {
        _fsm = new FSM<PlayDialogue>(this);

        _fsm.TransitionTo<Standby>();

        myEvent = transform.parent.GetComponent<IDialogueEvent>();
        outputText = GameObject.Find("Canvas").transform.Find("Subtitle").GetComponent<Text>();

        GetMyColor();

        lines = GetAudio(soundType.line);
        next = GetAudio(soundType.sfx);

        if (GetComponent<IObjective>() != null)
        {
            myO = GetComponent<IObjective>();
        }
    }

    private void Update()
    {
        _fsm.Update();
    }

    //--------------------
    // Specific functionality
    //--------------------

    public enum soundType { sfx, line }

    private AudioSource GetAudio(soundType myST)
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
                audioLength = a.clip.length;
                dialogueAdvanceTime = a.clip.length + 2;
                nextCharInterval = (a.clip.length - 1) / Dialogue.Length;
                a.volume = 1;
                a.loop = false;
                a.playOnAwake = false;
                a.Stop();
            }
            else
            {
                nextCharInterval = .02f;
                audioLength = .02f * Dialogue.Length;
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

    private void GetMyColor()
    {
        if (theSpeaker == Speaker.DANI)
        {
            refColor = GameObject.FindGameObjectWithTag("Managers").GetComponent<ColorManager>().DANI;
        }
        else if (theSpeaker == Speaker.Doc)
        {
            refColor = GameObject.FindGameObjectWithTag("Managers").GetComponent<ColorManager>().Doc;
        }
        else if (theSpeaker == Speaker.Ops)
        {
            refColor = GameObject.FindGameObjectWithTag("Managers").GetComponent<ColorManager>().Ops;
        }
    }

    //--------------------
    // IDialogue implementation
    //--------------------

    public void StateChoices(dialogueStates dS)
    {
        if (dS == dialogueStates.speaking)
        {
            lines.Play();
            GameObject.Find("Canvas").GetComponent<GameUIController>().DialogueFunction(theSpeaker, true);
            _fsm.TransitionTo<SpeakingState>();
        }

        if (dS == dialogueStates.spoken)
        {
            _fsm.TransitionTo<SpokenState>();
        }

        if (dS == dialogueStates.complete)
        {
            _fsm.TransitionTo<CompleteState>();
        }

        if (dS == dialogueStates.cleanup)
        {
            GameObject.Find("Canvas").GetComponent<GameUIController>().DialogueFunction(theSpeaker, false);
            _fsm.TransitionTo<CleanupState>();
        }

        if (dS == dialogueStates.inactive)
        {
            _fsm.TransitionTo<Standby>();
        }

        if (dS == dialogueStates.stopped)
        {
            GameObject.Find("Canvas").GetComponent<GameUIController>().DialogueFunction(theSpeaker, false);
            _fsm.TransitionTo<CleanupState>();
        }

    }

    private void Speaking()
    {
    }

    private void Spoken()
    {
    }

    private void Cleanup()
    {
    }

    private void Inactive()
    {
    }

    private void Stopped()
    {
    }

    public void DebugPrint()
    {
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
            //Debug.Log("Y Pressed");
            //if (next != null)
            //    next.Play();
            if (((BasicState)_fsm.CurrentState).name == "SpeakingState")
                StateChoices(dialogueStates.spoken);
            else if (((BasicState)_fsm.CurrentState).name == "SpokenState")
                StateChoices(dialogueStates.complete);
        }
    }

    public void LeftBumper(bool pushRelease, int pNum) { }

    public void RightBumper(bool pushRelease, int pNum) { }



    //------------------------------------------------
    // States
    //------------------------------------------------

    private class BasicState : FSM<PlayDialogue>.State
    {
        public string name;
    }

    private class Standby : BasicState
    {
        public override void Init()
        {
            name = "Standby";
        }

        public override void OnEnter()
        {

        }

        public override void Update()
        {
            return;
        }

        public override void OnExit()
        {

        }
    }

    private class SpeakingState : BasicState
    {
        private float charTimer;
        private int charCounter;

        public override void Init()
        {
            name = "SpeakingState";
        }

        public override void OnEnter()
        {
            if (!Context.colorFlip)
            {
                Context.colorFlip = true;
                Context.outputText.color = Context.refColor;
            }

            charTimer = 0;
            Context.startTalkTime = Time.time;
        }

        public override void Update()
        {
            charTimer += Time.deltaTime;

            charCounter = Mathf.Abs((int)(charTimer / Context.nextCharInterval));

            if (charCounter > Context.Dialogue.Length)
                TransitionTo<SpokenState>();
            else
                Context.outputText.text = Context.Dialogue.Substring(0, charCounter);
        }

        public override void OnExit()
        {

        }
    }

    private class SpokenState : BasicState
    {
        public override void Init()
        {
            name = "SpokenState";
        }

        public override void OnEnter()
        {
            Context.outputText.text = Context.Dialogue;
        }

        public override void Update()
        {
            if (Time.time - Context.startTalkTime >= Context.audioLength)
            {
                TransitionTo<CompleteState>();
            }
        }

        public override void OnExit()
        {

        }
    }

    private class CompleteState : BasicState
    {
        public override void Init()
        {
            name = "CompleteState";
        }

        public override void OnEnter()
        {

        }

        public override void Update()
        {
            TransitionTo<CleanupState>();
        }

        public override void OnExit()
        {
            Context.myEvent.NextLine();

            if (Context.myO != null)
            {
                Context.myO.Trigger();
            }

            if (Context.myLink != null)
            {
                Context.myLink.Link();
            }
        }
    }

    private class CleanupState : BasicState
    {
        public override void Init()
        {
            name = "CleanupState";
        }

        public override void OnEnter()
        {
            //Debug.Log("OnEnter() called");

            Context.lines.Stop();
            if (Context.outputText.text != "")
            {
                Context.outputText.text = "";
            }
        }

        public override void Update()
        {
            TransitionTo<Standby>();
        }

        public override void OnExit()
        {

        }
    }
}


public enum Speaker { DANI, Doc, Ops }