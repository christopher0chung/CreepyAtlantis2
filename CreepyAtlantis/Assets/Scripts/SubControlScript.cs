using UnityEngine;
using System.Collections;

public class SubControlScript : MonoBehaviour {

    public float moveScalar;
    private float appliedMoveScalar;
    public float rate;
    private Vector3 subPos;
    private Vector3 lastPos;

    private Transform myLight;
    public float angRate;
    private float appliedAngRate;
    private Vector3 currentAng = new Vector3 (0, 0, -45);

    public KeyCode left;
    public KeyCode right;
    public KeyCode rotUp;
    public KeyCode rotDown;

    public GameObject p1;
    public GameObject p2;

    void Start ()
    {
        subPos = transform.position;
        myLight = transform.Find("LightArray");
    }

    void FixedUpdate ()
    {
        subPos += transform.right * (appliedMoveScalar);

        lastPos = transform.position;
        transform.position = Vector3.Lerp(transform.position, subPos, rate);

        currentAng = new Vector3(transform.rotation.x, transform.rotation.y, appliedAngRate);
        if (appliedAngRate > -120 && appliedAngRate < 120)
            myLight.rotation = Quaternion.RotateTowards(myLight.rotation, Quaternion.Euler(currentAng), angRate);
        else if (appliedAngRate <= -120 && appliedAngRate >=-180)
            myLight.rotation = Quaternion.RotateTowards(myLight.rotation, Quaternion.Euler(0, 0, -120), angRate);
        else if (appliedAngRate >= 120 && appliedAngRate <= 180)
            myLight.rotation = Quaternion.RotateTowards(myLight.rotation, Quaternion.Euler(0, 0, 120), angRate);


        //if (lastPos.x > transform.position.x)
        //    Debug.Log("moving Left");
        //else if (lastPos.x < transform.position.x)
        //    Debug.Log("moving Right");
        //else
        //    Debug.Log("stationary");
    }

    void Update ()
    {
        //if (Input.GetKey(left))
        //    moveLeftRight(-1);
        //else if (Input.GetKey(right))
        //    moveLeftRight(1);
        //else
        //    moveLeftRight(0);

        //if (Input.GetKey(rotUp))
        //    rotateUpDown(1);
        //else if (Input.GetKey(rotDown))
        //    rotateUpDown(-1);
        //else
        //    rotateUpDown(0);
    }

    public void moveLeftRight (float leftRight)
    {
        if (leftRight < -0.25f)
        {
            appliedMoveScalar = -moveScalar;
        }
        else if (leftRight > 0.25f)
        {
            appliedMoveScalar = moveScalar;
        }
        else
        {
            appliedMoveScalar = 0;
        }
    }

    public void rotateUpDown(float upDown, float leftRight)
    {
        appliedAngRate = Mathf.Atan2(leftRight, upDown) * Mathf.Rad2Deg;
        Debug.Log(appliedAngRate);
    }
}
