using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour {

    void Awake ()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Components that want to react when controls are being hooked up should add local functions to onHookCtrl
    // HookControls() is used to call the event onHookCtrl;
    public delegate void ControlsHook(int player, Controllables target);
    public static event ControlsHook onHookCtrl;

    public void HookControls (int player, Controllables target)
    {
        onHookCtrl(player, target);
    }

    public delegate void ControlsUnhook(int player, Controllables target);
    public static event ControlsUnhook onUnhookCtrl;

    public void UnhookControls(int player, Controllables target)
    {
        onUnhookCtrl(player, target);
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            HookControls(0, Controllables.character0);
            HookControls(1, Controllables.character1);
        }
    }
}

public enum Controllables { character0, character1, submarine }
