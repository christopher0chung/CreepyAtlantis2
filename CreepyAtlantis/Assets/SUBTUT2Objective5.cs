using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SUBTUT2Objective5 : Objective
{

    //Triggered when left and right touched by Ops.

    public int myDialogueEventToCall;

    public override void Activate()
    {
        Init("SUBTUT2Objective1" + gameObject.name, Complete);
        GameObject.FindGameObjectWithTag("Managers").GetComponent<ObjectivesTracker>().ObjectiveCheck(this);
        Invoke("MakeTrigger", .2f);
    }

    void Complete()
    {
        Destroy(this.gameObject);
    }

    public override void Trigger()
    {
        GameObject.Find("DialogueManager").GetComponent<DialogueManager>().FireEvent(myDialogueEventToCall);

        GameObject.Find("SubToAdd01").GetComponent<SUBTUT2Objective6>().progressVal++;

        base.Trigger();
    }

    void MakeTrigger()
    {
        if (GetComponent<SphereCollider>() == null)
        {
            SphereCollider myCol = gameObject.AddComponent<SphereCollider>();
            myCol.radius = 2.5f;
            myCol.isTrigger = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("AHit");
        if (other.name == "Sub" && other.GetComponent<SubController>() != null)
        {
            Debug.Log("Hit something on sub");
            Trigger();
        }
    }
}