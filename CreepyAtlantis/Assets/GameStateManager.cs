using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {

    public Controllables[] controlStatus = new Controllables[2];

    public delegate void SwitchControls(int player, Controllables target);
    public static event SwitchControls onSwap;

    public void Swap (int player, Controllables target)
    {
        onSwap(player, target);
        controlStatus[player] = target;
    }

    public delegate void IngressEgress(int player, bool ingress);
    public static event IngressEgress onSubInteract;

    public void SubInteract (int player, bool ingress)
    {
        onSubInteract(player, ingress);
    }
}

public enum Controllables { character1, character2, submarine }
