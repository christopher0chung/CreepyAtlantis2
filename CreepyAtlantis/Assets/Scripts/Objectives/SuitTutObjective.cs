using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitTutObjective : Objective {

    void Start()
    {
        Init("CSLLastDialogueLine", Complete);
        GameObject.FindGameObjectWithTag("Managers").GetComponent<ObjectivesTracker>().ObjectiveCheck(this);
    }

    void Complete()
    {

    }

    public override void Trigger()
    {
        base.Trigger();
        GameObject.Find("Sub").GetComponent<SubController>().canMove = false;
        GameObject.Find("Sub").GetComponent<SubController>().canGetOut = true;
    }

    public override void Activate()
    {
        base.Activate();
    }
}
