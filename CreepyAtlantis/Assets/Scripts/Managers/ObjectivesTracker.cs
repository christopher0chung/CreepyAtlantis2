﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Objective: MonoBehaviour, IObjective
{
    [HideInInspector] public string uniqueID;
    public delegate void OnComplete();
    public OnComplete myOC;

    private bool _complete;
    [HideInInspector] public bool complete
    {
        get
        {
            return _complete;
        }
        set
        {
            if (value != _complete)
            {
                _complete = value;
                if (_complete)
                {
                    myOC.Invoke();
                }
            }
        }
    }

    public UnityEvent OnActivate;
    public UnityEvent OnTrigger;

    public void Init (string uID, OnComplete oC)
    {
        uniqueID = uID;
        myOC = oC;
        complete = false;
    }

    public virtual void Activate()
    {
        OnActivate.Invoke();
    }
    public virtual void Trigger()
    {
        OnTrigger.Invoke();
        GameObject.FindGameObjectWithTag("Managers").GetComponent<ObjectivesTracker>().ObjectiveUpdate(this);
    }
}

public interface IObjective
{
    void Activate();
    void Trigger();
}

public class ObjectivesTracker : MonoBehaviour {

    private static ObjectivesTracker _instance;
    public static ObjectivesTracker instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ObjectivesTracker();
            }
            return _instance;
        }
    }

    //public void ObjectivesUpdate(string detail)
    //{
    //    if (GetComponent<LevelLoader>().LEVEL == 1)
    //    {
    //        if (detail == "LAST")
    //        {
    //            GetComponent<LevelLoader>().LoadLevel(2);
    //        }
    //    }
    //}

    public List<Objective> Objectives = new List<Objective>();
    public List<string> ObjectivesIDs = new List<string>();
    public List<bool> ObjectivesComplete = new List<bool>();

    //Will be called for each Objective in a scene at Start.
    //Each Objective will pass itself to the OM see its status compared to stored status.
    //If stored reference says it's complete, it will call it's OnComplete
    //If it's the first time that it's passed, it will store its info for future reference.
    public void ObjectiveCheck(Objective myO)
    {
        bool exists = false;
        for (int i = 0; i < ObjectivesIDs.Count; i++)
        {
            if (ObjectivesIDs[i] == myO.uniqueID)
            {
                if (ObjectivesComplete[i])
                {
                    myO.myOC();
                    exists = true;
                    break;
                }
            }
        }
        if (!exists)
        {
            Objectives.Add(myO);
            ObjectivesIDs.Add(myO.uniqueID);
            ObjectivesComplete.Add(myO.complete);
        }
    }

    public void ObjectiveUpdate(Objective myO)
    {
        bool exists = false;
        for (int i = 0; i < ObjectivesIDs.Count; i++)
        {
            if (ObjectivesIDs[i] == myO.uniqueID)
            {
                if (ObjectivesComplete[i])
                {
                    myO.complete = true;
                    exists = true;
                    break;
                }
                else
                {
                    exists = true;
                    ObjectivesComplete[i] = true;
                    Objectives[i].complete = true;
                    myO.complete = true;
                    break;
                }
            }
            else
                continue;
        }
        if (!exists)
        {
            Debug.Log("Unregistered Objective Completed.");
        }
    }
}