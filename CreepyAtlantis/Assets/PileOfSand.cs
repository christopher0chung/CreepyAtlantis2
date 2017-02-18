﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileOfSand : SSInteractableObject {

	void Awake () {
        Init(TriggerShape.sphere, Vector3.zero);
        InitSphere(2, transform.position);
	}

    public override void OnPress(int pNum)
    {
        HideInteractable();
        Destroy(this.gameObject);
    }

    void Update () {
        _detectFunc();
	}

    public override void SphereCastDetect()
    {
        base.SphereCastDetect();
    }

    public override void Interact(int pNum, bool pressRelease)
    {
        Instantiate(Resources.Load("KickSand"), transform.position, Quaternion.identity);
        base.Interact(pNum, pressRelease);
        Debug.Log("interact");
    }
}