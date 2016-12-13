using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour{

    public Transform cameraPos;
    private Rigidbody2D myRB;
    private PlayerAir myAir;

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

    private Vector2 leftRightForce;
    private Vector2 upForce;

	// Use this for initialization
	void Start () {
        myRB = GetComponent<Rigidbody2D>();
        scale = notOnGroundScale;
        myAir = GetComponent<PlayerAir>();
        PSBoost = transform.Find("PSBoost").GetComponent<BoostPSScript>();
	}

    // Update is called once per frame
    void Update () {

        //if (Input.GetKey(rightMove) && transform.position.x < cameraPos.position.x + 20.5f)
        //{
        //    leftRightForce = Vector2.right * walkForce * scale;
        //    myAir.Consume(walkingRate * Time.deltaTime);
        //    transform.rotation = Quaternion.Euler(0, 0, 0);
        //}
        //else if (Input.GetKey(leftMove) && transform.position.x > cameraPos.position.x - 20.5f)
        //{
        //    leftRightForce = Vector2.right * -walkForce * scale;
        //    myAir.Consume(walkingRate * Time.deltaTime);
        //    transform.rotation = Quaternion.Euler(0, 180, 0);

        //}
        //else
        //{
        //    leftRightForce = Vector2.zero;
        //}

        //if (Input.GetKey(thrust))
        //{
        //    upForce = Vector2.up * thrustForce;
        //    myAir.Consume(thrustRate * Time.deltaTime);
        //    PSBoost.onOff = true;
        //}
        //else
        //{
        //    upForce = Vector2.zero;
        //    PSBoost.onOff = false;
        //}

        myAir.Consume(breathingRate * Time.deltaTime);

    }

    public void MoveRight()
    {
        if (transform.position.x < cameraPos.position.x + 20.5f)
        {
            leftRightForce = Vector2.right * walkForce * scale;
            myAir.Consume(walkingRate * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void MoveLeft()
    {
        if (transform.position.x < cameraPos.position.x + 20.5f)
        {
            leftRightForce = Vector2.right * -walkForce * scale;
            myAir.Consume(walkingRate * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void MoveNeutral()
    {
        leftRightForce = Vector2.zero;
    }

    public void Boost(bool trueFalse)
    {
        if (trueFalse)
        {
            upForce = Vector2.up * thrustForce;
            myAir.Consume(thrustRate * Time.deltaTime);
            PSBoost.onOff = true;
        }
        else
        {
            upForce = Vector2.zero;
            PSBoost.onOff = false;
        }
    }

    public void FixedUpdate ()
    {
        myRB.AddForce(leftRightForce + upForce);
    }

    void OnCollisionStay2D ()
    {
        scale = onGroundScale;
    }

    void OnCollisionExit2D ()
    {
        scale = notOnGroundScale;
    }
}
