﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSLObjective : Objective {

    void Start()
    {
        Init ("CSLLastDialogueLine" + gameObject.name, Complete);
        GameObject.FindGameObjectWithTag("Managers").GetComponent<ObjectivesTracker>().ObjectiveCheck(this);
    }

    void Complete()
    {

    }

    public override void Trigger()
    {
        //GameObject.FindGameObjectWithTag("Managers").GetComponent<LevelLoader>().LoadLevel(3);
        EventManager.instance.Fire(new GE_LoadLevelRequest(3));
        base.Trigger();
    }

    public override void Activate()
    {
        base.Activate();
    }
}
