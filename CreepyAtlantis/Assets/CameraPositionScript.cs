using UnityEngine;
using System.Collections;

public class CameraPositionScript : MonoBehaviour {

    public Transform p1;
    public Transform p2;
    public Transform sub;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 camDest = new Vector3((p1.position.x + p2.position.x) / 2, transform.position.y, transform.position.z);
        if (camDest.x >= sub.position.x + 5)
            camDest.x = sub.position.x + 5;
        if (camDest.x <= sub.position.x - 5)
            camDest.x = sub.position.x - 5;

        transform.position = Vector3.Lerp(transform.position, camDest, .05f);

    }
}
