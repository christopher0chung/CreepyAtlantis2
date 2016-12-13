using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStateInitialization : MonoBehaviour {

	void Start () {
        SceneManager.sceneLoaded += GameStateInitialize;
	}
	
    void GameStateInitialize (Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Play01")
        {
            Invoke("SetControllersToChars", 1f);
            //Debug.Log("Invoked");
        }
    }

    private void SetControllersToChars()
    {
        GetComponent<GameStateManager>().SetControls(1, Controllables.character);

        GetComponent<GameStateManager>().SetControls(0, Controllables.character);
    }
}
