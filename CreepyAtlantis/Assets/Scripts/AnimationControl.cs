using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour {

    private  Animator myRAC;

    private animStates _currentState;
    public animStates currentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            if (value != _currentState)
            {
                _currentState = currentState;
                if (value == animStates.float_idle)
                {
                    myRAC.SetBool("Idle", true);
                    myRAC.SetBool("Grounded", false);
                }
                else if (value == animStates.float_boost)
                {
                    myRAC.SetBool("Idle", false);
                    myRAC.SetBool("Grounded", false);
                }
                else if (value == animStates.gnd_idle)
                {
                    myRAC.SetBool("Idle", true);
                    myRAC.SetBool("Grounded", true);
                }
                else if (value == animStates.gnd_move_left)
                {
                    myRAC.SetBool("Idle", false);
                    myRAC.SetBool("Grounded", true);
                }
                else if (value == animStates.gnd_move_right)
                {
                    myRAC.SetBool("Idle", false);
                    myRAC.SetBool("Grounded", true);
                }
            }
        }
    }

    public bool grounded;
    public bool idle;
    public bool movingRight;

    private float idleTimer;
    private float idleTime = .1f;

	// Use this for initialization
	void Start () {
        myRAC = GetComponent<Animator>();
	}

    private void setAnimState (bool gnd, bool idl, bool mvR)
    {
        if (gnd)
        {
            if (idl)
            {
                currentState = animStates.gnd_idle;
            }
            else
            {
                if (mvR)
                {
                    currentState = animStates.gnd_move_right;
                }
                else
                {
                    currentState = animStates.gnd_move_left;
                }
            }
        }
        else
        {
            if (idl)
            {
                currentState = animStates.float_idle;
            }
            else
            {
                currentState = animStates.float_boost;
            }
        }
    }

    public void SetGrounded (bool gnd)
    {
        grounded = gnd;
    }

    public void SetIdle(bool idl)
    {
        idle = idl;
        if (idl == false)
            idleTimer = 0;
    }

    public void SetRight(bool mvR)
    {
        movingRight = mvR;
    }

    // Update is called once per frame
    void FixedUpdate () {
        idleTimer += Time.fixedDeltaTime;
        if (idleTimer > idleTime)
        {
            SetIdle(true);
        }

        setAnimState(grounded, idle, movingRight);

	}
}

public enum animStates { gnd_idle, gnd_move_left, gnd_move_right, float_idle, float_boost }
