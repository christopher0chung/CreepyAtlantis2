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
        Destroy(this.gameObject);
    }

    public override void Trigger()
    {
        myDM.FireEvent(myDM.ReturnEventIndex(whatDEToFire));
        GameObject.Find("Sub").GetComponent<SubController>().canMove = true;
        GameObject.Find("Sub").GetComponent<SubController>().canGetOut = true;
        base.Trigger();
    }

    public override void Activate()
    {
        base.Activate();
    }
}
