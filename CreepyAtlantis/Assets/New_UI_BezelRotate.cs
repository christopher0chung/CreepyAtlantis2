using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_UI_BezelRotate : MonoBehaviour {

    private float rotAng;

    private float timer;
    private float tripTime;

	void Update () {

        timer += Time.deltaTime;
        if (timer >= tripTime)
        {
            tripTime = Random.Range(2f, 3f);
            timer -= tripTime;
            rotAng = Random.Range(-540, 540);
        }

        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, rotAng), .005f);
	}
}
