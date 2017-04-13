using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDialoguePlay : MonoBehaviour {

    private bool start;
    private float timer;

    public DialogueEvents myDE;
    public float delayTime;
    private DialogueManager myDM;

	// Use this for initialization
	void Start () {
        myDM = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (start)
        {
            timer += Time.deltaTime;
            if (timer >= delayTime)
            {
                myDM.FireEvent(myDM.ReturnEventIndex(myDE));
                GetComponent<Objective>().Trigger();
            }
        }
	}

    public void StartTimer()
    {
        start = true;
    }
}
