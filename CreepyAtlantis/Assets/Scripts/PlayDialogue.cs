using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayDialogue : MonoBehaviour, IDialogue {

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentState == Speaking)
                StateChoices(dialogueStates.spoken);
            else if (currentState == Spoken)
                StateChoices(dialogueStates.cleanup);
        }
    }

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
}

public enum dialogueStates { speaking, spoken, cleanup, inactive };
