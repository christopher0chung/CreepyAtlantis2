﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_UI_Parent : MonoBehaviour {

    private Camera myCam;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        transform.position = myCam.transform.position;
    }

    public void HookCam()
    {
        myCam = Camera.main;
    }
}
