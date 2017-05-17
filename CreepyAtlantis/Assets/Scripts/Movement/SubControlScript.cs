using UnityEngine;
using System.Collections;

public class GE_PlayerIngressEgress : GameEvent
{
    public PlayerID myID;
    public bool inTrueOutFalse;

    public GE_PlayerIngressEgress (PlayerID i, bool iO)
    {
        myID = i;
        inTrueOutFalse = iO;
    }
}

public class SubControlScript : MonoBehaviour {

    [SerializeField] private Transform myLight;
    public float angRate;

    public GameObject p1;
    public GameObject p2;

    public bool p1In;
    public bool p2In;

    public bool canMove;
    public bool canGetOut;

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

    //private Controllables[] ctrlRef;

    private Rigidbody myRB;

    void Awake ()
    {
        myRB = GetComponent<Rigidbody>();
        //ctrlRef = GameObject.FindGameObjectWithTag("Managers").GetComponent<GameStateManager>().currentPlayControlsRef;

        EventManager.instance.Register<Button_GE>(LocalHandler);
        EventManager.instance.Register<Stick_GE>(LocalHandler);
        EventManager.instance.Register<GE_PreLoadLevel>(LocalHandler);
        EventManager.instance.Register<GE_PlayerIngressEgress>(LocalHandler);
    }

    void Start()
    {
        lightAng = 300;
    }

    void Unregister()
    {
        EventManager.instance.Unregister<Button_GE>(LocalHandler);
        EventManager.instance.Unregister<Stick_GE>(LocalHandler);
        EventManager.instance.Unregister<GE_PreLoadLevel>(LocalHandler);
        EventManager.instance.Unregister<GE_PlayerIngressEgress>(LocalHandler);
    }

    void LocalHandler(GameEvent e)
    {
        if (e.GetType() == typeof(GE_PreLoadLevel))
        {
            Unregister();
        }
        else if (e.GetType() == typeof(GE_PlayerIngressEgress))
        {
            //Debug.Log("Reading a ingress/egress event");
            GE_PlayerIngressEgress p = (GE_PlayerIngressEgress)e;
            if (p.myID == PlayerID.p1)
            {
                p1In = p.inTrueOutFalse;
                p1InOutTimer = 0;
            }
            else
            {
                p2In = p.inTrueOutFalse;
                p2InOutTimer = 0;
            }
        }
        else if (e.GetType() == typeof(Button_GE))
        { 
            if (canGetOut)
            {
                //Debug.Log("Reading a button event from sub");

                Button_GE b = (Button_GE)e;
                if (b.button == Button.Action && b.pressedReleased)
                {
                    Debug.Log("Sending out");
                    if (p1In && b.thisPID == PlayerID.p1 && p1.GetComponent<PlayerController>().inOutReady)
                    {
                        StartCoroutine("IEInOutP1");
                        //EventManager.instance.Fire(new GE_PlayerIngressEgress(PlayerID.p1, false));
                        Debug.Log("P1 was in, now is out");
                    }
                    if (p2In && b.thisPID == PlayerID.p2 && p2.GetComponent<PlayerController>().inOutReady)
                        //EventManager.instance.Fire(new GE_PlayerIngressEgress(PlayerID.p2, false));
                        StartCoroutine("IEInOutP2");
                }
            }
        }
        else if (e.GetType() == typeof(Stick_GE))
        {
            Stick_GE s = (Stick_GE)e;
            if (p1In && s.thisPID == PlayerID.p1)
            {
                if (s.stick == Stick.Left && canMove)
                {
                    p1MoveVector = new Vector3(s.leftRight, s.upDown, 0) * moveForce;
                }
                if (s.stick == Stick.Right)
                {
                    if (s.upDown == 0 && s.leftRight == 0)
                    {
                        freeze0 = true;
                    }
                    else
                    {
                        //Debug.Log("Right stick inputing player 0");
                        freeze0 = false;

                        float ang = ((Mathf.Atan2(s.upDown, s.leftRight) * Mathf.Rad2Deg) + 360) % 360;
                        if (ang <= 90f)
                            desiredAngle = 360;
                        else
                            desiredAngle = Mathf.Clamp(ang, 180, 360);
                        //Debug.Log(desiredAngle);
                    }
                }
            }
            else if (p2In && s.thisPID == PlayerID.p2)
            {
                if (s.stick == Stick.Left)
                {
                    p2MoveVector = new Vector3(s.leftRight, s.upDown, 0) * moveForce;
                }
                if (s.stick == Stick.Right)
                {
                    if (s.upDown == 0 && s.leftRight == 0)
                    {
                        freeze1 = true;
                    }
                    else
                    {
                        //Debug.Log("Right stick inputing player 1");
                        freeze1 = false;

                        float ang = ((Mathf.Atan2(s.upDown, s.leftRight) * Mathf.Rad2Deg) + 360) % 360;
                        if (ang <= 90f)
                            desiredAngle = 360;
                        else 
                            desiredAngle = Mathf.Clamp(ang, 180,  360);
                        //Debug.Log(desiredAngle);
                    }
                }
            }
        }
    }

    private IEnumerator IEInOutP1()
    {
        yield return new WaitForSeconds(.1f);
        if (p1In && p1Ready)
        EventManager.instance.Fire(new GE_PlayerIngressEgress(PlayerID.p1, false));
        yield break;
    }

    private IEnumerator IEInOutP2()
    {
        yield return new WaitForSeconds(.1f);
        if (p2In && p2Ready)
        EventManager.instance.Fire(new GE_PlayerIngressEgress(PlayerID.p2, false));
        yield break;
    }

    void FixedUpdate ()
    {
        //MovementInput();

        resultantMoveVector = p1MoveVector + p2MoveVector;

        myRB.AddForce(resultantMoveVector);

        if ((freeze0 && !freeze1))
        {
            //Debug.Log("P2 Moving Light");
            lightAng = Mathf.MoveTowards(lightAng, desiredAngle, angRate);
            myLight.localEulerAngles = new Vector3(0, 0, lightAng);
        }
        if(!freeze0 && freeze1)
        {
            //Debug.Log("P1 Moving Light");
            lightAng = Mathf.MoveTowards(lightAng, desiredAngle, angRate);
            myLight.localEulerAngles = new Vector3(0, 0, lightAng);
        }
    }

    private float p1InOutTimer;
    private bool p1Ready;
    private float p2InOutTimer;
    private bool p2Ready;

    void Update()
    {
        if (p1In)
        {
            p1.transform.position = transform.position;
        }
        if (p2In)
        {
            p2.transform.position = transform.position;
        }

        p1InOutTimer += Time.deltaTime;
        p2InOutTimer += Time.deltaTime;

        if (p1InOutTimer >= 1)
            p1Ready = true;
        else
            p1Ready = false;

        if (p2InOutTimer >= 0)
            p2Ready = true;
        else
            p2Ready = false;
    }

    //public void Move (float upDown, float leftRight, int pNum)
    //{
    //    Vector3 assignedV3;

    //    if (Mathf.Abs((leftRight * leftRight) + (upDown * upDown)) >= 0.25f)
    //    {
    //        //appliedMoveScalar = (appliedMoveScalar + leftRight) / 2;
    //        assignedV3 = leftRight * Vector3.right * moveForce + upDown * Vector3.up * moveForce /2;
    //    }
    //    else
    //    {
    //        //appliedMoveScalar = (appliedMoveScalar + 0) / 2;
    //        assignedV3 = Vector3.zero;
    //    }

    //    if (pNum == 0)
    //    {
    //        p1MoveVector = assignedV3;
    //    }
    //    else
    //    {
    //        p2MoveVector = assignedV3;
    //    }
    //}

    //public void rotateUpDown(float upDown, float leftRight, int pNum)
    //{
    //    //Debug.Log("in RotateUpDown");
    //    if(Mathf.Abs(leftRight) > .25f || Mathf.Abs(upDown) > .25f)
    //    {
    //        if (pNum == 0)
    //            freeze0 = false;
    //        else
    //            freeze1 = false;
    //        desiredAngle = ((Mathf.Atan2(upDown, leftRight) * Mathf.Rad2Deg));
    //        Mathf.Clamp(desiredAngle, -120, 120);
    //    }
    //    else
    //    {
    //        if (pNum == 0)
    //            freeze0 = true;
    //        else
    //            freeze1 = true;
    //    }
    //}

    public void SetLeftRightMax(float lM, float rM)
    {
        //Debug.Log("left and right bounds set");
        leftMax = lM;
        rightMax = rM;
    }

    //private void MovementInput()
    //{
    //    if (ctrlRef[0] == Controllables.submarine && ctrlRef[1] == Controllables.submarine)
    //    {
    //        if (p1MoveVector == p2MoveVector)
    //        {
    //            resultantMoveVector = p1MoveVector;
    //        }
    //        else if (p1MoveVector != p2MoveVector)
    //        {
    //            if (p1MoveVector == Vector3.zero)
    //            {
    //                resultantMoveVector = p2MoveVector;
    //            }
    //            else if (p2MoveVector == Vector3.zero)
    //            {
    //                resultantMoveVector = p1MoveVector;
    //            }
    //            else
    //            {
    //                resultantMoveVector = Vector3.zero;
    //            }
    //        }
    //        else
    //        {
    //            resultantMoveVector = Vector3.zero;
    //        }
    //    }
    //    else if ((ctrlRef[0] == Controllables.submarine && ctrlRef[1] != Controllables.submarine) || (ctrlRef[1] == Controllables.submarine && ctrlRef[0] != Controllables.submarine))
    //    {
    //        {
    //            if (ctrlRef[0] == Controllables.submarine)
    //            {
    //                resultantMoveVector = p1MoveVector;
    //                p2MoveVector = Vector3.zero;
    //            }
    //            else if (ctrlRef[1] == Controllables.submarine)
    //            {
    //                resultantMoveVector = p2MoveVector;
    //                p1MoveVector = Vector3.zero;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        resultantMoveVector = Vector3.zero;
    //    }
    //    myRB.AddForce(resultantMoveVector);
    //}
}