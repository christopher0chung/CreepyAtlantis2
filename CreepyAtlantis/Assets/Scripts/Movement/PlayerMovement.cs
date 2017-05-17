using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour{

    public Transform cameraPos;
    private Rigidbody myRB;
    private PlayerAir myAir;
    private AnimationControl myAC;

    public float thrustForce;
    private float appliedThrustForce;

    private bool _grounded;
    public bool grounded
    {
        get
        {
            return _grounded;
        }
        set
        {
            if (value != _grounded)
            {
                _grounded = value;
                if (_grounded)
                    walkMomentumTimer = 0;
            }
        }
    }
    private bool groundedCastCheck;
    private float walkMomentumTimer;
    private float walkMomentumScalar;

    [SerializeField] private float breathingAirRate;
    [SerializeField] private float walkingAirRate;
    [SerializeField] private float boostAirRate;

    private BoostPSScript PSBoost;

    public float sCCastDist;
    public float sCCastRad;
    private RaycastHit[] myRCHs;

    private bool nowBoosting;
    private float justUp;

    [SerializeField] private Vector3 baseForce;

    public facingDirection myDir;
    private Vector3 myCharRot;

    private void SetDir ( facingDirection direction)
    {
        switch (direction)
        {
            case facingDirection.left:
                myDir = facingDirection.left;
                myCharRot = new Vector3(0, 90, 0);
                break;
            case facingDirection.forward:
                myDir = facingDirection.forward;
                myCharRot = Vector3.zero;
                break;
            case facingDirection.right:
                myDir = facingDirection.right;
                myCharRot = new Vector3(0, -90, 0);
                break;
        }
    }

    private Ray stepRay;
    public RaycastHit stepRayHit;

	// Use this for initialization
	void Start () {
        myRB = GetComponent<Rigidbody>();
        myAir = GetComponent<PlayerAir>();
        PSBoost = transform.Find("Effects").Find("PSBoost").GetComponent<BoostPSScript>();
        myAC = GetComponentInChildren<AnimationControl>();
	}

    void Update () {
        myAir.Consume(breathingAirRate * Time.deltaTime);
        WalkingMomentum();
    }

    private void WalkingMomentum()
    {
        walkMomentumTimer += Time.deltaTime;
        walkMomentumScalar = Mathf.Clamp01(walkMomentumTimer);
    }

    public void Movement (float upDown, float leftRight)
    {
        // Handles facing direction and animation
        if (leftRight > .2f)
        {
            SetDir(facingDirection.right);
            myAC.SetIdle(false);
        }
        else if (leftRight < -.2f)
        {
            SetDir(facingDirection.left);
            myAC.SetIdle(false);
        }
        else if (Mathf.Abs(leftRight)<= .2f && Mathf.Abs(upDown) >.2f && !grounded)
        {
            SetDir(facingDirection.forward);
        }


        if (!groundedCastCheck)
        {
            appliedThrustForce = thrustForce;
            // Starts boosting if >.3 from the center in any direction
            if (Mathf.Sqrt(upDown * upDown + leftRight * leftRight) >= .3f)
            {
                nowBoosting = true;
                justUp = Mathf.Clamp01(upDown);
            }
            else
            {
                nowBoosting = false;
                justUp = 0;
            }
            // Calculates the baseForce
            baseForce = new Vector3(leftRight, Mathf.Lerp(upDown, justUp, .5f) * .9f, 0) * appliedThrustForce;
        }
        else if (grounded && groundedCastCheck)
        {
            appliedThrustForce = 3 * thrustForce;
            if (upDown > .5f && Mathf.Abs(leftRight) < .2f)
            {
                nowBoosting = true;
            }
            else
            {
                nowBoosting = false;
            }

            baseForce = Vector3.up * upDown * appliedThrustForce;

            if (Mathf.Abs(leftRight) > .3f)
            {
                if (leftRight > 0)
                {
                    myRB.MovePosition(transform.position + Vector3.right * .040f * 60 * Time.deltaTime * walkMomentumScalar);
                }
                else
                {
                    myRB.MovePosition(transform.position + Vector3.right * -.040f * 60 * Time.deltaTime * walkMomentumScalar);
                }
                myAir.Consume(walkingAirRate * Time.deltaTime);
            }
        }
        else
        {
            baseForce = Vector3.zero;
        }

        if (nowBoosting)
        {
            PSBoost.onOff = true;
            myAir.Consume(boostAirRate * Time.deltaTime);
        }
        else
            PSBoost.onOff = false;
    }

    public void FixedUpdate ()
    {
        DetectGround();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(myCharRot), .09f);
        myRB.AddForce(baseForce);
    }

    void DetectGround()
    {
        Ray toGround = new Ray(transform.position, Vector3.down);
        myRCHs = Physics.SphereCastAll(toGround, sCCastRad, sCCastDist);

        Debug.DrawRay(transform.position, Vector3.down * sCCastDist);

        foreach (RaycastHit myCols in myRCHs)
        {
            if (myCols.collider.gameObject.tag == "Walkable")
            {
                myAC.SetGrounded(true);
                groundedCastCheck = true;
                return;
            }
        }
        groundedCastCheck = false;
        grounded = false;
        EventManager.instance.Fire(new Character_Grounded_GE(transform.root.gameObject.name, GroundStates.NotGrounded));
        myAC.SetGrounded(false);
    }
    void OnCollisionEnter (Collision other)
    {
        if (groundedCastCheck && other.gameObject.tag == "Walkable")
        {
            grounded = true;
            EventManager.instance.Fire(new Character_Grounded_GE(transform.root.gameObject.name, GroundStates.Grounded));
        }
    }
}

public enum facingDirection { left, right, forward }
