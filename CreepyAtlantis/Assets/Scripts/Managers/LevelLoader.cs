using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelLoader : MonoBehaviour {

    //Event Manager should be handling this but music controls are here for the time being.
    //private MusicManager myMM;

    private string[] funcToLevel = new string[6];

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
                _level = value;
                GameObject.FindGameObjectWithTag("Managers").GetComponent<GameStateManager>().PreLoadLevel();
                Invoke(funcToLevel[value], 0.5f);              
            }
        }
    }
    //public int LEVEL
    //{
    //    get
    //    {
    //        return _level;
    //    }
    //}

    public int hold;

    void Awake()
    {
        funcToLevel[0] = "DiedLevel";
        funcToLevel[1] = "LoadLevelOne";
        funcToLevel[2] = "LoadLevelTwo";
        funcToLevel[3] = "LoadLevelThree";
        funcToLevel[4] = "LoadLevelFour";
        funcToLevel[5] = "LoadLevelFive";

        //myMM = GetComponent<MusicManager>();

        EventManager.instance.Register<Button_GE>(DeathLoad);
    }

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
        LoadLevel(0);
    }

    public void DeathLoad(GameEvent e)
    {
        if (hold != 0 && level == 0)
        LoadLevel(hold);
    }

    public void LoadLevel (int lvl)
    {
        if (CheckReady(lvl))
        {
            level = lvl;
        }
    }

    private bool CheckReady (int lvl)
    {
        //return true;
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

    private void DiedLevel()
    {
        //Debug.Log("Loading Empty");
        SceneManager.LoadScene("Empty");
    }

    private void LoadLevelOne()
    {
        //Debug.Log("LoadLevelOne");
        SceneManager.LoadScene("ControllerCharacterHookup");
        //SceneManager.LoadScene("Play01");
        //SceneManager.LoadScene("Add02", LoadSceneMode.Additive);
    }

    private void LoadLevelTwo()
    {
        SceneManager.LoadScene("CutSceneLevel");
        //myMM.FadeOut(0);
        //myMM.FadeIn(1);
        //myMM.FadeIn(2);
        //myMM.FadeIn(3);
    }

    private void LoadLevelThree()
    {
        //SceneManager.LoadScene("Play01");
        //SceneManager.LoadScene("Add02", LoadSceneMode.Additive);
        SceneManager.LoadScene("Play01");
        //SceneManager.LoadScene("Add00", LoadSceneMode.Additive);
        SceneManager.LoadScene("Add01", LoadSceneMode.Additive);
        //myMM.FadeOut(2);
        //myMM.FadeOut(3);
    }

    private void LoadLevelFour()
    {
        SceneManager.LoadScene("Play01");
        SceneManager.LoadScene("Add03", LoadSceneMode.Additive);
        //myMM.FadeOut(2);
        //myMM.FadeOut(3);
    }

    private void LoadLevelFive()
    {
        SceneManager.LoadScene("EndScreen");
    }

    private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadLevel(level+1);
        }
    }
}
