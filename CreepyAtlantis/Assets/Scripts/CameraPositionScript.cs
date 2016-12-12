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

        float lowestY = p1.position.y;
        if (p2.position.y < lowestY)
            lowestY = p2.position.y;
        lowestY -= 2;
        lowestY = Mathf.Clamp(lowestY, 0, 15);

        float maxX = Mathf.Abs(p1.position.x - p2.position.x);
        if (Mathf.Abs(p1.position.x - sub.position.x) > maxX)
            maxX = Mathf.Abs(p1.position.x - sub.position.x);
        if (Mathf.Abs(p2.position.x - sub.position.x) > maxX)
            maxX = Mathf.Abs(p2.position.x - sub.position.x);
        maxX = Mathf.Clamp(maxX, 0, 40);

        float maxXScalar = maxX / 40;

        float maxY = Mathf.Abs(p1.position.y - p2.position.y);
        if (Mathf.Abs(p1.position.y - sub.position.y) > maxY)
            maxY = Mathf.Abs(p1.position.y - sub.position.y);
        if (Mathf.Abs(p2.position.y - sub.position.y) > maxY)
            maxY = Mathf.Abs(p2.position.y - sub.position.y);
        maxY = Mathf.Clamp(maxY, 0, 20);

        float maxYScalar = maxY / 20;

        //Debug.Log(maxXScalar + " " + maxYScalar);

        float maxScalar = maxXScalar;
        if (maxYScalar > maxScalar)
            maxScalar = maxYScalar;

        float depth = Mathf.Lerp(-25, -45, maxScalar);

        Vector3 camDest = new Vector3((p1.position.x + p2.position.x) / 2, lowestY, depth);
        if (camDest.x >= sub.position.x + 5)
            camDest.x = sub.position.x + 5;
        if (camDest.x <= sub.position.x - 5)
            camDest.x = sub.position.x - 5;

        transform.position = Vector3.Lerp(transform.position, camDest, .05f);

    }
}
