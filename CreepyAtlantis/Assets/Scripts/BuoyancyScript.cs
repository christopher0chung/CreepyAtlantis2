using UnityEngine;
using System.Collections;

public class BuoyancyScript : MonoBehaviour {

    //public float period;
    //public float magnitude;
    private float originalHeight;
    //private float timer;

    private Rigidbody myRB;
    private bool upDown;

	// Use this for initialization
	void Start () {
        originalHeight = transform.position.y;
        myRB = GetComponent<Rigidbody>();
	}
	
	//// Update is called once per frame
	//void Update () {

 //       timer += Time.deltaTime;
 //       myRB.MovePosition(new Vector3(transform.position.x, originalHeight + magnitude * Mathf.Sin(timer * period), transform.position.z));

	//}

    void FixedUpdate()
    {
        myRB.AddForce(BuoyancyVector());
    }

    private Vector3 BuoyancyVector ()
    {
        return Vector3.zero;
    }
}
