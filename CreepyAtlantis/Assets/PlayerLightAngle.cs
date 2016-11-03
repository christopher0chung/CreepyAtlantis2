using UnityEngine;
using System.Collections;

public class PlayerLightAngle : MonoBehaviour {

    public float lightAngle;

    public void LookAngleCalc(float leftRight, float upDown)
    {
        lightAngle = Mathf.Atan2(upDown, leftRight) * Mathf.Rad2Deg;
    }

	void Update () {

        transform.position = transform.root.position + Vector3.up * 1.373f;
        transform.rotation = Quaternion.Euler(0, 0, lightAngle);


    }
}
