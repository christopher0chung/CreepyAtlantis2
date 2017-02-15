using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour{

    public Transform cameraPos;
    private Rigidbody myRB;
    private PlayerAir myAir;
    private AnimationControl myAC;

    public KeyCode leftMove;
    public KeyCode rightMove;
    public KeyCode thrust;

    public float thrustForce;
    public float walkForce;
    private bool grounded;
    public float onGroundScale;
    public float notOnGroundScale;

    public float breathingRate;
    public float walkingRate;
    public float thrustRate;
    public float toolRateBase;

    private float scale;
    private BoostPSScript PSBoost;

    public float sCCastDist;
    public float sCCastRad;
    private RaycastHit[] myRCHs;

    public float maxBoostTime;
    private float boostTimer;
    private bool _nowBoosting;
    private bool nowBoosting
    {
        get
        {
            return _nowBoosting;
        }
        set
        {
            if (value != _nowBoosting)
            {
                _nowBoosting = value;
                if (value)
                {
                    boostTimer = 0;
                }
            }
        }
    }

    private Vector3 boostForce;
    private Vector3 baseForce;

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
        scale = notOnGroundScale;
        myAir = GetComponent<PlayerAir>();
        PSBoost = transform.Find("Effects").Find("PSBoost").GetComponent<BoostPSScript>();
        myAC = GetComponentInChildren<AnimationControl>();
	}

    void Update () {
        myAir.Consume(breathingRate * Time.deltaTime);
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


        if (!grounded)
        {
            // Starts boosting 
            if (Mathf.Sqrt(upDown * upDown + leftRight * leftRight) >= .3f)
            {
                nowBoosting = true;
            }
            else
            {
                nowBoosting = false;
            }
            // Calculates the baseForce
            baseForce = new Vector3(leftRight, upDown, 0) * walkForce;
        }

        else if (grounded)
        {
            if (upDown > .5f && Mathf.Abs(leftRight) < .2f)
            {
                nowBoosting = true;
            }
            else
            {
                nowBoosting = false;
            }

            baseForce = Vector3.up * upDown;

            if (Mathf.Abs(leftRight) > .3f)
            {
                if (leftRight > 0)
                {
                    myRB.MovePosition(transform.position + Vector3.right * .05f * 60 * Time.deltaTime);
                }
                else
                {
                    myRB.MovePosition(transform.position + Vector3.right * -.05f * 60 * Time.deltaTime);
                }
            }
        }

        // Scales boosting based on time since boost
        boostTimer += Time.deltaTime;
        if (boostTimer <= maxBoostTime)
        {
            float justUp = Mathf.Clamp(upDown, 0, 1);
            boostForce = new Vector3(leftRight, justUp, 0) * thrustForce * (1 - (boostTimer / maxBoostTime));
            myAir.Consume(thrustRate * Time.deltaTime);
            PSBoost.onOff = true;
        }
        else
        {
            boostForce = Vector3.zero;
            PSBoost.onOff = false;
        }
    }

    public void FixedUpdate ()
    {
        DetectGround();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(myCharRot), .09f);

        myRB.AddForce(baseForce + boostForce);
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
                scale = onGroundScale;
                myAC.SetGrounded(true);
                grounded = true;
                return;
            }
        }
        grounded = false;
        scale = notOnGroundScale;
        myAC.SetGrounded(false);
    }
}

public enum facingDirection { left, right, forward }
