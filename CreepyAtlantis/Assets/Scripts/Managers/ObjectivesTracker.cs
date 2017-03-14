using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Objective: MonoBehaviour, IObjective
{
    [HideInInspector] public string uniqueID;
    public delegate void OnComplete();
    public OnComplete myOC;

    [HideInInspector] public bool complete;

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
        complete = true;
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

    //Will be called for each Objective in a scene at Start.
    //Each Objective will pass itself to the OM see its status compared to stored status.
    //If stored reference says it's complete, it will call it's OnComplete
    //If it's the first time that it's passed, it will store its info for future reference.
    public void ObjectiveCheck(Objective myO)
    {
        bool exists = false;
        foreach(Objective o in Objectives)
        {
            if (o.uniqueID == myO.uniqueID)
            {
                if (o.complete)
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
        }
    }

    public void ObjectiveUpdate(Objective myO)
    {
        bool exists = false;
        foreach (Objective o in Objectives)
        {
            if (o.uniqueID == myO.uniqueID)
            {
                if (o.complete)
                {
                    myO.complete = true;
                    exists = true;
                    break;
                }
            }
        }
        if (!exists)
        {
            Debug.Log("Unregistered Objective Completed.");
        }
    }
}
