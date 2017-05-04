using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelpPosition : MonoBehaviour {

    public Vector3 offset;
    private Transform sub;

	// Use this for initialization
	void Start () {
        sub = GameObject.Find("Sub").transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = sub.position + offset;
    }
}
