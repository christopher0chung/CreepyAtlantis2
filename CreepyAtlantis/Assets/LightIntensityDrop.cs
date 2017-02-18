using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensityDrop : MonoBehaviour {

    public Light myLight;
    private float timer = 1;

	// Use this for initialization
	void Start () {
        myLight = GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime / 8;
        myLight.range = (timer * timer) * 12;
        myLight.intensity = timer * timer;
	}
}
