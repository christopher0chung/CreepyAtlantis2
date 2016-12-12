﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour {

    public class DialogueEventClass
    {
        public IDialogueEvent Script;
        public string DEventName;
        private bool Trigger;
        public bool TRIGGER
        {
            get
            {
                return Trigger;
            }
            set
            {
                if (value != Trigger)
                {
                    Trigger = value;
                    Script.StartLines();
                }
            }
        }
        public DialogueEventClass (GameObject me, IDialogueEvent myEvent, int i)
        {
            Script = myEvent;
            DEventName = me.transform.GetComponent<DialogueManager>().myEventsNames[i];
            Trigger = false;
        }
    }

    public List<DialogueEventClass> myEvents = new List<DialogueEventClass>();

    public List<string> myEventsNames = new List<string>();

    // Use this for initialization
    void Start()
    {
        GetMyEvents();
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I))
            myEvents[0].TRIGGER = true;
    }

    private void GetMyEvents()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            myEventsNames.Add("Event #: " + i + " - " + (transform.GetChild(i).gameObject.name));
            myEvents.Add(new DialogueEventClass(this.gameObject, transform.GetChild(i).GetComponent<IDialogueEvent>(), i));
            //Debug.Log(myEvents[i].DEventName);
        }
    }
}