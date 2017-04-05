using UnityEngine;
using System.Collections;

public class SubControlScript : MonoBehaviour {

    [SerializeField] private Transform myLight;
    public float angRate;

    public GameObject p1;
    public GameObject p2;

    public float lightAng;

    public float desiredAngle;

    private bool freeze0;
    private bool freeze1;

    public float leftMax;
    public float rightMax;

    [SerializeField] private float moveForce;
    [SerializeField] private Vector3 p1MoveVector;
    [SerializeField] private Vector3 p2MoveVector;
    public Vector3 resultantMoveVector;

    private Controllables[] ctrlRef;

    private Rigidbody myRB;

    void Start ()
    {
        myRB = GetComponent<Rigidbody>();
        ctrlRef = GameObject.FindGameObjectWithTag("Managers").GetComponent<GameStateManager>().currentPlayControlsRef;
    }

    void FixedUpdate ()
    {
        MovementInput();

        if (!freeze0 || !freeze1)
        {
            lightAng = Mathf.MoveTowards(lightAng, desiredAngle, angRate);
            myLight.localEulerAngles = new Vector3(0, 0, lightAng);
        }
    }

    public void Move (float upDown, float leftRight, int pNum)
    {
        Vector3 assignedV3;

        if (Mathf.Abs((leftRight * leftRight) + (upDown * upDown)) >= 0.25f)
        {
            //appliedMoveScalar = (appliedMoveScalar + leftRight) / 2;
            assignedV3 = leftRight * Vector3.right * moveForce + upDown * Vector3.up * moveForce /2;
        }
        else
        {
            //appliedMoveScalar = (appliedMoveScalar + 0) / 2;
            assignedV3 = Vector3.zero;
        }

        if (pNum == 0)
        {
            p1MoveVector = assignedV3;
        }
        else
        {
            p2MoveVector = assignedV3;
        }
    }

    public void rotateUpDown(float upDown, float leftRight, int pNum)
    {
        //Debug.Log("in RotateUpDown");
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
        //Debug.Log("left and right bounds set");
        leftMax = lM;
        rightMax = rM;
    }

    private void MovementInput()
    {
        if (ctrlRef[0] == Controllables.submarine && ctrlRef[1] == Controllables.submarine)
        {
            if (p1MoveVector == p2MoveVector)
            {
                resultantMoveVector = p1MoveVector;
            }
            else if (p1MoveVector != p2MoveVector)
            {
                if (p1MoveVector == Vector3.zero)
                {
                    resultantMoveVector = p2MoveVector;
                }
                else if (p2MoveVector == Vector3.zero)
                {
                    resultantMoveVector = p1MoveVector;
                }
                else
                {
                    resultantMoveVector = Vector3.zero;
                }
            }
            else
            {
                resultantMoveVector = Vector3.zero;
            }
        }
        else if ((ctrlRef[0] == Controllables.submarine && ctrlRef[1] != Controllables.submarine) || (ctrlRef[1] == Controllables.submarine && ctrlRef[0] != Controllables.submarine))
        {
            {
                if (ctrlRef[0] == Controllables.submarine)
                {
                    resultantMoveVector = p1MoveVector;
                    p2MoveVector = Vector3.zero;
                }
                else if (ctrlRef[1] == Controllables.submarine)
                {
                    resultantMoveVector = p2MoveVector;
                    p1MoveVector = Vector3.zero;
                }
            }
        }
        else
        {
            resultantMoveVector = Vector3.zero;
        }
        myRB.AddForce(resultantMoveVector);
    }
}