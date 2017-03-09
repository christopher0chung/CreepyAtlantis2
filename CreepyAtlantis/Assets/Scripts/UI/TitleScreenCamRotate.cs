using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenCamRotate : MonoBehaviour {

    float timer;
    float angY;
    float angX;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        angY = 60 * Mathf.Sin(timer * .13f);
        angX = 20 * Mathf.Cos(timer * .26f);

        transform.localRotation = Quaternion.Euler(angX, angY, 0);

    }
}
