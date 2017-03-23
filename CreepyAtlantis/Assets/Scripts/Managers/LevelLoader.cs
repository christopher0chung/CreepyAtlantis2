using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelLoader : MonoBehaviour {

    //Event Manager should be handling this but music controls are here for the time being.
    private MusicManager myMM;

    private string[] funcToLevel = new string[4];

    [SerializeField] private int _level;
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
                Invoke(funcToLevel[value - 1], 0.5f);              
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

    public

    void Awake()
    {
        funcToLevel[0] = "LoadLevelOne";
        funcToLevel[1] = "LoadLevelTwo";
        funcToLevel[2] = "LoadLevelThree";
        funcToLevel[3] = "LoadLevelFour";
        myMM = GetComponent<MusicManager>();
    }

    public void ReloadLevel()
    {
        int currentLevel = level;
        _level = 0;
        LoadLevel(currentLevel);
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
        else
            return false;
    }

    private void LoadLevelOne()
    {
        //Debug.Log("LoadLevelOne");
        //SceneManager.LoadScene("ControllerCharacterHookup");
        SceneManager.LoadScene("Play01");
        SceneManager.LoadScene("Add02", LoadSceneMode.Additive);
    }

    private void LoadLevelTwo()
    {
        SceneManager.LoadScene("CutSceneLevel");
        myMM.FadeOut(0);
        myMM.FadeIn(1);
        myMM.FadeIn(2);
        myMM.FadeIn(3);
    }

    private void LoadLevelThree()
    {
        SceneManager.LoadScene("Play01");
        SceneManager.LoadScene("Add00", LoadSceneMode.Additive);
        myMM.FadeOut(2);
        myMM.FadeOut(3);
    }

    private void LoadLevelFour()
    {
        SceneManager.LoadScene("Play01");
        SceneManager.LoadScene("Add01", LoadSceneMode.Additive);
        myMM.FadeOut(2);
        myMM.FadeOut(3);
    }

    private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadLevel(level+1);
        }
    }
}
