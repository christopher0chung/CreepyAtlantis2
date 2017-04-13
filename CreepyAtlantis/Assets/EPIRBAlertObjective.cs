using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPIRBAlertObjective : Objective
{

    void Start()
    {
        Init("EPIRBAlertObjective", Complete);
        GameObject.FindGameObjectWithTag("Managers").GetComponent<ObjectivesTracker>().ObjectiveCheck(this);
    }

    void Complete()
    {
        Destroy(this.gameObject);
    }

    public override void Trigger()
    {
        base.Trigger();
    }

    public override void Activate()
    {
        base.Activate();
    }
}
