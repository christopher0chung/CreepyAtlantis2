using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueChoice : MonoBehaviour, IDialogue, IControllable{

    public int whoseChoice;

    public string Choice1;
    public string Choice2;

    private Text outputChoice1;
    private Text outputChoice2;

    public delegate void stateManager();
    public stateManager currentState;

    public ControllerAdapter[] myAdapters;
    public ObjectivesTracker myOT;

    public Color myColor;
    private bool colorFlip;

    private AudioSource next;

    private float timer;
    public float dialogueAdvanceTime;
    private bool timerFlip;

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

    private IDialogueEvent myEvent;

    public GameObject eventChoice1;
    public GameObject eventChoice2;

    private DialogueManager myDM;

    void Awake()
    {
        gameObject.AddComponent<ControllerAdapter>();
        gameObject.AddComponent<ControllerAdapter>();
        myAdapters = GetComponents<ControllerAdapter>();
        myAdapters[0].Initialize(0);
        myAdapters[1].Initialize(1);

        GameStateManager.onSetControls += SetControllerAdapter;
        GameStateManager.onEndDialogue += SetControllerAdapter;

        myOT = GameObject.Find("GameStateManager").GetComponent<ObjectivesTracker>();
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
        next = GetComponent<AudioSource>();
        //StateChoices(dialogueStates.speaking);
        myDM = transform.root.gameObject.GetComponent<DialogueManager>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
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
        next.Play();
        StateChoices(dialogueStates.inactive);
        myOT.ObjectivesUpdate(gameObject.name);
    }

    private void Inactive()
    {
        return;
    }

    public void DebugPrint()
    {
        //Debug.Log(Dialogue);
    }

    public void LeftStick(float leftRight, float upDown, int pNum) { }

    public void RightStick(float leftRight, float upDown, int pNum) { }

    public void AButton(bool pushRelease, int pNum) { }

    public void LeftBumper(bool pushRelease, int pNum)
    {
        //Debug.Log(pNum);
        if (pNum == whoseChoice && pushRelease)
        {
            TriggerChoice(0);
            StateChoices(dialogueStates.cleanup);
        }
    }

    public void RightBumper(bool pushRelease, int pNum)
    {
        //Debug.Log(pNum);
        if (pNum == whoseChoice && pushRelease)
        {
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
            else
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
            StartCoroutine(TriggerChoice1Coroutine());
        }
    }

    private IEnumerator TriggerChoice1Coroutine()
    {
        yield return new WaitForSeconds(.4f);
        for (int i = 0; i < myDM.myEventsNames.Count; i++)
        {
            if (myDM.myEventsNames[i] == eventChoice1.name)
                myDM.myEvents[i].TRIGGER = true;
        }
    }

    private IEnumerator TriggerChoice2Coroutine()
    {
        yield return new WaitForSeconds(.4f);
        for (int i = 0; i < myDM.myEventsNames.Count; i++)
        {
            if (myDM.myEventsNames[i] == eventChoice2.name)
                myDM.myEvents[i].TRIGGER = true;
        }
    }
}
public enum dialogueStates { speaking, spoken, cleanup, inactive };
