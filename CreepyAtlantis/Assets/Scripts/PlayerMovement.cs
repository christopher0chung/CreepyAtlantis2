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
    public float onGroundScale;
    public float notOnGroundScale;

    public float breathingRate;
    public float walkingRate;
    public float thrustRate;
    public float toolRateBase;

    private float scale;
    private BoostPSScript PSBoost;

    private Vector3 leftRightForce;
    private Vector3 upForce;

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

    //public void MoveRight()
    //{
    //    if (transform.position.x < cameraPos.position.x + 20.5f)
    //    {
    //        leftRightForce = Vector3.right * walkForce * scale;
    //        myAir.Consume(walkingRate * Time.deltaTime);
    //        myAC.SetIdle(false);
    //        myAC.SetRight(true);
    //    }
    //}

    //public void MoveLeft()
    //{
    //    if (transform.position.x > cameraPos.position.x - 20.5f)
    //    {
    //        leftRightForce = Vector3.right * -walkForce * scale;
    //        myAir.Consume(walkingRate * Time.deltaTime);
    //        myAC.SetIdle(false);
    //        myAC.SetRight(false);
    //    }
    //}

    public void AltMovement (float upDown, float leftRight)
    {
        if (leftRight > .2f)
        {
            SetDir(facingDirection.right);
        }
        else if (leftRight < -.2f)
        {
            SetDir(facingDirection.left);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(myCharRot), 5);
        if (Mathf.Sqrt(upDown * upDown + leftRight * leftRight) >= .3f)
        {
            nowBoosting = true;
        }
        else
        {
            nowBoosting = false;
        }
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
        baseForce = new Vector3(leftRight, upDown, 0) * walkForce;

    }

    //public void MoveNeutral()
    //{
    //    leftRightForce = Vector3.zero;
    //}

    //public void Boost(bool trueFalse)
    //{
    //    nowBoosting = trueFalse;

    //    if (trueFalse && boostTimer <= maxBoostTime)
    //    {
    //        boostTimer += Time.deltaTime;

    //        upForce = Vector3.up * thrustForce * (1 - (boostTimer / maxBoostTime));
    //        myAir.Consume(thrustRate * Time.deltaTime);
    //        PSBoost.onOff = true;
    //    }
    //    else
    //    {
    //        upForce = Vector3.zero;
    //        PSBoost.onOff = false;
    //    }
    //}

    public void FixedUpdate ()
    {
        DetectGround();

        //myRB.AddForce(leftRightForce + upForce);
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
                return;
            }
        }

        scale = notOnGroundScale;
        myAC.SetGrounded(false);
    }
}

public enum facingDirection { left, right, forward }
