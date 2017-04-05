using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanicSnow : MonoBehaviour {

    private Rigidbody myRB;

    private float timer;
    private float threshold;

	// Use this for initialization
	void Start () {
        myRB = GetComponent<Rigidbody>();
        threshold = Random.Range(.7f, 1.1f);
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= threshold)
        {
            timer -= threshold;
            threshold = Random.Range(.7f, 1.1f);
        }
	}

    void FixedUpdate()
    {

    }
}
