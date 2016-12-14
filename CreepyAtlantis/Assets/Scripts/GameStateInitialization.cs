using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStateInitialization : MonoBehaviour {

    private string sceneName;
   
	void Start () {
        SceneManager.sceneLoaded += GameStateInitialize;
	}
	
    void GameStateInitialize (Scene scene, LoadSceneMode mode)
    {
        sceneName = scene.name;
        if (scene.name == "Play01")
        {
            Invoke("SetControllersToChars", 1f);
            Invoke("SetLeftRightBounds", 1f);
            //Debug.Log("Invoked");
        }
    }

    private void SetControllersToChars()
    {
        GetComponent<GameStateManager>().SetControls(1, Controllables.character);

        GetComponent<GameStateManager>().SetControls(0, Controllables.character);
    }

    private void SetLeftRightBounds()
    {
        if (sceneName == "Play01")
        {
            //For any level where Play01 is the base level, this manager will assume a LevelSpecs component with relevant information to exist in the added scene.
            GameObject.Find("Sub").GetComponent<SubControlScript>().SetLeftRightMax(GameObject.Find("LevelSpecs").GetComponent<LevelSpecs>().leftMax, GameObject.Find("LevelSpecs").GetComponent<LevelSpecs>().rightMax);
        }
    }
}
