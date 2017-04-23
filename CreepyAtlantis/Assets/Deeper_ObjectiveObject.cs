using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deeper_ObjectiveObject : MonoBehaviour {

    #region Creation Properties
    [SerializeField]private readonly Type_GameObjective myType;

    [SerializeField] private readonly string iD_name;
    private string iD_locSerial;

    [SerializeField] private readonly string label;
    [SerializeField] private readonly string description;

    [SerializeField] private Status_GameObjective myStatus;
    #endregion

    #region Optional Properties
    [SerializeField] private readonly string optional_iDToSubScribeTo;
    [SerializeField] private readonly int optional_timesToInteractToComplete;
    [SerializeField] private readonly float optional_timeInsideToComplete;
    [SerializeField] private readonly string[] optional_nameOfColliderOwners;
    #endregion

    #region Functional Vars
    private GameObjective _myObjv;

    private delegate void SelectedUpdate();
    private SelectedUpdate _mySU;

    [HideInInspector] public int interactCount;
    [HideInInspector] public float timer;
    #endregion

    void Awake()
    {
        iD_locSerial = transform.position.ToString();
        _myObjv = _MakeObj(myType, iD_name + iD_locSerial, label, description, optional_iDToSubScribeTo);
    }

	void Start () {
        _myObjv.ManagerCheckIn();
	}

    void Update()
    {
        if (_mySU != null)
            _mySU();
    }

    void OnTriggerEnter(Collider other)
    {
        IsInside(other);
    }

    #region Optional Updates


    #endregion

    #region Contextual Functions
    public void WasInteracted()
    {
        if (myType == Type_GameObjective.Interact)
        {
            if (_myObjv.status == Status_GameObjective.Active)
                _myObjv.status = Status_GameObjective.Completed;
        }
        else if (myType == Type_GameObjective.InteractMultiple)
        {
            if (_myObjv.status == Status_GameObjective.Active)
            {
                interactCount++;
                if (interactCount >= optional_timesToInteractToComplete)
                    _myObjv.status = Status_GameObjective.Completed;
            }
        }
        else if (myType == Type_GameObjective.Collect)
        {
            if (_myObjv.status == Status_GameObjective.Active)
                _myObjv.status = Status_GameObjective.Completed;
        }
        else if (myType == Type_GameObjective.Collect_SubObjv)
        {
            if (_myObjv.status == Status_GameObjective.Active)
                _myObjv.status = Status_GameObjective.Completed;
        }
    }

    private void IsInside(Collider other)
    {
        foreach (string name in optional_nameOfColliderOwners)
        {
            if (other.name == name)
            {
                if (myType == Type_GameObjective.Find)
                    _myObjv.status = Status_GameObjective.Completed;
                else if (myType == Type_GameObjective.Time)
                {
                    timer += Time.fixedDeltaTime;
                    if (timer >= optional_timeInsideToComplete)
                        _myObjv.status = Status_GameObjective.Completed;
                }
            }
        }
    }

    #endregion

    #region Internal Methods
    private GameObjective _MakeObj (Type_GameObjective t, string i, string l, string d, string s)
    {
        if (t == Type_GameObjective.Interact || t == Type_GameObjective.InteractMultiple)
        {
            return new Interact_GameObjective(i, l, d);
        }
        else if (t == Type_GameObjective.Collect)
        {
            return new CollectMultiple_GameObjective(i, l, d);
        }
        else if (t == Type_GameObjective.Collect_SubObjv)
        {
            return new CollectMultiple_SubObjective_GameObjective(i, l, d, s);
        }
        return null;
    }
    #endregion
}
