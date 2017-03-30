using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkAttacking : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float range;
    private Camera myCam;

    private enum SharkPos { OffLeft, OnScreen, OffRight }
    private enum SharkStates { PatrolBackground, PatrolInView, PatrolInStrikingDist, Engage, Attack, Flee }

    private delegate void SharkFunc();
    private SharkFunc activeFunc;

    private SharkPos currentPos;

    private SharkStates currentState;
    private void SetState(SharkStates state)
    {
        currentState = state;
        switch (state)
        {
            case SharkStates.PatrolBackground:
                activeFunc = PatrolBackgroundFunc;
                break;
            case SharkStates.PatrolInView:
                activeFunc = PatrolInViewFunc;
                break;
            case SharkStates.PatrolInStrikingDist:
                activeFunc = PatrolInStrikingDistFunc;
                break;
            case SharkStates.Engage:
                activeFunc = EngageFunc;
                break;
            case SharkStates.Attack:
                activeFunc = AttackFunc;
                break;
            case SharkStates.Flee:
                activeFunc = FleeFunc;
                break;
        }
    }

    void Awake()
    {
        myCam = Camera.main;
    }

    void PatrolBackgroundFunc()
    {
    }

    void PatrolInViewFunc()
    {
    }

    void PatrolInStrikingDistFunc()
    {
    }

    void EngageFunc()
    {

    }

    void AttackFunc()
    {

    }

    void FleeFunc()
    {

    }

    void Update()
    {
        UpdateSharkPosEnum();

        if (activeFunc != null)
            activeFunc();
    }

    private void UpdateSharkPosEnum()
    {
        if (transform.position.x > (myCam.transform.position.x - range) && transform.position.x < (myCam.transform.position.x + range))
        {
            currentPos = SharkPos.OnScreen;
        }
        else
        {
            if (transform.position.x < myCam.transform.position.x)
            {
                currentPos = SharkPos.OffLeft;
            }
            else if (transform.position.x > myCam.transform.position.x)
            {
                currentPos = SharkPos.OffRight;
            }
        }
    }
}
