using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitTutObjective : Objective {

    void Start()
    {
        Init("SuitTutObjective" + gameObject.name, Complete);
        GameObject.FindGameObjectWithTag("Managers").GetComponent<ObjectivesTracker>().ObjectiveCheck(this);
    }

    void Complete()
    {

    }

    public override void Trigger()
    {
        GameObject.Find("Sub").GetComponent<SubController>().canMove = false;
        GameObject.Find("Sub").GetComponent<SubController>().canGetOut = true;
        base.Trigger();
    }

    public override void Activate()
    {
        base.Activate();
    }
}
