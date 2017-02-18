using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenPurposeSpinner : MonoBehaviour {

    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float z;

	void Update () {
        transform.Rotate(new Vector3(x * Time.deltaTime, y * Time.deltaTime, z * Time.deltaTime));
	}
}
