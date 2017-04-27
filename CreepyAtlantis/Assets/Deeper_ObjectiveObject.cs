using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Deeper_ObjectiveObject : MonoBehaviour {

    public enum Type_GameObjective_HowToTrigger { TimeInColllider, PositiveAction}

    #region Creation Properties
    [SerializeField] private Type_GameObjective myType;
    [SerializeField] private Type_GameObjective_HowToTrigger myMethod;

    [SerializeField] private string iD_name;
    private string iD_locSerial;

    [SerializeField] private string label;
    [SerializeField] private string description;

    [SerializeField] private Status_GameObjective myStatus;
    #endregion

    #region Optional Properties
    [Header("Time In Collider Data")]
    [SerializeField] private bool optional_startActive;
    [SerializeField] private string optional_iDToSubScribeTo;
    [SerializeField] private int optional_timesToInteractToComplete;
    [SerializeField] private float optional_timeInsideToComplete;

    [Header("Positive Action Data")]
    [SerializeField] private string[] optional_nameOfColliderOwners;
    [SerializeField] private DialogueEvents optional_dialogueEvent;
    [SerializeField] private Deeper_ObjectiveObject optional_precedingObjective;
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
        _myObjv = _MakeObj(myType, iD_name + iD_locSerial, label, description, optional_iDToSubScribeTo);
        EventManager.instance.Register<GameObjectiveEvent>(EventListener);
        GameStateManager.onPreLoadLevel += OnPreLoadLevel;
    }

    void OnPreLoadLevel()
    {
        EventManager.instance.Unregister<GameObjectiveEvent>(EventListener);
        GameStateManager.onPreLoadLevel -= OnPreLoadLevel;
    }

	void Start () {
        _myObjv.ManagerCheckIn();
        if (optional_startActive)
        {
            _myObjv.status = Status_GameObjective.Active;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        IsInside(other);
    }

    void EventListener(GameEvent e)
    {
        if (e.GetType() == typeof(GameObjectiveEvent))
        {
            GameObjectiveEvent GOE = (GameObjectiveEvent)e;

            if (GOE.GObjv.iD == optional_precedingObjective._myObjv.iD)
            {
                if (GOE.GObjv.status == Status_GameObjective.Completed || GOE.GObjv.status == Status_GameObjective.CleanedUp)
                {
                    _myObjv.status = Status_GameObjective.Active;
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
                    myInd = (GameObject) Instantiate(Resources.Load("OLI"), transform);
                    onActivated.Invoke();
                }
                else if (GOE.GObjv.status == Status_GameObjective.Triggered)
                {
                    DialogueManager myDM = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
                    if (optional_dialogueEvent != null)
                        myDM.FireEvent(myDM.ReturnEventIndex(optional_dialogueEvent));
                    onTriggered.Invoke();
                }
                else if (GOE.GObjv.status == Status_GameObjective.Completed)
                {
                    if (myInd != null)
                    Destroy(myInd);
                    onCompleted.Invoke();
                }
                else if (GOE.GObjv.status == Status_GameObjective.CleanedUp)
                {
                    onCleanedUp.Invoke();
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
    private GameObjective _MakeObj (Type_GameObjective t, string i, string l, string d, string s)
    {
        if (t == Type_GameObjective.InteractDiscrete)
        {
            return new InteractDiscrete_GameObjective(i, l, d);
        }
        else if (t == Type_GameObjective.InteractOver)
        {
            return new InteractOver_GameObjective(i, l, d);
        }
        else if (t == Type_GameObjective.InteractOver_Sub)
        {
            return new InteractOver_Sub_GameObjective(i, l, d, s);
        }
        return null;
    }
    #endregion
}
