using UnityEngine;
using System.Collections;

public class TimedCleanup : MonoBehaviour {

    private float timer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer >=8)
        {
            Destroy(this.gameObject);
        }
	}
}
