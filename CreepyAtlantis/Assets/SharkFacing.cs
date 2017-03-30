using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkFacing : MonoBehaviour {

    private Vector3 lastPos;
    private Vector3 dir;

    // Use this for initialization
    void Start () {
        //currentPos = transform.position;
        //pastPos = currentPos;
	}
	
	// Update is called once per frame
	void Update () {
        dir = Vector3.Normalize(transform.position - lastPos);
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir, Vector3.up), .09f);
        }
        lastPos = transform.position;
    }
}
