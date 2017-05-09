using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type_GameObjective { InteractDiscrete, InteractOver, InteractOver_Sub }
//-----------------------------------------
// Created - base GObjv created (happen in Awake)
// Subscription of substeps should happen before Initialization.  Sequenced by Manager.
// Initialized - checked with manager to see if previously completed (happen in Start)
// Active - when a GObjv goes Active, generally means that indicator is on, description is displayed, etc.
// Triggered - interstitial step that fires effects, or enables the next in the chain
// Completed - intended as point at which follow-on objectives can become active, etc.
//           - happens when all requirements are met
// CleanUp - either delete, turn off interactivity, etc.
public enum Status_GameObjective { Created, Initialized, Active, Triggered, Completed, CleanedUp }

public class GameObjectiveEvent : GameEvent
{
    public GameObjective GObjv;
    public GameObjectiveEvent(GameObjective o)
    {
        GObjv = o;
    }
}

public class GameObjective {

    public string iD;
    public string label;
    public string description;

    public Type_GameObjective myType;

    private Status_GameObjective _status;
    public Status_GameObjective status
    {
        get
        {
            return _status;
        }
        set
        {
            if (value != _status)
            {
                _status = value;
                _ManagerUpdate();
                //Debug.Log("my new status is: " + _status.ToString());
                EventManager.instance.Fire(new GameObjectiveEvent(this));
            }
        }
    }

    public void Init (string id, string l, string d)
    {
        iD = id;
        label = l;
        description = d;
        status = Status_GameObjective.Created;
    }

    public void ManagerCheckIn ()
    {
        // placeholder
        // happens in start after created in the obj.
        //Debug.Log("Checking in");
        status = GameObject.Find("GameStateManager").GetComponent<GameObjectiveManager>().GameObjectiveCheckIn(this);
    }

    private void _ManagerUpdate ()
    {
        // checks in everytime the status changes.
        GameObject.Find("GameStateManager").GetComponent<GameObjectiveManager>().ObjectiveUpdate(this);
    }
}

#region Child GameObjectives

public class InteractDiscrete_GameObjective: GameObjective
{
    public InteractDiscrete_GameObjective(string id, string l, string d)
    {
        myType = Type_GameObjective.InteractDiscrete;
        Init(id, l, d);
    }
}

public class InteractOver_GameObjective: GameObjective
{
    public List<InteractOver_Sub_GameObjective> listOfSubs = new List<InteractOver_Sub_GameObjective>();
    public int countTotal;
    private int _countProgres;
    public int countProgress
    {
        get
        {
            return _countProgres;
        }
        set
        {
            if (value != _countProgres)
            {
                _countProgres = value;
                if (countTotal == _countProgres)
                {
                    status = Status_GameObjective.Triggered;
                }
            }
        }
    }

    public InteractOver_GameObjective(string id, string l, string d)
    {
        Init(id, l, d);
        myType = Type_GameObjective.InteractOver;
    }
    public void SubscribeToThisIMGO (InteractOver_Sub_GameObjective o)
    {
        listOfSubs.Add(o);
        countTotal = listOfSubs.Count;
    }

    public void UpdateMySubs (GameEvent e)
    {
        int completeSubs = 0;

        if(e.GetType() == typeof(GameObjectiveEvent))
        {
            GameObjectiveEvent GOE = (GameObjectiveEvent) e;
            foreach(InteractOver_Sub_GameObjective sub in listOfSubs)
            {
                if (sub.iD == GOE.GObjv.iD)
                    completeSubs++;
            }
        }
        countProgress = completeSubs;
    }
}

public class InteractOver_Sub_GameObjective: GameObjective
{
    private string iDOfIMGOToSubscribeTo;

    public InteractOver_Sub_GameObjective(string id, string l, string d, string idToSub)
    {
        Init(id, l, d);
        myType = Type_GameObjective.InteractOver_Sub;
        iDOfIMGOToSubscribeTo = idToSub;
    }
}

#endregion
