using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotationScript : MonoBehaviour {

    public Transform[] elements = new Transform[5];

    private float _timer;
    private float _tickTime;
    private Vector3[] originalRots = new Vector3[5];
    private Vector3 rotMod;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < elements.Length; i++)
        {
            originalRots[i] = elements[i].localRotation.eulerAngles;
        }
	}
	
	// Update is called once per frame
	void Update () {
        _timer += Time.deltaTime;
        if (_timer >= _tickTime)
        {
            _tickTime = Random.Range(.5f, 1.1f);
            _timer = 0;
            rotMod = new Vector3(0, 0, Random.Range(-180, 180));
        }

        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].localRotation = Quaternion.Slerp(elements[i].localRotation, Quaternion.Euler(originalRots[i] +rotMod), Time.deltaTime * 2);
        }
	}
}

