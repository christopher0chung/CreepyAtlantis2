using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSpawner : MonoBehaviour {

    int counter;
    float timer;

	void Start() { 
	}
	
	// Update is called once per frame
	void Update () {
        if (counter >= 8)
            return;


        timer += Time.deltaTime;
        if (timer > 8)
        {
            timer -= 8;
            counter++;
            Instantiate(Resources.Load("Shark"), new Vector3(-30, Random.Range(5f, 30f), Random.Range(10, 30)), Quaternion.identity);
        }


	}
}
