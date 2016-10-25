using UnityEngine;
using System.Collections;

public class LightBeamBehavior : MonoBehaviour {

    private float timer;
    private SpriteRenderer mySR;

	// Use this for initialization
	void Start () {

        timer = Random.Range(-6000, -5000);
        mySR = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        mySR.color = new Color(mySR.color.r, mySR.color.g, mySR.color.b, .05f * Mathf.Sin(timer));
	}
}
