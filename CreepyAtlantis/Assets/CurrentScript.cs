using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentScript : MonoBehaviour {

    private Rigidbody myRB;

    [SerializeField] private float leftRightForce; //12
    private float appliedLeftRightForce;

    [SerializeField] private float upDownForce; //12
    private float appliedUpDownForce;

    private float timer;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        myRB.AddForce(BuoyancyVector());
    }

    private Vector3 BuoyancyVector()
    {
        timer += Time.deltaTime;
        if (timer > 4)
        {
            timer -= 4;
            appliedLeftRightForce = Random.Range(-leftRightForce, leftRightForce);
            appliedUpDownForce = Random.Range(-5, upDownForce);
        }
        return (Vector3.right * appliedLeftRightForce + Vector3.up * appliedUpDownForce);
    }
}
