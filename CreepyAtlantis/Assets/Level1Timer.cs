using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level1Timer : MonoBehaviour {

    public string myLevel;

    private Text myText;

    [HideInInspector] public float timer;

    private LevelLoader myLL;

    private bool done;

    void Awake()
    {
        SceneManager.sceneLoaded += NextScene;
        myText = GetComponent<Text>();
    }

    void Start()
    {
        timer = 180;
        myLL = GameObject.Find("GameStateManager").GetComponent<LevelLoader>();
    }

    void Update()
    {
        if (timer <= 0 && !done)
        {
            done = true;
            SceneManager.sceneLoaded -= NextScene;
            myLL.LoadLevel(myLL.GetLevel() + 1);
        }
        if (!done)
        {
            timer -= Time.deltaTime;
            int m = (int)(timer / 60);
            int sInt = (int)(timer % 60);
            string s;
            if ((int)sInt >= 10)
                s = sInt.ToString();
            else if (sInt < 10)
                s = "0" + sInt.ToString();
            else
                s = "00";

            myText.text = "Time Remaining: " + m + ":" + s;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.root.gameObject.name == "Character1")
        {
            timer -= Time.fixedDeltaTime;
        }
    }

    private IEnumerator delayedLoadLevel()
    {
        yield return new WaitForSeconds(3);
        myLL.LoadLevel(myLL.GetLevel() + 1);
        yield break;
    }

    private void NextScene(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == myLevel)
        {
            SceneManager.sceneLoaded -= NextScene;
            Destroy(this.gameObject);
        }

        else
        {
            float maxTime = 10000;
            GameObject[] allTimers = GameObject.FindGameObjectsWithTag("LevelTimer");
            foreach(GameObject aTimer in allTimers)
            {
                if (aTimer.GetComponent<Level1Timer>().timer < maxTime)
                {
                    maxTime = aTimer.GetComponent<Level1Timer>().timer;
                }
            }

            if (timer > maxTime)
            {
                SceneManager.sceneLoaded -= NextScene;
                Destroy(this.gameObject);
            }
        }
    }
}