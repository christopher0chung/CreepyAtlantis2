using UnityEngine;
using System.Collections;

public class CameraPositionScript : MonoBehaviour {

    public Transform p1;
    public Transform p2;
    public Transform sub;

    public Transform ind;

    private Vector3 camDest;

    void Start()
    {
        Camera.onPreCull += CameraUpdate;
    }

    void OnDestroy()
    {
        Camera.onPreCull -= CameraUpdate;
    }

    void CameraUpdate(Camera camera) {

        float lowestY = p1.position.y;
        if (p2.position.y < lowestY)
            lowestY = p2.position.y;
        lowestY -= 2;
        lowestY = Mathf.Clamp(lowestY, sub.position.y - 20, sub.position.y - 5);

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
        MoveIndicatorTrigger(maxScalar);

        camDest = new Vector3((p1.position.x + p2.position.x) / 2, lowestY, depth);
        if (camDest.x >= sub.position.x + 5)
            camDest.x = sub.position.x + 5;
        if (camDest.x <= sub.position.x - 5)
            camDest.x = sub.position.x - 5;

        //Vector3 prev = transform.position;
        //transform.position = Vector3.Lerp(transform.position, camDest, 4 * Time.deltaTime);
        //Debug.Log(Vector3.Distance(prev, transform.position));

        transform.position = camDest;
    }

    private void MoveIndicatorTrigger (float zeroToOne)
    {
        //Debug.Log(zeroToOne);
        ind.transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Lerp(8.66f, 15.6f, zeroToOne), 0);
        ind.transform.localScale = new Vector3(Mathf.Lerp(30, 54, zeroToOne), Mathf.Lerp(14.66f, 26.4f, zeroToOne), 10f);
    }
}
