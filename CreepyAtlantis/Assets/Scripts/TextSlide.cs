using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSlide : MonoBehaviour {

    private RectTransform myRT;

    private float xPos;
    private float timer;



	// Use this for initialization
	void Start () {
        myRT = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        xPos = -752 + 711.0f * (timer / 5);
        xPos = Mathf.Clamp(xPos, -752, -41);
        myRT.localPosition = new Vector3(xPos, -22, 0);

	}
}
