using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add01StartObjective : Objective {

    void Start()
    {
        Init("ADD01Start", Complete);
        GameObject.FindGameObjectWithTag("Managers").GetComponent<ObjectivesTracker>().ObjectiveCheck(this);
        Invoke("MakeTrigger", 2f);
    }

    void Complete()
    {
        Destroy(this.gameObject);
    }

    public override void Trigger()
    {
        base.Trigger();
        GameObject.Find("DialogueManager").GetComponent<DialogueManager>().FireEvent(0);
    }

    void MakeTrigger()
    {
        SphereCollider myCol = gameObject.AddComponent<SphereCollider>();
        myCol.radius = 100;
        myCol.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("AHit");
        if (other.name == "Sub" && other.GetComponent<SubController>() != null)
        {
            Debug.Log("Hit something on sub");

            other.GetComponent<SubController>().canMove = false;
            other.GetComponent<SubController>().canGetOut = false;
            Trigger();
        }
    }

}
