using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelpObjectiveDialogueDone : Objective
{

    void Start()
    {
        Init("KelpObjectiveOnRemove", Complete);
        GameObject.FindGameObjectWithTag("Managers").GetComponent<ObjectivesTracker>().ObjectiveCheck(this);
    }

    void Complete()
    {

    }

    public override void Trigger()
    {
        GameObject.Find("Sub").GetComponent<SubController>().canMove = true;
        GameObject.Find("Sub").GetComponent<SubController>().canGetOut = true;
        base.Trigger();
    }

    public override void Activate()
    {
        base.Activate();
    }
}
