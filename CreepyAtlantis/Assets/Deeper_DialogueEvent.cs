using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deeper_DialogueEvent : MonoBehaviour {

    private Deeper_DialogueLine[] myLines;
    private DialogueEventStatus status;

	void Start () {
        myLines = new Deeper_DialogueLine[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            myLines[i] = transform.GetChild(i).GetComponent<Deeper_DialogueLine>();
        }

        EventManager.instance.Register<GE_DialogueManager_Status>(EventFunc);
        EventManager.instance.Register<GE_Dia_Event>(EventFunc);

        status = DialogueEventStatus.Standby;
	}

    public void Fire()
    {
        EventManager.instance.Fire(new GE_Dia_Line(myLines[0].speaker, myLines[0].line, DialogueLineAction.Play, myLines[0].gameObject.name));
        EventManager.instance.Fire(new GE_Dia_Event(DialogueEventStatus.Active, this.gameObject.name));
    }

    void EventFunc(GameEvent e)
    {
        if (e.GetType() == typeof(GE_DialogueManager_Status))
        {
            GE_DialogueManager_Status d = (GE_DialogueManager_Status)e;
            if (d.status == DialogueLineStatus.Complete)
            {
                // if the last line in this event is completed, it is switched to complete
                if (d.audioFileName == myLines[myLines.Length - 1].gameObject.name)
                {
                    EventManager.instance.Fire(new GE_Dia_Event(DialogueEventStatus.Complete, this.gameObject.name));
                }
                // if any line in the event is fired, try to fire the next one
                for (int i = 0; i < myLines.Length; i++)
                {
                    if (d.audioFileName == myLines[i].gameObject.name)
                    {
                        if (i + 1 <= myLines.Length - 1)
                        {
                            EventManager.instance.Fire(new GE_Dia_Line(myLines[i + 1].speaker, myLines[i + 1].line, DialogueLineAction.Play, myLines[i + 1].gameObject.name));
                        }
                        else
                            return;
                    }
                }
            }
        }
        else if (e.GetType() == typeof(GE_Dia_Event))
        {
            GE_Dia_Event d = (GE_Dia_Event)e;
            if (d.eventName == this.gameObject.name)
            {
                status = d.status;
            }
        }
    }
}
