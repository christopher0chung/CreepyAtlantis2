using UnityEngine;
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
                    if (Trigger)
                    {
                        Script.StartLines();
                        GameObject.Find("GameStateManager").GetComponent<GameStateManager>().SetControls(0, Controllables.dialogue);
                        GameObject.Find("GameStateManager").GetComponent<GameStateManager>().SetControls(1, Controllables.dialogue);
                    }
                    else
                    {
                        Script.StopLines();
                    }
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

    private void GetMyEvents()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            myEventsNames.Add((transform.GetChild(i).gameObject.name));
            myEvents.Add(new DialogueEventClass(this.gameObject, transform.GetChild(i).GetComponent<IDialogueEvent>(), i));
            //Debug.Log(myEvents[i].DEventName);
        }
    }

    public int ReturnEventIndex(DialogueEvents myDE)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (myEventsNames[i] == myDE.gameObject.name)
            {
                return i;
            }
        }
        return 9999;
    }

    public void FireEvent(int index)
    {
        //test to see if you can force everything to stop before playing the triggered one.
        foreach (DialogueEventClass myE in myEvents)
        {
            myE.TRIGGER = false;
        }

        myEvents[index].TRIGGER = true;
    }
}