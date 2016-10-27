using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D myRB;
    private PlayerAir myAir;

    public float thrustForce;
    public float walkForce;
    public float onGroundScale;
    public float notOnGroundScale;

    public float breathingRate;
    public float walkingRate;
    public float thrustRate;
    public float toolRateBase;

    private float scale;

	// Use this for initialization
	void Start () {
        myRB = GetComponent<Rigidbody2D>();
        scale = notOnGroundScale;
        myAir = GetComponent<PlayerAir>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.D))
        {
            myRB.AddForce(Vector2.right * walkForce * scale);
            myAir.Consume(walkingRate * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            myRB.AddForce(Vector2.right * -walkForce * scale);
            myAir.Consume(walkingRate * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            myRB.AddForce(Vector2.up * thrustForce);
            myAir.Consume(thrustRate * Time.deltaTime);
        }

        myAir.Consume(breathingRate * Time.deltaTime);

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
