using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SUBTUT2Objective0 : Objective {

    //Triggered at the end of first dialogue event.
    //Sets up the next two Objectives.
    void Start()
    {
        Init("SUBTUTObjective0", Complete);
        GameObject.FindGameObjectWithTag("Managers").GetComponent<ObjectivesTracker>().ObjectiveCheck(this);
    }

    void Complete()
    {

    }

    public override void Trigger()
    {
        GameObject.Find("Sub").GetComponent<SubController>().canMove = true;

        GameObject.FindGameObjectWithTag("Managers").GetComponent<GameStateManager>().SetControls(0, Controllables.submarine);
        GameObject.FindGameObjectWithTag("Managers").GetComponent<GameStateManager>().SetControls(1, Controllables.none);

        GameObject[] objectives = GameObject.FindGameObjectsWithTag("Objectives");
        foreach (GameObject obj in objectives)
        {
            //Debug.Log(obj.name);

            if (obj.name == "SubLOps" || obj.name == "SubROps")
            {
                obj.SetActive(true);
                obj.GetComponent<IObjective>().Activate();
            }
        }

        base.Trigger();
        Debug.Log("at the end of trigger");
    }
}
