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

    public float desiredAngle;

    private bool freeze;

    public float leftMax;
    public float rightMax;

    void Start ()
    {
        subPos = transform.position;
        myLight = transform.Find("LightArray");
        appliedAngRate = 0;
    }

    void FixedUpdate ()
    {
        //Debug.Log(subPos);

        if (!p1.activeSelf && !p2.activeSelf)
        {
            if ((appliedMoveScalar < 0 && transform.position.x > leftMax) || (appliedMoveScalar > 0 && transform.position.x < rightMax))
                subPos += transform.right * (appliedMoveScalar);
        }
        else if ((!p1.activeSelf && p2.activeSelf) || (!p2.activeSelf && p1.activeSelf))
        {
            if ((appliedMoveScalar < 0 && transform.position.x > leftMax) || (appliedMoveScalar > 0 && transform.position.x < rightMax))
            {
                if (!p1.activeSelf)
                {
                    // if you're on the left of the active player and want to move right
                    if (transform.position.x < p2.transform.position.x + 20 && appliedMoveScalar > 0)
                        subPos += transform.right * (appliedMoveScalar);
                    // if you're on the right of the active player and want to move left
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
            }
        }
        else
        {
            return;
        }

        transform.position = Vector3.Lerp(transform.position, subPos, rate);

        if (!freeze)
        {
            lightAng = Mathf.MoveTowards(lightAng, desiredAngle, angRate);
            myLight.localEulerAngles = new Vector3(0, 0, lightAng);
        }


    }

    public void moveLeftRight (float leftRight)
    {
        if (leftRight < -0.25f)
        {
            appliedMoveScalar = (appliedMoveScalar -moveScalar) / 2;
        }
        else if (leftRight > 0.25f)
        {
            appliedMoveScalar = (appliedMoveScalar + moveScalar) / 2;
        }
        else
        {
            appliedMoveScalar = 0;
        }
    }

    public void rotateUpDown(float upDown, float leftRight)
    {
        if(Mathf.Abs(leftRight) > .25f || Mathf.Abs(upDown) > .25f)
        {
            freeze = false;
            desiredAngle = ((Mathf.Atan2(upDown, leftRight) * Mathf.Rad2Deg));
            Mathf.Clamp(desiredAngle, -120, 120);
        }
        else
        {
            freeze = true;
        }
    }

    public void SetLeftRightMax(float lM, float rM)
    {
        leftMax = lM;
        rightMax = rM;
    }
}
