using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueChoice : MonoBehaviour, IDialogue, IControllable{

    //--------------------
    // Public properites
    //--------------------

    public int whoseChoice;

    public string Choice1;
    public string Choice2;

    //private Text label;
    private Text outputChoice1;
    private Text outputChoice2;

    public delegate void stateManager();
    public stateManager currentState;

    public Color myColor;

    public float dialogueAdvanceTime;

    public GameObject eventChoice1;
    public GameObject eventChoice2;


    //--------------------
    // Private properties
    //--------------------

    private FSM<DialogueChoice> _fsm;

    private ControllerAdapter[] myAdapters;
    
    private bool colorFlip;

    private AudioSource next;

    private IDialogueEvent myEvent;

    private DialogueManager myDM;

    private GameObject lBIcon;
    private GameObject rBIcon;

    private Speaker theSpeaker;


    //--------------------
    // State machine
    //--------------------

    public void StateChoices(dialogueStates dS)
    {
        if (dS == dialogueStates.speaking)
        {
            GameObject.Find("Canvas").GetComponent<GameUIController>().DialogueFunction(theSpeaker, true);
            _fsm.TransitionTo<SpokenState>();
        }

        if (dS == dialogueStates.spoken)
        {
            GameObject.Find("Canvas").GetComponent<GameUIController>().DialogueFunction(theSpeaker, true);
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


    //--------------------
    // Scheduled functionality
    //--------------------

    void Awake()
    {
        gameObject.AddComponent<ControllerAdapter>();
        gameObject.AddComponent<ControllerAdapter>();
        myAdapters = GetComponents<ControllerAdapter>();
        myAdapters[0].Initialize(0);
        myAdapters[1].Initialize(1);

        GameStateManager.onSetControls += SetControllerAdapter;
        GameStateManager.onEndDialogue += EndDialogue;
        GameStateManager.onPreLoadLevel += UnSub;

        if (whoseChoice == 0)
            theSpeaker = Speaker.Ops;
        else
            theSpeaker = Speaker.Doc;
    }

    void Start()
    {
        _fsm = new FSM<DialogueChoice>(this);

        _fsm.TransitionTo<Standby>();

        myEvent = transform.parent.GetComponent<IDialogueEvent>();
        if (whoseChoice == 0)
        {
            outputChoice1 = GameObject.Find("Canvas").transform.Find("C1 Choice 1").GetComponent<Text>();
            outputChoice2 = GameObject.Find("Canvas").transform.Find("C1 Choice 2").GetComponent<Text>();
            lBIcon = GameObject.Find("Canvas").transform.Find("LBIcon-Left").gameObject;
            rBIcon = GameObject.Find("Canvas").transform.Find("RBIcon-Left").gameObject;
            //label = GameObject.Find("Canvas").transform.Find("C1 Label").GetComponent<Text>();
        }
        else
        {
            outputChoice1 = GameObject.Find("Canvas").transform.Find("C2 Choice 1").GetComponent<Text>();
            outputChoice2 = GameObject.Find("Canvas").transform.Find("C2 Choice 2").GetComponent<Text>();
            lBIcon = GameObject.Find("Canvas").transform.Find("LBIcon-Right").gameObject;
            rBIcon = GameObject.Find("Canvas").transform.Find("RBIcon-Right").gameObject;
            //label = GameObject.Find("Canvas").transform.Find("C2 Label").GetComponent<Text>();
        }
        next = GetAudio();
        myDM = transform.root.gameObject.GetComponent<DialogueManager>();
    }

    void Update()
    {
        _fsm.Update();
    }


    //--------------------
    // Specific functionality
    //--------------------

    private AudioSource GetAudio()
    {
        AudioSource a = gameObject.AddComponent<AudioSource>();
        a.clip = Resources.Load<AudioClip>("SFX/NextSound");
        a.volume = .03f;
        a.pitch = .5f;
        a.loop = false;
        a.playOnAwake = false;
        a.Stop();
        return a;
    }


    //--------------------
    // IDialogue implementaiton
    //--------------------

    private void Spoken()
    {

    }

    private void Cleanup()
    {

    }

    private void Stopped()
    {

    }

    private void Inactive()
    {
        return;
    }

    public void DebugPrint()
    {
        //Debug.Log(Dialogue);
    }


    //--------------------
    // IControllable implemenation
    //--------------------

    public void LeftStick(float leftRight, float upDown, int pNum) { }

    public void RightStick(float leftRight, float upDown, int pNum) { }

    public void AButton(bool pushRelease, int pNum) { }

    public void YButton(bool pushRelease, int pNum) { }

    public void LeftBumper(bool pushRelease, int pNum)
    {
        if (pNum == whoseChoice && pushRelease && ((BasicState)_fsm.CurrentState).name == "SpokenState")
        {
            next.Play();

            TriggerChoice(0);
            StateChoices(dialogueStates.cleanup);
        }
    }

    public void RightBumper(bool pushRelease, int pNum)
    {
        if (pNum == whoseChoice && pushRelease && ((BasicState)_fsm.CurrentState).name == "SpokenState")
        {
            next.Play();

            TriggerChoice(1);
            StateChoices(dialogueStates.cleanup);
        }
    }

    public void SetControllerAdapter(int player, Controllables myControllable)
    {
        if (myAdapters[player] != null)
        {
            if (myControllable == Controllables.dialogue)
                myAdapters[player].enabled = true;
        }
    }
    public void EndDialogue(int player, Controllables myControllable)
    {
        if (myAdapters[player] != null)
        {
            myAdapters[player].enabled = false;
        }
    }

    private void UnSub()
    {
        GameStateManager.onSetControls -= SetControllerAdapter;
        GameStateManager.onEndDialogue -= EndDialogue;
        GameStateManager.onPreLoadLevel -= UnSub;
    }

    private void TriggerChoice(int choiceNum)
    {
        if (choiceNum == 0)
        {
            StartCoroutine(TriggerChoice1Coroutine());
        }
        else if (choiceNum == 1)
        {
            StartCoroutine(TriggerChoice2Coroutine());
            //Debug.Log("Triggering choice 2");
        }
    }


    //--------------------
    // Coroutines
    //--------------------

    private IEnumerator TriggerChoice1Coroutine()
    {
        yield return new WaitForSeconds(.4f);
        for (int i = 0; i < myDM.myEventsNames.Count; i++)
        {
            if (myDM.myEventsNames[i] == eventChoice1.name)
            {
                myDM.myEvents[i].TRIGGER = true;
                yield break;
            }

        }
    }

    private IEnumerator TriggerChoice2Coroutine()
    {
        yield return new WaitForSeconds(.4f);
        for (int i = 0; i < myDM.myEventsNames.Count; i++)
        {
            if (myDM.myEventsNames[i] == eventChoice2.name)
            {
                myDM.myEvents[i].TRIGGER = true;
                yield break;
            }
        }
    }

    //------------------------------------------------
    // States
    //------------------------------------------------

    private class BasicState : FSM<DialogueChoice>.State
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
        public override void Init()
        {
            name = "SpeakingState";
        }

        public override void OnEnter()
        {

        }

        public override void Update()
        {
            TransitionTo<SpokenState>();
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
            Debug.Log("In Choice SpokenState");
        }

        public override void OnEnter()
        {
            if (!Context.colorFlip)
            {
                Context.colorFlip = true;
                Context.outputChoice1.color = Context.outputChoice2.color = Context.myColor;
            }
            Context.outputChoice1.text = Context.Choice1;
            Context.outputChoice2.text = Context.Choice2;
            Context.lBIcon.GetComponent<Image>().enabled = Context.rBIcon.GetComponent<Image>().enabled = true;

            if (Context.whoseChoice == 0)
                EventManager.instance.Fire(new P1_DialogueChoiceRumble_GE());
            if (Context.whoseChoice == 1)
                EventManager.instance.Fire(new P2_DialogueChoiceRumble_GE());
        }

        public override void Update()
        {

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
            Context.outputChoice1.text = Context.outputChoice2.text = "";
            Context.lBIcon.GetComponent<Image>().enabled = Context.rBIcon.GetComponent<Image>().enabled = false;
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
public enum dialogueStates { speaking, spoken, complete, cleanup, inactive, stopped };
