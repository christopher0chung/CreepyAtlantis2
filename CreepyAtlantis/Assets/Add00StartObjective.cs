using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add00StartObjective : Objective {

    // Starts the first dialogue event.

    void Start()
    {
        Init("ADD00Start", Complete);
        GameObject.FindGameObjectWithTag("Managers").GetComponent<ObjectivesTracker>().ObjectiveCheck(this);
        Invoke("Trigger", 1.5f);
    }

    void Complete()
    {
        Destroy(this.gameObject);
    }

    public override void Trigger()
    {
        GameObject.Find("DialogueManager").GetComponent<DialogueManager>().FireEvent(0);
        base.Trigger();
    }
}
