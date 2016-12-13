using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DensityParticleSystemPosition : MonoBehaviour {

    private Transform camCtrl;

	// Use this for initialization
	void Start () {
        camCtrl = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = camCtrl.position + (Vector3.forward * -(camCtrl.position.z + 10)) + (Vector3.up * 0.3f * -(camCtrl.position.z + 10));
	}
}
