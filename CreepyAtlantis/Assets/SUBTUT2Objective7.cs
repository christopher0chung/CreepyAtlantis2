using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SUBTUT2Objective7 : Objective
{
    //Triggered when far right at the end.

    public override void Activate()
    {
        Init("SUBTUT2Objective7" + gameObject.name, Complete);
        GameObject.FindGameObjectWithTag("Managers").GetComponent<ObjectivesTracker>().ObjectiveCheck(this);
        Invoke("MakeTrigger", .2f);
    }

    void Complete()
    {
        Destroy(this.gameObject);
    }

    public override void Trigger()
    {
        GameObject.FindGameObjectWithTag("Managers").GetComponent<LevelLoader>().LoadLevel(3);

        base.Trigger();
    }

    void MakeTrigger()
    {
        if (GetComponent<SphereCollider>() == null)
        {
            SphereCollider myCol = gameObject.AddComponent<SphereCollider>();
            myCol.radius = 1f;
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