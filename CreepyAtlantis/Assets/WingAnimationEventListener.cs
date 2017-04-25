using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingAnimationEventListener : MonoBehaviour {

    public enum WingSide { Port, Stbd }
    [SerializeField] private WingSide WingSelector;

    public enum WingStates { Retracted, Deployed, Operating, PrepForStow }

    private Vector3 wingRetracted;
    private Vector3 wingDeployed;
    //private Vector3 wingDeployed_Offset;

    private FSM<WingAnimationEventListener> _fsm;

    private float timer;
    [SerializeField] private float wingTwitchTime;

	// Use this for initialization
	void Start () {
        _fsm = new FSM<WingAnimationEventListener>(this);
        _fsm.TransitionTo<Operate>();

        if (WingSelector == WingSide.Port)
        {
            wingRetracted = new Vector3(0, 0, 90);
        }
        else
        {
            wingRetracted = new Vector3(0, 0, -90);
        }
        wingDeployed = new Vector3(-90, 0, 0);

        EventManager.instance.Register<Character_Grounded_GE>(DriveState);
	}

    //void OnEnable()
    //{
    //    _fsm.TransitionTo<Operate>();
    //}

    // Update is called once per frame
    void Update () {
        _fsm.Update();
    }

    void DriveState(GameEvent e)
    {
        if (e.GetType() == typeof(Character_Grounded_GE))
        {
            Character_Grounded_GE g_GE = (Character_Grounded_GE)e;
            if (g_GE.Name == transform.root.gameObject.name)
            {
                if (g_GE.G == GroundStates.Grounded)
                {
                    _fsm.TransitionTo<PrepForStow>();
                }
                else
                {
                    if (!(((BasicState)_fsm.CurrentState).name == "Deploy" || ((BasicState)_fsm.CurrentState).name == "Operate"))
                        _fsm.TransitionTo<Deploy>();
                }
            }
        }
    }

    #region Finite State Machine States
    //---------------------------------------------------
    // FSM States
    //---------------------------------------------------

    private class BasicState : FSM<WingAnimationEventListener>.State
    {
        public string name;
    }

    private class Standby : BasicState
    {
        public override void Init()
        {
            name = "Standby";
        }

        public override void OnEnter()
        {
            //Debug.Log("In " + name);
            
        }

        public override void Update()
        {
            return;
        }

        public override void OnExit()
        {

        }
    }

    private class Retract : BasicState
    {
        public override void Init()
        {
            name = "Retract";
        }

        public override void OnEnter()
        {
            //Debug.Log("In " + name);

        }

        public override void Update()
        {
            Context.transform.localRotation = Quaternion.Slerp(Context.transform.localRotation, Quaternion.Euler(Context.wingRetracted), .07f);
            if (Quaternion.Angle(Context.transform.localRotation, Quaternion.Euler(Context.wingRetracted)) <= 1)
                TransitionTo<Standby>();
        }

        public override void OnExit()
        {
            Context.transform.rotation = Quaternion.Euler(Context.wingRetracted);
        }
    }

    private class Deploy : BasicState
    {
        public override void Init()
        {
            name = "Deploy";
        }

        public override void OnEnter()
        {
            //Debug.Log("In " + name);

        }

        public override void Update()
        {
            Context.transform.localRotation = Quaternion.Slerp(Context.transform.localRotation, Quaternion.Euler(Context.wingDeployed), .07f);
            if (Quaternion.Angle(Context.transform.localRotation, Quaternion.Euler(Context.wingDeployed)) <= 1)
                TransitionTo<Operate>();
        }

        public override void OnExit()
        {
            Context.transform.localRotation = Quaternion.Euler(Context.wingDeployed);
        }
    }

    private class Operate : BasicState
    {
        private float timer;
        private Vector3 wingDeployed_Offset;
        public override void Init()
        {
            name = "Operate";
        }

        public override void OnEnter()
        {
            //Debug.Log("In " + name);
            timer = 0;
        }

        public override void Update()
        {
            timer += Time.deltaTime;
            if (timer >= Context.wingTwitchTime)
            {
                timer = 0;
                wingDeployed_Offset = new Vector3(
                    Random.Range(-45, 45),
                    Random.Range(-30, 30),
                    Random.Range(-30, 30));
                //Debug.Log(wingDeployed_Offset);
            }

            Context.transform.localRotation = Quaternion.Slerp(Context.transform.localRotation, Quaternion.Euler(Context.wingDeployed + wingDeployed_Offset), .07f);
        }

        public override void OnExit()
        {

        }
    }

    private class PrepForStow : BasicState
    {
        public override void Init()
        {
            name = "PrepForStow";
        }

        public override void OnEnter()
        {
            //Debug.Log("In " + name);

        }

        public override void Update()
        {
            Context.transform.localRotation = Quaternion.Slerp(Context.transform.localRotation, Quaternion.Euler(Context.wingDeployed), .07f);
            if (Quaternion.Angle(Context.transform.localRotation, Quaternion.Euler(Context.wingDeployed)) <= 1)
                TransitionTo<Retract>();
        }

        public override void OnExit()
        {
            Context.transform.localRotation = Quaternion.Euler(Context.wingDeployed);
        }
    }
    #endregion
}
