using UnityEngine;
using System.Collections;

public class SubControlScript : MonoBehaviour {

    public float moveScalar;
    public float rate;
    private Vector3 subPos;
    private Vector3 lastPos;

    public KeyCode left;
    public KeyCode right;
    public KeyCode rotUp;
    public KeyCode rotDown;

    void Start ()
    {
        subPos = transform.position;
    }

    void FixedUpdate ()
    {
        lastPos = transform.position;
        transform.position = Vector3.Lerp(transform.position, subPos, rate);

        if (lastPos.x > transform.position.x)
            Debug.Log("moving Left");
        else if (lastPos.x < transform.position.x)
            Debug.Log("moving Right");
        else
            Debug.Log("stationary");
    }

    void Update ()
    {
        if (Input.GetKey(left))
            moveLeftRight(-1);
        else if (Input.GetKey(right))
            moveLeftRight(1);
    }

    public void moveLeftRight (float leftRight)
    {
        if (leftRight < 0)
        {
            subPos += transform.right * (-moveScalar);
        }
        else if (leftRight > 0)
        {
            subPos += transform.right * (moveScalar);
        }
    }

}
