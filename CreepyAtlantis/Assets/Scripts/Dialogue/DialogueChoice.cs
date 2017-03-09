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

    private ControllerAdapter[] myAdapters;
    
    private bool colorFlip;

    private AudioSource next;

    private float timer;
    private bool timerFlip;

    private IDialogueEvent myEvent;

    private DialogueManager myDM;


    //--------------------
    // State machine
    //--------------------

    public void StateChoices(dialogueStates dS)
    {
        switch (dS)
        {
            case (dialogueStates.speaking):
                StateChoices(dialogueStates.spoken);
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
    }

    void Start()
    {
        myEvent = transform.parent.GetComponent<IDialogueEvent>();
        if (whoseChoice == 0)
        {
            outputChoice1 = GameObject.Find("Canvas").transform.Find("C1 Choice 1").GetComponent<Text>();
            outputChoice2 = GameObject.Find("Canvas").transform.Find("C1 Choice 2").GetComponent<Text>();
        }
        else
        {
            outputChoice1 = GameObject.Find("Canvas").transform.Find("C2 Choice 1").GetComponent<Text>();
            outputChoice2 = GameObject.Find("Canvas").transform.Find("C2 Choice 2").GetComponent<Text>();
        }
        next = GetAudio();
        myDM = transform.root.gameObject.GetComponent<DialogueManager>();
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState();
            timer += Time.deltaTime;
            if (dialogueAdvanceTime != 0 && timer >= dialogueAdvanceTime && !timerFlip)
            {
                currentState = Cleanup;
            }
        }
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
        if (!colorFlip)
        {
            colorFlip = true;
            outputChoice1.color = outputChoice2.color = myColor;
        }
        outputChoice1.text = Choice1;
        outputChoice2.text = Choice2;
    }

    private void Cleanup()
    {
        timerFlip = true;
        outputChoice1.text = outputChoice2.text = "";
        //Debug.Log("in clean up");
        myEvent.NextLine();
        StateChoices(dialogueStates.inactive);
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
        if (pNum == whoseChoice && pushRelease && currentState == Spoken)
        {
            next.Play();

            TriggerChoice(0);
            StateChoices(dialogueStates.cleanup);
        }
    }

    public void RightBumper(bool pushRelease, int pNum)
    {
        if (pNum == whoseChoice && pushRelease && currentState == Spoken)
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
}
public enum dialogueStates { speaking, spoken, cleanup, inactive };
