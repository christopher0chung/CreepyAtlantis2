using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerSpeedScript : MonoBehaviour {

    public ParticleSystem[] myPS = new ParticleSystem[4];

    [SerializeField] private float threshold;

    private Rigidbody myRB;

	void Start () {
        myRB = transform.root.GetComponent<Rigidbody>();
	}
	
	void Update () {
        transform.Rotate(Vector3.up, transform.root.GetComponent<SubControlScript>().resultantMoveVector.x);

        if (transform.root.GetComponent<SubControlScript>().resultantMoveVector.x > 0)
        {
            for (int i = 0; i < myPS.Length; i++)
            {
                myPS[i].transform.localRotation = Quaternion.Euler(-90, 0, 0);
            }
        }
        else
        {
            for (int i = 0; i < myPS.Length; i++)
            {
                myPS[i].transform.localRotation = Quaternion.Euler(90, 0, 0);
            }
        }

        if (Mathf.Abs(transform.root.GetComponent<SubControlScript>().resultantMoveVector.x) > threshold)
        {
            int whichOne = Random.Range(0, (int)Mathf.Abs(transform.root.GetComponent<SubControlScript>().resultantMoveVector.x));
            if (whichOne < 4)
                myPS[whichOne].Emit(1);
        }
	}
}
