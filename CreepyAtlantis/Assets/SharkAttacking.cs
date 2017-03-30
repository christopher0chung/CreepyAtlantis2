using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkAttacking : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float range;
    private Camera myCam;

    private Vector3[][] enumToV3;

    private enum SharkPos { OffLeft, OnScreen, OffRight }
    private enum SharkDist { Background, InView, InStrikingDist }
    private enum SharkStates { Patrol, Engage, Attack, Flee }

    private delegate void SharkFunc();
    private SharkFunc activeFunc;

    private SharkPos currentPos;

    private SharkStates currentState;
    private void SetState(SharkStates state)
    {
        currentState = state;
        switch (state)
        {
            case SharkStates.Patrol:
                activeFunc = PatrolFunc;
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

    void Start()
    {
        SetInDict(SharkPos.OffLeft, SharkDist.Background, Vector3.zero);
        SetInDict(SharkPos.OffLeft, SharkDist.InView, Vector3.zero);
        SetInDict(SharkPos.OffLeft, SharkDist.InStrikingDist, Vector3.zero);

        SetInDict(SharkPos.OnScreen, SharkDist.Background, Vector3.zero);
        SetInDict(SharkPos.OnScreen, SharkDist.InView, Vector3.zero);
        SetInDict(SharkPos.OnScreen, SharkDist.InStrikingDist, Vector3.zero);

        SetInDict(SharkPos.OffRight, SharkDist.Background, Vector3.zero);
        SetInDict(SharkPos.OffRight, SharkDist.InView, Vector3.zero);
        SetInDict(SharkPos.OffRight, SharkDist.InStrikingDist, Vector3.zero);
    }

    void SetInDict (SharkPos pos, SharkDist dist, Vector3 val)
    {
        enumToV3[(int)pos][(int)dist] = val;
    }

    Vector3 GetFromDict (SharkPos pos, SharkDist dist)
    {
        return enumToV3[(int)pos][(int)dist];
    }

    void PlaceShark()
    {
        if (currentPos == SharkPos.OffLeft)
        {
        }

        transform.position = myCam.transform.position + new Vector3();

    }

    void PatrolFunc()
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
