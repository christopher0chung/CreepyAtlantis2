using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentScript : MonoBehaviour, ICurrent {

    private Rigidbody myRB;

    private Vector3 currentVal;

    [SerializeField] private float forceScalar;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        currentVal = Vector3.Lerp(currentVal, Vector3.zero, Time.deltaTime);
    }

    void FixedUpdate()
    {
        myRB.AddForce(currentVal);
    }

    public void PushWithCurrent(Vector3 dir)
    {
        currentVal = dir * forceScalar;
    }
    
}
