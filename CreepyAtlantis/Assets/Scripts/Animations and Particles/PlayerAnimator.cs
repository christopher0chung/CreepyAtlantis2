using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    private Animator myAnim;

    private bool _grounded;
    private bool grounded
    {
        get
        {
            return _grounded;
        }
        set
        {
            if (value != _grounded)
            {
                myAnim.SetBool("grounded", value);
                _grounded = value;
            }
        }
    }

    private bool idle;
    private float idleTimer;

    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    public void SetGrounded(bool gnd)
    {
        grounded = gnd;
    }

    public void SetIdle(bool idl)
    {
        idle = idl;
        idleTimer = 0;
    }

    private void Update()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer >= .1f)
        {
            idle = true;
        }
    }
}
