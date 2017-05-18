using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GE_Danger : GameEvent{ }

public class GameObjectiveManager : MonoBehaviour {

    public bool subMove;
    public bool ingEg;

    void Awake()
    {
        EventManager.instance.Register<GameSaveEvent>(SaveObjectives);
        EventManager.instance.Register<GameSaveLoadEvent>(LoadObjectives);
        EventManager.instance.Register<GE_Danger>(DangerHandler);
        EventManager.instance.Register<GE_GetSubStatus>(SaveSubStatus);
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

    private void SaveSubStatus(GameEvent e)
    {
        GE_GetSubStatus g = (GE_GetSubStatus)e;

        subMove = g.Move;
        ingEg = g.IngEg;
    }

    private void SaveObjectives(GameEvent e)
    {
        _SavedObjectivesList = ObjectivesList;
    }

    private void LoadObjectives(GameEvent e)
    {
        ObjectivesList = _SavedObjectivesList;
        PushSubStatus(subMove, ingEg);
    }

    private void PushSubStatus ( bool s, bool i)
    {
        StartCoroutine(DelayedSubStatus( s, i));
    }

    private IEnumerator DelayedSubStatus (bool s, bool i)
    {
        yield return new WaitForSeconds(.1f);
        EventManager.instance.Fire(new GE_SubStatus(s, i));
        yield break;
    }

    private void DangerHandler(GameEvent e)
    {
        danger = true;
        Debug.Log("DANGER");
    }

    private float _dangerTimer;

    private bool _danger;
    private bool danger
    {
        get
        {
            return _danger;
        }
        set
        {
            _danger = value;
            if (_danger)
                _dangerTimer = 0;
        }
    }
    private float _timer;

    void Update()
    {
        _dangerTimer += Time.deltaTime;
        _timer += Time.deltaTime;
        if (_dangerTimer >= 5)
        {
            danger = false;
        }
        if (!danger)
        {
            if (_timer >= 1)
            {
                _timer -= 1;
                if (ObjectivesList != null)
                {
                    EventManager.instance.Fire(new GameSaveEvent());
                }
            }
        }
    }
}


