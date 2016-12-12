using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStateInitialization : MonoBehaviour {

	void Start () {
        SceneManager.sceneLoaded += GameStateInitialize;
	}
	
    void GameStateInitialize (Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            StartCoroutine(DelayedPlayer());
        }
    }

    private IEnumerator DelayedPlayer ()
    {
        yield return new WaitForSeconds(.1f);
        GetComponent<GameStateManager>().SetControls(0, Controllables.character);
        GetComponent<GameStateManager>().SetControls(1, Controllables.character);
    }
}
