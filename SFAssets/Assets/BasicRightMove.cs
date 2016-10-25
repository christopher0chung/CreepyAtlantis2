using UnityEngine;
using System.Collections;

public class BasicRightMove : MonoBehaviour {

    private float timer;
    public float bobScale;
    public float rate;

    public float myHeight;


	// Use this for initialization
	void Start () {
        myHeight = transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3(transform.position.x + rate, myHeight + bobScale * Mathf.Sin(timer), transform.position.z);
	}
}
