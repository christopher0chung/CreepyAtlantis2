using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinAutoAdjust : MonoBehaviour {

    [SerializeField] bool portTrueStbdFalse;

    [SerializeField] float adjustTime;

    private Vector3 startAngs;

    private float timer;
    private float xOffset;
    private float yOffSet;

	// Use this for initialization
	void Start () {
        startAngs = transform.localEulerAngles;

        xOffset = Random.Range(-40, 40);
        yOffSet = Random.Range(25, 45);

        transform.localEulerAngles = Manipulate(xOffset, yOffSet);
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        transform.localRotation = Quaternion.Slerp(Quaternion.Euler(transform.localEulerAngles), Quaternion.Euler(Manipulate(xOffset, yOffSet)), .9f * Time.deltaTime);

        if (timer >= adjustTime)
        {
            timer -= adjustTime;
            xOffset = Random.Range(-40, 40);
            yOffSet = Random.Range(25, 45);
        }
	}

    private Vector3 Manipulate (float xO, float yO)
    {
        if (portTrueStbdFalse)
        {
            return startAngs - Vector3.up * yOffSet - Vector3.right * xOffset;
        }
        else
        {
            return startAngs + Vector3.up * yOffSet + Vector3.right * xOffset;
        }
    }
}
