using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectiveCompletedEvent : GameEvent
{

}

public class GameObjective {

    public string iD;
    public bool completed;

    public GameObjective (string id)
    {
        iD = id;
    }

    public void Complete()
    {
        completed = true;
        //EventManager.instance.Fire();
    }
}
