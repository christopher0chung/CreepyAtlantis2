using UnityEngine;
using System.Collections;

public class SubControlScript : MonoBehaviour {

    public float moveScalar;
    private float appliedMoveScalar;
    public float rate;
    private Vector3 subPos;
    //private Vector3 lastPos;

    private Transform myLight;
    public float angRate;
    public float appliedAngRate;
    //private Vector3 currentAng = new Vector3 (0, 0, -45);

    public KeyCode left;
    public KeyCode right;
    public KeyCode rotUp;
    public KeyCode rotDown;

    public GameObject p1;
    public GameObject p2;

    public float lightAng;

    void Start ()
    {
        subPos = transform.position;
        myLight = transform.Find("LightArray");
        appliedAngRate = 0;
    }

    void FixedUpdate ()
    {
        if (!p1.activeSelf)
        {
            if (transform.position.x < p2.transform.position.x + 20 && appliedMoveScalar > 0)
                subPos += transform.right * (appliedMoveScalar);
            else if (transform.position.x > p2.transform.position.x - 20 && appliedMoveScalar < 0)
                subPos += transform.right * (appliedMoveScalar);
        }
        else if (!p2.activeSelf)
        {
            if (transform.position.x < p1.transform.position.x + 20 && appliedMoveScalar > 0)
                subPos += transform.right * (appliedMoveScalar);
            else if (transform.position.x > p1.transform.position.x - 20 && appliedMoveScalar < 0)
                subPos += transform.right * (appliedMoveScalar);
        }
        else
        {
            subPos += transform.right * (appliedMoveScalar);
        }

        //lastPos = transform.position;
        transform.position = Vector3.Lerp(transform.position, subPos, rate);

        //currentAng = new Vector3(transform.rotation.x, transform.rotation.y, desiredAngle);
        //if (desiredAngle > -120 && desiredAngle < 120)
        //    myLight.rotation = Quaternion.RotateTowards(myLight.rotation, Quaternion.Euler(currentAng), angRate);
        //else if (desiredAngle <= -120 && desiredAngle >=-180)
        //    myLight.rotation = Quaternion.RotateTowards(myLight.rotation, Quaternion.Euler(0, 0, -120), angRate);
        //else if (desiredAngle >= 120 && desiredAngle <= 180)
        //    myLight.rotation = Quaternion.RotateTowards(myLight.rotation, Quaternion.Euler(0, 0, 120), angRate);

        lightAng += appliedAngRate;
        if (lightAng >= 120)
            lightAng = 120;
        else if (lightAng <= -120)
            lightAng = -120;
        myLight.localRotation = Quaternion.Euler(0, 0, lightAng);


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

    public void rotateUpDown(float leftRight, float upDown)
    {
        //Debug.Log("in rotupdown");
        if (upDown > .25f)
            appliedAngRate = angRate;
        else if (upDown < -.25f)
            appliedAngRate = -angRate;
        else
            appliedAngRate = 0;

    }
}
