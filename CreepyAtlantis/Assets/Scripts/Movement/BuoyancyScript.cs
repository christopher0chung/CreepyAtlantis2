using UnityEngine;
using System.Collections;

public class BuoyancyScript : MonoBehaviour {

    private float originalHeight;

    private Rigidbody myRB;
    private bool upDown;
    [SerializeField] float upDownForce; //12

	void Start () {
        originalHeight = transform.position.y;
        myRB = GetComponent<Rigidbody>();
	}
	
    void FixedUpdate()
    {
        myRB.AddForce(BuoyancyVector());
    }

    private Vector3 BuoyancyVector ()
    {
        if (originalHeight - transform.position.y >= 1 && !upDown)
        {
            upDown = true;
        }
        if (originalHeight - transform.position.y <= -1 && upDown)
        {
            upDown = false;
        }

        if (upDown)
            return Vector3.up * upDownForce;
        else
            return Vector3.up * -upDownForce;
    }
}
