using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GE_PreLoadLevel : GameEvent{ }

public class GE_LoadLevelRequest : GameEvent
{
    public int level;
    public GE_LoadLevelRequest (int lvl)
    {
        level = lvl;
    }
}

public class GE_PostAwakeLoadLevel : GameEvent { }

public class LevelLoader : MonoBehaviour {

    private string[] funcToLevel = new string[6];

    public Scene[] myScenes = new Scene[3];

    private int _level;
    private int level
    {
        get
        {
            return _level;
        }
        set
        {
            if (value != _level)
            {
                EventManager.instance.Fire(new GE_PreLoadLevel());
                _level = value;
                Invoke(funcToLevel[value], 0);
                //GameObject.FindGameObjectWithTag("Managers").GetComponent<GameStateManager>().PreLoadLevel();
                //Invoke(funcToLevel[value], 0.5f);              
            }
        }
    }

    public int hold;

    void Awake()
    {
        funcToLevel[0] = "DiedLevel";
        funcToLevel[1] = "LoadLevelOne";
        funcToLevel[2] = "LoadLevelTwo";
        funcToLevel[3] = "LoadLevelThree";
        funcToLevel[4] = "LoadLevelFour";
        funcToLevel[5] = "LoadLevelFive";

        SceneManager.sceneLoaded += WhenSceneLoaded;
        //EventManager.instance.Register<Button_GE>(DeathLoad);
        EventManager.instance.Register<GE_LoadLevelRequest>(LoadLevelHandler);
    }

    private bool CheckReady (int lvl)
    {
        if (lvl == 0)
            return true;
        if (lvl == 1)
        {
            if (GameObject.FindGameObjectWithTag("Player1"))
                return true;
            else
                return false;
        }
        else if (lvl == 2)
            return true;
        else if (lvl == 3)
            return true;
        else if (lvl == 4)
            return true;
        else if (lvl == 5)
            return true;
        else
            return false;
    }

    private void LoadLevelHandler (GameEvent e)
    {
        GE_LoadLevelRequest l = (GE_LoadLevelRequest)e;
        if (CheckReady(l.level))
        {
            level = l.level;
        }
    }

    //public void LoadLevel (int lvl)
    //{
    //    if (CheckReady(lvl))
    //    {
    //        level = lvl;
    //    }
    //}

    private void WhenSceneLoaded (Scene scene, LoadSceneMode mode)
    {
        EventManager.instance.Fire(new GE_PostAwakeLoadLevel());
    }

    #region Death Level
    public int GetLevel()
    {
        return level;
    }
    public int GetHold()
    {
        return hold;
    }

    public void DeathUnload()
    {
        hold = level;
        //LoadLevel(0);
    }

    //public void DeathLoad(GameEvent e)
    //{
    //    //if (hold != 0 && level == 0)
    //    //LoadLevel(hold);
    //}



    private void DiedLevel()
    {
        SceneManager.LoadScene("Empty");
    }

    #endregion

    #region Level Loading Functions
    private void LoadLevelOne()
    {
        SceneManager.LoadScene("ControllerCharacterHookup");
    }

    private void LoadLevelTwo()
    {
        SceneManager.LoadScene("CutSceneLevel");
    }

    private void LoadLevelThree()
    {
        SceneManager.LoadScene("Play01");
        SceneManager.LoadScene("Add01", LoadSceneMode.Additive);
    }

    private void LoadLevelFour()
    {
        SceneManager.LoadScene("Play01");
        SceneManager.LoadScene("Add03", LoadSceneMode.Additive);
    }

    private void LoadLevelFive()
    {
        SceneManager.LoadScene("EndScreen");
    }

    //private void Update ()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        LoadLevel(level+1);
    //    }
    //}
    #endregion
}
