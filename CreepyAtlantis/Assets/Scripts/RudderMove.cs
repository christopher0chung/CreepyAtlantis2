using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RudderMove : MonoBehaviour {

    private float ang;
    private float timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        ang = 30 * Mathf.Sin(timer * .5f);

        transform.localRotation = Quaternion.Euler(-90, ang, 0);

	}
}
