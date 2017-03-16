using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SUBTUT2Objective6 : Objective
{
    private int _progressVal;
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
        GameObject TA01 = GameObject.Find("ToAdd01");
        TA01.SetActive(true);
        TA01.GetComponent<IObjective>().Activate();
        GameObject.Find("DialogueManager").GetComponent<DialogueManager>().FireEvent(myDialogueEventToCall);
        base.Trigger();
    }
}