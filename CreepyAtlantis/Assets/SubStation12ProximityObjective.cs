using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubStation12ProximityObjective : Objective
{

    public DialogueEvents whatDEToFire;

    private DialogueManager myDM;

    void Start()
    {
        Init("SubStation12ProximityObjective", Complete);
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
        base.Trigger();
    }

    public override void Activate()
    {
        base.Activate();
    }
}
