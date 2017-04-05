using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSwinAnim : MonoBehaviour {

    [SerializeField] Transform bone0;
    [SerializeField] Transform bone1;
    [SerializeField] Transform bone2;

    private float timer;
    [SerializeField] private float freqModifier;
    [SerializeField] private float b1Scalar;
    [SerializeField] private float b2Scalar;

	void Update () {
        float b1Temp = TimeToAng() * b1Scalar;
        bone1.localEulerAngles = new Vector3(bone1.localEulerAngles.x, bone1.localEulerAngles.y, b1Temp);

        float b2Temp = TimeToAng() * b2Scalar;
        bone2.localEulerAngles = new Vector3(bone2.localEulerAngles.x, bone2.localEulerAngles.y, b2Temp);

        TimeToAng();
    }

    private float TimeToAng ()
    {
        timer += Time.deltaTime;
        return Mathf.Sin(freqModifier * timer * transform.root.GetComponent<SharkAttacking>().appliedSpeed);
    }
}
