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

    private Vector3 dirVec;

    private SharkPos _currentPos;
    private SharkPos currentPos
    {
        get
        {
            return _currentPos;
        }
        set
        {
            if (value != _currentPos)
            {
                if (_currentPos == SharkPos.OnScreen)
                {
                    //currentPos value placed twice for sequencing.
                    _currentPos = value;
                    PlaceShark();
                }
                else
                {
                    _currentPos = value;
                }

            }
        }
    }
    private SharkDist _currentDist;
    private SharkDist currentDist
    {
        get
        {
            return _currentDist;
        }
        set
        {
            if (value != _currentDist)
            {
                _currentDist = value;
                PlaceShark();
            }
        }
    }

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
        SetInDict(SharkPos.OffLeft, SharkDist.Background, new Vector3(-40, 0, 35));
        SetInDict(SharkPos.OffLeft, SharkDist.InView, new Vector3(-35, 0, 20));
        SetInDict(SharkPos.OffLeft, SharkDist.InStrikingDist, new Vector3(-30, 0, 10));

        SetInDict(SharkPos.OnScreen, SharkDist.Background, Vector3.zero);
        SetInDict(SharkPos.OnScreen, SharkDist.InView, Vector3.zero);
        SetInDict(SharkPos.OnScreen, SharkDist.InStrikingDist, Vector3.zero);

        SetInDict(SharkPos.OffRight, SharkDist.Background, new Vector3(40, 0, 35));
        SetInDict(SharkPos.OffRight, SharkDist.InView, new Vector3(35, 0, 20));
        SetInDict(SharkPos.OffRight, SharkDist.InStrikingDist, new Vector3(30, 0, 10));
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
        transform.position = myCam.transform.position + GetFromDict(currentPos, currentDist);
    }

    void PatrolFunc()
    {
        if (currentPos == SharkPos.OffLeft)
        {
            currentPos = SharkPos.OnScreen;
            dirVec = Vector3.right * speed * Time.deltaTime;
        }
        else if (currentPos == SharkPos.OffRight)
        {
            currentPos = SharkPos.OnScreen;
            dirVec = Vector3.right * speed * -Time.deltaTime;
        }

        if (currentPos == SharkPos.OnScreen)
        {
            transform.position += dirVec;
        }
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
