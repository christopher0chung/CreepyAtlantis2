using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add01StartObjective : Objective {

    void Start()
    {
        Init("ADD01Start", Complete);
        GameObject.FindGameObjectWithTag("Managers").GetComponent<ObjectivesTracker>().ObjectiveCheck(this);
    }

    void Complete()
    {
        Destroy(this.gameObject);
    }

    public override void Trigger()
    {
        base.Trigger();
        GameObject.FindGameObjectWithTag("Managers").GetComponent<LevelLoader>().LoadLevel(2);
    }

    void MakeTrigger()
    {
        SphereCollider myCol = gameObject.AddComponent<SphereCollider>();
        myCol.radius = 100;
        myCol.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.name == "Sub")
        {
            other.transform.parent.GetComponent<SubController>().canMove = false;
            other.transform.parent.GetComponent<SubController>().canGetOut = true;
            Trigger();
        }
    }

}
