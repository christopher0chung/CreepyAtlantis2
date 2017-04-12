using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAngAnim : MonoBehaviour {

    private Rigidbody myRB;

    [SerializeField] private Vector3 localAng;
    private float shipAng;
    private float shipAngResultant;
	// Use this for initialization
	void Start () {
        myRB = transform.root.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        shipAngResultant = -myRB.velocity.x * myRB.velocity.y;

        shipAng = Mathf.Lerp(shipAng, shipAngResultant, Time.deltaTime * 2);

        localAng = new Vector3(shipAng, 90, 0);

        transform.localRotation = Quaternion.Euler(localAng);
        //transform.root.GetComponent<SubControlScript>().resultantMoveVector.x
    }
}
