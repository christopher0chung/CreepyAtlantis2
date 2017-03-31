using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerSpin : MonoBehaviour {

    private float ang;
    [SerializeField] private float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.localRotation = Quaternion.Euler(0, 0, ang += (speed * Time.deltaTime));
		
	}
}
