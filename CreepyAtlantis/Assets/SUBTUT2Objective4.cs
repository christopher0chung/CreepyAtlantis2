using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SUBTUT2Objective4 : Objective
{
    public int myDialogueEventToCall;

    //Triggered at the end of dialogue saying it is Doc's turn.
    void Start()
    {
        Init("SUBTUT0", Complete);
        GameObject.FindGameObjectWithTag("Managers").GetComponent<ObjectivesTracker>().ObjectiveCheck(this);
    }

    void Complete()
    {
        Destroy(this.gameObject);
    }

    public override void Trigger()
    {
        GameObject.Find("DialogueManager").GetComponent<DialogueManager>().FireEvent(myDialogueEventToCall);

        GameObject SLD = GameObject.Find("SubLDoc");
        SLD.SetActive(true);
        SLD.GetComponent<IObjective>().Activate();
        GameObject SRD = GameObject.Find("SubRDoc");
        SRD.SetActive(true);
        SRD.GetComponent<IObjective>().Activate();

        GameObject.Find("Sub").GetComponent<SubController>().canMove = true;

        base.Trigger();
    }
}
