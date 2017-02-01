﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueChoice : MonoBehaviour, IDialogue, IControllable{

    public int whoseChoice;

    public string Choice1;
    public string Choice2;

    public Text outputChoice1;
    public Text outputChoice2;

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

    private IDialogueEvent eventChoice1;
    private IDialogueEvent eventChoice2;

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
        outputChoice1 = GameObject.Find("Canvas").transform.Find("Choice 1").GetComponent<Text>();
        outputChoice2 = GameObject.Find("Canvas").transform.Find("Choice 2").GetComponent<Text>();
        next = GetComponent<AudioSource>();
        //StateChoices(dialogueStates.speaking);
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
            GetComponent<DialogueManager>().myEvents[0].TRIGGER = true;
            StateChoices(dialogueStates.cleanup);
        }
    }

    public void RightBumper(bool pushRelease, int pNum)
    {
        //Debug.Log(pNum);
        if (pNum == whoseChoice && pushRelease)
        {
            GetComponent<DialogueManager>().myEvents[1].TRIGGER = true;
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
}
public enum dialogueStates { speaking, spoken, cleanup, inactive };