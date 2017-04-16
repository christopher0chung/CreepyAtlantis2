using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour {

    public Controllables[] currentPlayControlsRef = new Controllables[2];

    void Awake ()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Components that want to react when controls are being hooked up should add local functions to onHookCtrl
    // HookControls() is used to call the event onHookCtrl;
    public delegate void ControlsSet(int player, Controllables target);
    public static event ControlsSet onSetControls;

    public void SetControls (int player, Controllables target)
    {
        //Debug.Log(player + " " + target);
        
        onSetControls(player, target);
        if (target != Controllables.dialogue)
            currentPlayControlsRef[player] = target;
    }

    public delegate void ControlsEndDialogue(int player, Controllables target);
    public static event ControlsEndDialogue onEndDialogue;

    public void EndDialogue(int player)
    {
        if (onEndDialogue != null)
            onEndDialogue(player, currentPlayControlsRef[player]);
    }

    public delegate void ControlsPreLoadLevel();
    public static event ControlsPreLoadLevel onPreLoadLevel;

    public void PreLoadLevel()
    {
        //Debug.Log("PreLoad");
        if (onPreLoadLevel != null)
            onPreLoadLevel();
    }

    public delegate void IngressEgress(int player, bool ingress);
    public static event IngressEgress onSubInteract;

    public void SubInteract (int player, bool ingress)
    {
        onSubInteract(player, ingress);
    }

    public delegate void NarcOrNormal(int player, bool narc);
    public static event NarcOrNormal onNarcUnnarc;

    public void NarcUnnarc(int player, bool narc)
    {
        onNarcUnnarc(player, narc);
    }

    public delegate void DialogueEvent(bool dialoguePlaying);
    public static event DialogueEvent onDialogueEvent;

    public void controlsToDialogue(bool dialoguePlaying)
    {
        onDialogueEvent(dialoguePlaying);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //if (scene.buildIndex == 1)
        //{
        //    SetControls(0, Controllables.character);
        //    SetControls(1, Controllables.character);
        //}
        onEndDialogue = null;
    }

    //void Update ()
    //{
    //    if (Input.GetKeyDown(KeyCode.H))
    //    {
    //        SetControls(0, Controllables.submarine);
    //        SetControls(1, Controllables.none);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.J))
    //    {
    //        SetControls(1, Controllables.submarine);
    //        SetControls(0, Controllables.none);
    //    }
    //}
}

public enum Controllables { character, submarine, dialogue, none }

