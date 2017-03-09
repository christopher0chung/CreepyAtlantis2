using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerSpin : MonoBehaviour {

    private float ang;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.localRotation = Quaternion.Euler(0, ang += 15.45f, 0);
		
	}
}
