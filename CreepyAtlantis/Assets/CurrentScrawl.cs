using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentScrawl : MonoBehaviour {

	
	void Update () {

        Vector3 myPos;
        myPos = transform.position + Vector3.right * 5 * Time.deltaTime;
        if (myPos.x >= 620)
        {
            myPos.x -= 650;
        }
        transform.position = myPos;

        transform.Rotate(Vector3.forward, Time.deltaTime / 12);
	}
}
