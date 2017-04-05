using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOfLevelVign : MonoBehaviour {

    private VignetteController myVC;
    private float timer;
    private bool done;

	// Use this for initialization
	void Start () {
        myVC = GetComponent<VignetteController>();
        myVC.slider = 0;

    }
	
	// Update is called once per frame
	void Update () {
        if (!done)
        {
            timer += Time.deltaTime / 2;
            if (timer >= 1)
            {
                done = true;
                timer = 1;
            }
            myVC.slider = timer;
        }
        else
        {
            return;
        }
  	}
}
