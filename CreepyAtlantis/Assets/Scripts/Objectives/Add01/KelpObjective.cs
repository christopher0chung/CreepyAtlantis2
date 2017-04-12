using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelpObjective : Objective {

    public DialogueEvents whatDEToFire;

    private DialogueManager myDM;

    void Start()
    {
        Init("KelpObjectiveOnRemove", Complete);
        myDM = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        GameObject.FindGameObjectWithTag("Managers").GetComponent<ObjectivesTracker>().ObjectiveCheck(this);
    }

    void Complete()
    {

    }

    public override void Trigger()
    {
        myDM.FireEvent(66);
        base.Trigger();
    }

    public override void Activate()
    {
        base.Activate();
    }
}
