using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectiveManager : MonoBehaviour {

    void Awake()
    {
        EventManager.instance.Register<GameSaveEvent>(SaveObjectives);
    }

    public List<GameObjective> ObjectivesList = new List<GameObjective>();

    private List<GameObjective> _SavedObjectivesList = new List<GameObjective>();

    public Status_GameObjective GameObjectiveCheckIn(GameObjective o)
    {
        for (int i = 0; i < ObjectivesList.Count; i++)
        {
            if (ObjectivesList[i].iD == o.iD)
            {
                return ObjectivesList[i].status;
            }
        }
        // Runs if does not exist
        ObjectivesList.Add(o);
        return Status_GameObjective.Initialized;
    }

    public void ObjectiveUpdate(GameObjective o)
    {
        for (int i = 0; i < ObjectivesList.Count; i++)
        {
            if (ObjectivesList[i].iD == o.iD)
            {
                ObjectivesList[i].status = o.status;
            }
        }
    }

    private void SaveObjectives(GameEvent e)
    {
        _SavedObjectivesList = ObjectivesList;
    }

    private void LoadObjectives(GameEvent e)
    {
        ObjectivesList = _SavedObjectivesList;
    }
}


