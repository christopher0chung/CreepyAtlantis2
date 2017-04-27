using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPush : MonoBehaviour {

    private bool startMoving;
    public float speed;

    public Vector3 destination;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyDown(KeyCode.U))
        {
            startMoving = true;
        }

        if (startMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        }

	}
}
