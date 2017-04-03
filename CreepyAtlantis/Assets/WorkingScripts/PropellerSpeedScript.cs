using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerSpeedScript : MonoBehaviour {

    public ParticleSystem[] myPS = new ParticleSystem[4];

    [SerializeField] private float threshold;

    private Rigidbody myRB;

    public float rotSpeed;

	void Start () {
        myRB = transform.root.GetComponent<Rigidbody>();
	}
	
	void Update () {

        PropSpeed();
        PropCav();
    }

    void PropSpeed ()
    {
        rotSpeed = Mathf.Lerp(rotSpeed, transform.root.GetComponent<SubControlScript>().resultantMoveVector.x/10, Time.deltaTime * 2);

        transform.Rotate(Vector3.forward, rotSpeed);
    }

    void PropCav()
    {
        if (transform.root.GetComponent<SubControlScript>().resultantMoveVector.x > 0)
        {
            for (int i = 0; i < myPS.Length; i++)
            {
                myPS[i].transform.localRotation = Quaternion.Euler(180, 0, 0);
            }
        }
        else
        {
            for (int i = 0; i < myPS.Length; i++)
            {
                myPS[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (Mathf.Abs(transform.root.GetComponent<SubControlScript>().resultantMoveVector.x) > threshold)
        {
            int whichOne;
            if (Mathf.Abs(rotSpeed) > 20)
                whichOne = Random.Range(0, 12);
            else if (Mathf.Abs(rotSpeed) > 10)
                whichOne = Random.Range(0, 20);
            else if (Mathf.Abs(rotSpeed) > 5)
                whichOne = Random.Range(0, 30);
            else
                return;

            if (whichOne < 4)
            {
                myPS[whichOne].Emit(1);
            }
        }
    }
}
