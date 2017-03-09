using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelLoader : MonoBehaviour {

    //Event Manager should be handling this but music controls are here for the time being.
    private MusicManager myMM;

    public string[] funcToLevel = new string[2];

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
                Invoke(funcToLevel[value - 1], 0.5f);
                _level = value;
            }
        }
    }
    public int LEVEL
    {
        get
        {
            return _level;
        }
    }

    public

    void Awake()
    {
        funcToLevel[0] = "LoadLevelOne";
        funcToLevel[1] = "LoadLevelTwo";
        myMM = GetComponent<MusicManager>();
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
        else
            return false;
    }

    private void LoadLevelOne()
    {
        SceneManager.LoadScene("CutSceneLevel");
        myMM.FadeOut(0);
        myMM.FadeIn(1);
        myMM.FadeIn(2);
        myMM.FadeIn(3);
    }

    private void LoadLevelTwo()
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
            LoadLevel(2);
        }
    }
}
