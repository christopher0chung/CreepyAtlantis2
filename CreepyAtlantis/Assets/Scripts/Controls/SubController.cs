﻿using UnityEngine;
using System.Collections;

public class SubController : MonoBehaviour, IControllable {

    public SubControlScript myMovement;
    public TrapDoorScript myTDS;
    public ControllerAdapter [] myAdapters;

    public bool canGetOut;
    public bool canMove;

    void Awake()
    {
        myMovement = GetComponent<SubControlScript>();
        myTDS = GetComponentInChildren<TrapDoorScript>();

        gameObject.AddComponent<ControllerAdapter>();
        gameObject.AddComponent<ControllerAdapter>();
        myAdapters = GetComponents<ControllerAdapter>();
        myAdapters[0].Initialize(0);
        myAdapters[1].Initialize(1);

        GameStateManager.onSetControls += SetControllerAdapter;
        GameStateManager.onPreLoadLevel += UnSub;
    }

    public void UnSub()
    {
        GameStateManager.onSetControls -= SetControllerAdapter;
        GameStateManager.onPreLoadLevel -= UnSub;
    }

    public void LeftStick(float upDown, float leftRight, int pNum)
    {
        if (canMove)
            myMovement.moveLeftRight(leftRight, pNum);
        else
            myMovement.moveLeftRight(0, pNum);
    }

    public void RightStick(float upDown, float leftRight, int pNum)
    {
        myMovement.rotateUpDown(upDown, leftRight, pNum);
    }

    public void AButton(bool pushRelease, int pNum)
    {
        if (pushRelease && canGetOut)
        {
            GameObject.Find("GameStateManager").GetComponent<GameStateManager>().SetControls(pNum, Controllables.character);
            GameObject.Find("GameStateManager").GetComponent<GameStateManager>().SubInteract(pNum, false);
        }
    }

    public void YButton(bool pushRelease, int pNum) { }

    public void LeftBumper(bool pushRelease, int pNum) { }

    public void RightBumper(bool pushRelease, int pNum) { }

    public void SetControllerAdapter(int player, Controllables myControllable)
    {
        if (myControllable == Controllables.submarine)
            myAdapters[player].enabled = true;
        else if (myControllable == Controllables.dialogue)
            return;
        else
            myAdapters[player].enabled = false;
    }
}