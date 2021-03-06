﻿using UnityEngine;
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

        if (sceneName == "ControllerCharacterHookup")
        {
            //GetComponent<SelectionManager>().Init();
            SetControllersToChars();
        }
        if (sceneName == "Play01")
        {
            //Invoke("SetControllersToChars", 1f);
            SetControllersToSub();
        }
        else if (sceneName == "Add00" || sceneName == "Add01" || sceneName == "Add02" || sceneName == "Add03")
        {
            //Debug.Log("Scene: " + sceneName + " initialized");
            Invoke("SetLeftRightBounds", 1f);
            Invoke("SetSubProperties", 1f);

            if (GetComponent<LevelLoader>().GetLevel() == GetComponent<LevelLoader>().GetHold())
            {
                Invoke("MoveSub", 1f);
            }
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
        if (sceneName == "Add00" || sceneName == "Add01" || sceneName == "Add02" || sceneName == "Add03")
        {
            //For any level where Play01 is the base level, this manager will assume a LevelSpecs component with relevant information to exist in the added scene.
            GameObject.Find("Sub").GetComponent<SubControlScript>().SetLeftRightMax(GameObject.Find("LevelSpecs").GetComponent<LevelSpecs>().leftMax, GameObject.Find("LevelSpecs").GetComponent<LevelSpecs>().rightMax);
        }
    }

    private void SetSubProperties()
    {
        if (sceneName == "Add00" || sceneName == "Add01" || sceneName == "Add02" || sceneName == "Add03")
        {
            GameObject.Find("Sub").GetComponent<SubControlScript>().canGetOut = GameObject.Find("LevelSpecs").GetComponent<LevelSpecs>().canGetOutInitial;
            GameObject.Find("Sub").GetComponent<SubControlScript>().canMove = GameObject.Find("LevelSpecs").GetComponent<LevelSpecs>().canMoveInitial;
        }
    }

    private void MoveSub()
    {
        GameObject.Find("Sub").transform.position = GetComponent<ObjectivesTracker>().respawnPos;
        GameObject.Find("Sub").GetComponent<SubControlScript>().canGetOut = GetComponent<ObjectivesTracker>().respawnSubExit;
        GameObject.Find("Sub").GetComponent<SubControlScript>().canMove = GetComponent<ObjectivesTracker>().respawnSubMove;
    }
}
