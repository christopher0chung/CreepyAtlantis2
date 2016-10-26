using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody2D myRB;

    public float thrustForce;
    public float walkForce;
    public float onGroundScale;
    public float notOnGroundScale;

    private float scale;

	// Use this for initialization
	void Start () {
        myRB = GetComponent<Rigidbody2D>();
        scale = notOnGroundScale;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.D))
        {
            myRB.AddForce(Vector2.right * walkForce * scale);
        }

        if (Input.GetKey(KeyCode.A))
        {
            myRB.AddForce(Vector2.right * -walkForce * scale);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            myRB.AddForce(Vector2.up * thrustForce);
        }

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
