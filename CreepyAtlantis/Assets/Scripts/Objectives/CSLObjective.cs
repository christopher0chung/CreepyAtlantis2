using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSLObjective : Objective {

    void Start()
    {
        Init ("CSLLastDialogueLine", Complete);
        GameObject.FindGameObjectWithTag("Managers").GetComponent<ObjectivesTracker>().ObjectiveCheck(this);
    }

    void Complete()
    {

    }

    public override void Trigger()
    {
        base.Trigger();
        GameObject.FindGameObjectWithTag("Managers").GetComponent<LevelLoader>().LoadLevel(2);
    }

    public override void Activate()
    {
        base.Activate();
    }
}
