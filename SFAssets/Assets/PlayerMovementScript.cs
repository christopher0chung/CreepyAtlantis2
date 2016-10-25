using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour {

    private Rigidbody2D myRB;
    public float movementForce;
    public float jumpForce;

    public float onGroundMultiplier;

	// Use this for initialization
	void Start () {
        myRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.D))
        {
            myRB.AddForce(Vector2.right * movementForce * onGroundMultiplier, ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            myRB.AddForce(Vector2.right * -movementForce * onGroundMultiplier, ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            myRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        }

    }

    void OnCollisionStay2D()
    {
        onGroundMultiplier = 1f;
    }
    void OnCollisionExit2D()
    {
        onGroundMultiplier = .25f;
    }
}
