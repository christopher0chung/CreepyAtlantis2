using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Deeper_ObjectiveObject : MonoBehaviour {

    public enum Type_GameObjective_HowToTrigger { TimeInColllider, PositiveAction, HandOff, DialogueComplete }

    #region Creation Properties
    [SerializeField] private Type_GameObjective myType;
    [SerializeField] private Type_GameObjective_HowToTrigger myMethod;

    [SerializeField] private string iD_name;
    private string iD_locSerial;

    public string label;
    public string description;

    [SerializeField] private Status_GameObjective myStatus;
    #endregion

    #region Optional Properties
    [Header("Check TRUE if active from start.")]
    [SerializeField] private bool optional_startActive;

    [Header("Objective that activates this OnComplete")]
    [SerializeField] private Deeper_ObjectiveObject optional_precedingObjective;

    [Header("Objective that triggers this OnComplete for handoff")]
    [SerializeField] private Deeper_ObjectiveObject optional_handoffObjective;

    [Header("For Interactable Objectives")]
    [SerializeField] private int optional_timesToInteractToComplete;

    [Header("For Collider Objectives")]
    [SerializeField] private float optional_timeInsideToComplete;
    [SerializeField] private string[] optional_nameOfColliderOwners;

    [Header("Dialogue Event to Fire OnActive")]
    [SerializeField] private Deeper_DialogueEvent optional_dialogueEventOnActive;

    [Header("Dialogue Event to Fire OnTriggered")]
    [SerializeField] private Deeper_DialogueEvent optional_dialogueEventOnTriggered;

    [Header("Name of id if part of set")]
    [SerializeField] private string optional_iDToSubScribeTo;

    [Header("Optional: Dialogue that activates this objective")]
    [SerializeField] private Deeper_DialogueLine_Base optional_DialogueToActivate;

    [Header("Check first box if it should change sub status OnActive")]
    [SerializeField] private bool AffectSubOnTrigger;
    [SerializeField] private bool CanMoveNow;
    [SerializeField] private bool CanGetOutNow;
    #endregion

    #region Functional Vars
    private GameObjective _myObjv;

    [HideInInspector] public int interactCount;
    [HideInInspector] public float timer;

    private GameObject myInd;
    #endregion

    #region UnityEvents
    public UnityEvent onCreated;
    public UnityEvent onInitialized;
    public UnityEvent onActivated;
    public UnityEvent onTriggered;
    public UnityEvent onCompleted;
    public UnityEvent onCleanedUp;

    #endregion

    void Awake()
    {
        iD_locSerial = transform.position.ToString();
        _MakeObj(myType, iD_name + iD_locSerial, label, description, optional_iDToSubScribeTo);
        EventManager.instance.Register<GameObjectiveEvent>(EventListener);
        EventManager.instance.Register<GE_DiaToObjv>(EventListener);
        GameStateManager.onPreLoadLevel += OnPreLoadLevel;
    }

    void OnPreLoadLevel()
    {
        EventManager.instance.Unregister<GameObjectiveEvent>(EventListener);
        EventManager.instance.Unregister<GE_DiaToObjv>(EventListener);
        GameStateManager.onPreLoadLevel -= OnPreLoadLevel;
    }

	void Start () {
        _myObjv.ManagerCheckIn();
        if (optional_startActive && _myObjv.status == Status_GameObjective.Initialized)
        {
            _myObjv.status = Status_GameObjective.Active;
        }
	}

    void OnTriggerStay(Collider other)
    {
        IsInside(other);
    }

    void EventListener(GameEvent e)
    {
        if (e.GetType() == typeof(GameObjectiveEvent))
        {
            GameObjectiveEvent GOE = (GameObjectiveEvent)e;

            if (optional_precedingObjective != null)
            {
                if (GOE.GObjv.iD == optional_precedingObjective._myObjv.iD)
                {
                    if (GOE.GObjv.status == Status_GameObjective.Completed || GOE.GObjv.status == Status_GameObjective.CleanedUp)
                    {
                        if (_myObjv.status == Status_GameObjective.Initialized)
                            _myObjv.status = Status_GameObjective.Active;
                    }
                }
            }
            
            if (optional_handoffObjective != null)
            {
                if (GOE.GObjv.iD == optional_handoffObjective._myObjv.iD)
                {
                    if (GOE.GObjv.status == Status_GameObjective.Active)
                    {
                        if (_myObjv.status == Status_GameObjective.Active)
                            _myObjv.status = Status_GameObjective.Triggered;
                    }
                }
            }

            if (GOE.GObjv.iD == _myObjv.iD)
            {
                myStatus = GOE.GObjv.status;
                if (GOE.GObjv.status == Status_GameObjective.Created)
                {
                    onCreated.Invoke();
                }
                else if (GOE.GObjv.status == Status_GameObjective.Initialized)
                {
                    onInitialized.Invoke();
                }
                else if (GOE.GObjv.status == Status_GameObjective.Active)
                {
                    myInd = (GameObject) Instantiate(Resources.Load("OLI"), transform.position, Quaternion.identity);

                    if (GetComponent<Deeper_InteractableObject>() != null)
                    {
                        myInd.GetComponent<ObjectiveLocationIndicator>().AssignWho(GetComponent<Deeper_InteractableObject>().whoCanInteract);
                    }

                    if (GetComponent<Deeper_IlluminableObject>() != null)
                    {
                        GetComponent<Deeper_IlluminableObject>().AssignWho(GetComponent<Deeper_InteractableObject>().whoCanInteract);
                    }

                    myInd.transform.parent = transform;

                    if (optional_dialogueEventOnActive != null)
                    {
                        Debug.Log("In onActive with a dialogue");
                        optional_dialogueEventOnActive.Fire();
                    }

                    if (AffectSubOnTrigger)
                    {
                        GameObject.Find("Sub").GetComponent<SubController>().canMove = CanMoveNow;
                        GameObject.Find("Sub").GetComponent<SubController>().canGetOut = CanGetOutNow;
                    }
                    onActivated.Invoke();
                }
                else if (GOE.GObjv.status == Status_GameObjective.Triggered)
                {
                    DialogueManager myDM = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
                    if (optional_dialogueEventOnTriggered != null)
                        optional_dialogueEventOnTriggered.Fire();
                    onTriggered.Invoke();
                    _myObjv.status = Status_GameObjective.Completed;
                }
                else if (GOE.GObjv.status == Status_GameObjective.Completed)
                {
                    if (myInd != null)
                    Destroy(myInd);
                    onCompleted.Invoke();
                    _myObjv.status = Status_GameObjective.CleanedUp;
                }
                else if (GOE.GObjv.status == Status_GameObjective.CleanedUp)
                {
                    onCleanedUp.Invoke();
                }
            }
        }

        if (e.GetType() == typeof(GE_DiaToObjv))
        {
            GE_DiaToObjv d = (GE_DiaToObjv)e;
            if (optional_DialogueToActivate != null)
            {
                if (d.DialogueLineSerial == optional_DialogueToActivate.gameObject.name)
                {
                    if (_myObjv.status == Status_GameObjective.Initialized)
                    {
                        Debug.Log("Message received");
                        _myObjv.status = Status_GameObjective.Active;
                    }
                }
            }
        }
    }


    #region Contextual Functions
    public void WasInteracted()
    {
        if (myMethod == Type_GameObjective_HowToTrigger.PositiveAction)
        {
            if (_myObjv.status == Status_GameObjective.Active)
            {
                interactCount++;
                if (interactCount >= optional_timesToInteractToComplete)
                    _myObjv.status = Status_GameObjective.Triggered;
            }
        }
    }

    private void IsInside(Collider other)
    {
        foreach (string name in optional_nameOfColliderOwners)
        {
            if (other.name == name)
            {
                if (myMethod == Type_GameObjective_HowToTrigger.TimeInColllider)
                {
                    timer += Time.fixedDeltaTime;
                    if (timer >= optional_timeInsideToComplete)
                        _myObjv.status = Status_GameObjective.Triggered;
                }
            }
        }
    }

    #endregion

    #region Internal Methods
    private void _MakeObj (Type_GameObjective t, string i, string l, string d, string s)
    {
        if (t == Type_GameObjective.InteractDiscrete)
        {
            _myObjv = new InteractDiscrete_GameObjective(i, l, d);
        }
        else if (t == Type_GameObjective.InteractOver)
        {
            _myObjv = new InteractOver_GameObjective(i, l, d);
        }
        else if (t == Type_GameObjective.InteractOver_Sub)
        {
            _myObjv =  new InteractOver_Sub_GameObjective(i, l, d, s);
        }
    }
    #endregion
}
