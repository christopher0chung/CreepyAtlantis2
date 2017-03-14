using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SUBTUT2Objective2 : Objective
{
    //Triggered after Ops gets left and right.

    public int _progressVal;
    public int progressVal
    {
        get
        {
            return _progressVal;
        }
        set
        {
            if (value != _progressVal)
            {
                _progressVal = value;
                if (_progressVal == 2)
                {
                    Trigger();
                }
            }
        }
    }

    public int myDialogueEventToCall;

    void Start()
    {
        Init("SUBTUT2Objective2", Complete);
        GameObject.FindGameObjectWithTag("Managers").GetComponent<ObjectivesTracker>().ObjectiveCheck(this);
    }

    void Complete()
    {
        Destroy(this.gameObject);
    }

    public override void Trigger()
    {
        GameObject wPA = GameObject.Find("WaypointAlpha");
        wPA.SetActive(true);
        wPA.GetComponent<IObjective>().Activate();
        GameObject.Find("DialogueManager").GetComponent<DialogueManager>().FireEvent(myDialogueEventToCall);
        base.Trigger();
    }
}