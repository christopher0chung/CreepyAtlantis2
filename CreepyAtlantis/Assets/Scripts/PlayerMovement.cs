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

    public void MoveRight()
    {
        if (transform.position.x < cameraPos.position.x + 20.5f)
        {
            leftRightForce = Vector3.right * walkForce * scale;
            myAir.Consume(walkingRate * Time.deltaTime);
            myAC.SetIdle(false);
            myAC.SetRight(true);
        }
    }

    public void MoveLeft()
    {
        if (transform.position.x > cameraPos.position.x - 20.5f)
        {
            leftRightForce = Vector3.right * -walkForce * scale;
            myAir.Consume(walkingRate * Time.deltaTime);
            myAC.SetIdle(false);
            myAC.SetRight(false);
        }
    }

    public void MoveNeutral()
    {
        leftRightForce = Vector3.zero;
    }

    public void Boost(bool trueFalse)
    {
        if (trueFalse)
        {
            upForce = Vector3.up * thrustForce;
            myAir.Consume(thrustRate * Time.deltaTime);
            PSBoost.onOff = true;
        }
        else
        {
            upForce = Vector3.zero;
            PSBoost.onOff = false;
        }
    }

    public void FixedUpdate ()
    {
        DetectGround();

        myRB.AddForce(leftRightForce + upForce);
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
