using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMeshReadout : MonoBehaviour {

    private TextMesh myText;

    private Vector3 lastAngs;
    private Vector3 currentAngs;
    private Vector3 deltaAngs;

    public Vector3 smoothedDeltaAngs;

	// Use this for initialization
	void Start () {
        myText = GetComponent<TextMesh>();
        currentAngs = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        lastAngs = currentAngs;
        currentAngs = transform.eulerAngles;

        deltaAngs = currentAngs - lastAngs;

        smoothedDeltaAngs = Vector3.Lerp(smoothedDeltaAngs, deltaAngs, .1f);

        myText.text = "X: " + (int)(smoothedDeltaAngs.x * 60) + "\nY: " + (int)(smoothedDeltaAngs.y * 60) + "\nZ: " + (int)(smoothedDeltaAngs.z * 60);

	}
}
