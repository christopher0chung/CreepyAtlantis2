using UnityEngine;
using System.Collections;

public class BuoyancyScript : MonoBehaviour {

    public float period;
    public float magnitude;
    private float originalHeight;
    private float timer;

	// Use this for initialization
	void Start () {
        originalHeight = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, originalHeight + magnitude * Mathf.Sin(timer * period), transform.position.z);

	}
}
