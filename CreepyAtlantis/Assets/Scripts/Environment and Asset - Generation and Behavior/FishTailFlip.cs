using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTailFlip : MonoBehaviour {

    private float timer;
    public float period;
    public float amplitude;

    private float offset;

	// Use this for initialization
	void Start () {
		offset = Random.Range(0.0f, 13.23f);
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        transform.localRotation = Quaternion.Euler(-90, Mathf.Sin(timer * period + offset) * amplitude, 0);
		
	}
}
