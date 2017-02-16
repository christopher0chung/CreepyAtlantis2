using UnityEngine;
using System.Collections;

public class SubControlScript : MonoBehaviour {

    public float speed;
    public float appliedMoveScalar;
    public float rate;
    private Vector3 subPos;
    //private Vector3 lastPos;

    private Transform myLight;
    public float angRate;
    //private Vector3 currentAng = new Vector3 (0, 0, -45);

    public GameObject p1;
    public GameObject p2;

    public float lightAng;

    public float desiredAngle;

    private bool freeze0;
    private bool freeze1;

    public float leftMax;
    public float rightMax;

    void Start ()
    {
        subPos = transform.position;
        myLight = transform.Find("LightArray");
    }

    void FixedUpdate ()
    {
        //Debug.Log(subPos);
        if (!p1.activeSelf && !p2.activeSelf)
        {
            if ((appliedMoveScalar < 0 && transform.position.x > leftMax) || (appliedMoveScalar > 0 && transform.position.x < rightMax))
                subPos += transform.right * (appliedMoveScalar * speed);
        }
        else if ((!p1.activeSelf && p2.activeSelf) || (!p2.activeSelf && p1.activeSelf))
        {
            if ((appliedMoveScalar < 0 && transform.position.x > leftMax) || (appliedMoveScalar > 0 && transform.position.x < rightMax))
            {
                if (!p1.activeSelf)
                {
                    // if you're on the left of the active player and want to move right
                    if (transform.position.x < p2.transform.position.x + 20 && appliedMoveScalar > 0)
                        subPos += transform.right * (appliedMoveScalar * speed);
                    // if you're on the right of the active player and want to move left
                    else if (transform.position.x > p2.transform.position.x - 20 && appliedMoveScalar < 0)
                        subPos += transform.right * (appliedMoveScalar * speed);
                }
                else if (!p2.activeSelf)
                {
                    if (transform.position.x < p1.transform.position.x + 20 && appliedMoveScalar > 0)
                        subPos += transform.right * (appliedMoveScalar * speed);
                    else if (transform.position.x > p1.transform.position.x - 20 && appliedMoveScalar < 0)
                        subPos += transform.right * (appliedMoveScalar * speed);
                }
            }
        }
        else
        {
            return;
        }

        transform.position = Vector3.Lerp(transform.position, subPos, rate);

        if (!freeze0 || !freeze1)
        {
            lightAng = Mathf.MoveTowards(lightAng, desiredAngle, angRate);
            myLight.localEulerAngles = new Vector3(0, 0, lightAng);
        }
    }

    public void moveLeftRight (float leftRight, int pNum)
    {
        if (Mathf.Abs(leftRight) >= 0.25f)
        {
            appliedMoveScalar = (appliedMoveScalar + leftRight) / 2;
        }
        else
        {
            appliedMoveScalar = (appliedMoveScalar + 0) / 2;
        }
    }

    public void rotateUpDown(float upDown, float leftRight, int pNum)
    {
        Debug.Log("in RotateUpDown");
        if(Mathf.Abs(leftRight) > .25f || Mathf.Abs(upDown) > .25f)
        {
            if (pNum == 0)
                freeze0 = false;
            else
                freeze1 = false;
            desiredAngle = ((Mathf.Atan2(upDown, leftRight) * Mathf.Rad2Deg));
            Mathf.Clamp(desiredAngle, -120, 120);
        }
        else
        {
            if (pNum == 0)
                freeze0 = true;
            else
                freeze1 = true;
        }
    }

    public void SetLeftRightMax(float lM, float rM)
    {
        Debug.Log("left and right bounds set");
        leftMax = lM;
        rightMax = rM;
    }
}
