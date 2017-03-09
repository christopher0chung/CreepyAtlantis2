using UnityEngine;
using System.Collections;

public class MaskToTargetScript : MonoBehaviour {

    public Transform target;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = target.position;
        transform.rotation = target.rotation;

	}
}
