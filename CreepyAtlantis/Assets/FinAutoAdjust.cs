using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinAutoAdjust : MonoBehaviour {

    [SerializeField] bool portTrueStbdFalse;

    [SerializeField] float adjustTime;

    private Vector3 startAngs;

    private float timer;
    private float xOffset;
    private float yOffset;
    private float zOffset;

	// Use this for initialization
	void Start () {
        startAngs = transform.localEulerAngles;

        xOffset = Random.Range(-40, 40);
        yOffset = Random.Range(25, 45);
        zOffset = Random.Range(0, 20);


        transform.localEulerAngles = Manipulate(xOffset, yOffset, zOffset);
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        transform.localRotation = Quaternion.Slerp(Quaternion.Euler(transform.localEulerAngles), Quaternion.Euler(Manipulate(xOffset, yOffset, zOffset)), .9f * Time.deltaTime);

        if (timer >= adjustTime)
        {
            timer -= adjustTime;
            xOffset = Random.Range(-40, 40);
            yOffset = Random.Range(25, 45);
            zOffset = Random.Range(0, 45);
        }
    }

    private Vector3 Manipulate (float xO, float yO, float zO)
    {
        if (portTrueStbdFalse)
        {
            return startAngs - Vector3.up * yO - Vector3.right * xO - Vector3.forward * zO;
        }
        else
        {
            return startAngs + Vector3.up * yO + Vector3.right * xO + Vector3.forward * zO;
        }
    }
}
