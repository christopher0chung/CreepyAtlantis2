using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkAboutKelpObjective : Objective
{

    public DialogueEvents whatDEToFire;

    private DialogueManager myDM;

    void Start()
    {
        Init("TalkAboutKelp", Complete);
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