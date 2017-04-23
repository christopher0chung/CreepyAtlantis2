using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type_GameObjective { Collect, Find, InteractSingle, InteractMultiple, Time, Collect_SubObjv, InteractMultiple_SubObjv }
//-----------------------------------------
// Created - base GObjv created (happen in Awake)
// Subscription of substeps should happen before Initialization.  Sequenced by Manager.
// Initialized - checked with manager to see if previously completed (happen in Start)
// Active - when a GObjv goes Active, generally means that indicator is on, description is displayed, etc.
// Completed - intended as point at which follow-on objectives can become active, etc.
//           - happens when all requirements are met
// CleanUp - either delete, turn off interactivity, etc.
public enum Status_GameObjective { Created, Initialized, Active, Completed, CleanedUp }

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

    public Status_GameObjective ManagerCheckIn (GameObjective o)
    {
        // placeholder
        return Status_GameObjective.Initialized;
    }
}

public class InteractSingle_GameObjective: GameObjective
{
    public InteractSingle_GameObjective (string id, string l, string d)
    {
        Init(id, l, d);
        myType = Type_GameObjective.InteractSingle;
        status = ManagerCheckIn(this);
    }
}

public class InteractMultiple_SubObjective_GameObjective: GameObjective
{
    public InteractMultiple_SubObjective_GameObjective(string id, string l, string d)
    {
        Init(id, l, d);
        myType = Type_GameObjective.InteractMultiple_SubObjv;
    }
}

public class InteractMultiple_GameObjective: GameObjective
{
    public InteractMultiple_GameObjective(string id, string l, string d)
    {
        Init(id, l, d);
        myType = Type_GameObjective.InteractMultiple;
    }

}
