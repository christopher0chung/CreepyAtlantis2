using UnityEngine;
using System.Collections;

public class PlayerLightAngle : MonoBehaviour {

    public float lightAngle;

    public void LookAngleCalc(float leftRight, float upDown)
    {
        lightAngle = Mathf.Atan2(upDown, leftRight) * Mathf.Rad2Deg;
    }

	void Update () {

        //transform.position = transform.root.position + Vector3.up * 1.373f;
        lightAngle = Mathf.Clamp (lightAngle, -60, 60);
        transform.localRotation = Quaternion.Euler(lightAngle, 180, 0);


    }
}
