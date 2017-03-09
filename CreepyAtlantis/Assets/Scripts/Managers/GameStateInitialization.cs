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
        //Debug.Log(sceneName);
        if (sceneName == "Play01")
        {
            //Invoke("SetControllersToChars", 1f);
            SetControllersToSub();
        }
        else if (sceneName == "Add01")
        {
            Invoke("SetLeftRightBounds", 1f);
        }
    }

    private void SetControllersToChars()
    {
        GetComponent<GameStateManager>().SetControls(1, Controllables.character);

        GetComponent<GameStateManager>().SetControls(0, Controllables.character);
    }

    private void SetControllersToSub()
    {
        GetComponent<GameStateManager>().SubInteract(0, true);
        GetComponent<GameStateManager>().SubInteract(1, true);
        GetComponent<GameStateManager>().SetControls(0, Controllables.submarine);
        GetComponent<GameStateManager>().SetControls(1, Controllables.submarine);
    }

    private void SetLeftRightBounds()
    {
        if (sceneName == "Add01")
        {
            //For any level where Play01 is the base level, this manager will assume a LevelSpecs component with relevant information to exist in the added scene.
            GameObject.Find("Sub").GetComponent<SubControlScript>().SetLeftRightMax(GameObject.Find("LevelSpecs").GetComponent<LevelSpecs>().leftMax, GameObject.Find("LevelSpecs").GetComponent<LevelSpecs>().rightMax);
        }
    }
}
