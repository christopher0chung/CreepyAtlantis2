using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveEvent : GameEvent { }
public class GameSaveLoadEvent : GameEvent { }

public class GameManager : MonoBehaviour {

    public void SaveGame()
    {
        EventManager.instance.Fire(new GameSaveEvent());
    }

    public void LoadGame()
    {
        EventManager.instance.Fire(new GameSaveLoadEvent());
    }

}
