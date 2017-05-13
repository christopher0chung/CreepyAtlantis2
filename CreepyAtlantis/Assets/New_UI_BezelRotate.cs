using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_UI_BezelRotate : MonoBehaviour {

    private float rotAng;

    private float timer;
    private float tripTime;

    void Start()
    {
        timer = Random.Range(0f, 5f);
    }

	void Update () {

        timer += Time.deltaTime;
        if (timer >= tripTime)
        {
            tripTime = Random.Range(4f, 6f);
            timer -= tripTime;
            rotAng = Random.Range(-540, 540);
        }

        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, rotAng), .002f);
	}
}
