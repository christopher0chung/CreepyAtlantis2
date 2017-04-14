using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumbleManager : MonoBehaviour {

    private FSM<RumbleManager> _fsm_P1;
    private float _P1_Intensity;

    private FSM<RumbleManager> _fsm_P2;
    private float _P2_Intensity;

    void Awake()
    {
        EventManager.instance.Register<P1_DialogueChoiceRumble_GE>(P1_DialogueRumble);
        EventManager.instance.Register<P2_DialogueChoiceRumble_GE>(P2_DialogueRumble);
    }

    void Start () {
        _fsm_P1 = new FSM<RumbleManager>(this);
        _fsm_P1.TransitionTo<Standby_P1>();

        _fsm_P2 = new FSM<RumbleManager>(this);
        _fsm_P2.TransitionTo<Standby_P2>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            EventManager.instance.Fire(new P1_DialogueChoiceRumble_GE());
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            EventManager.instance.Fire(new P2_DialogueChoiceRumble_GE());
        }

        _fsm_P1.Update();
        _fsm_P2.Update();

        XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.One, 0, _P1_Intensity);
        XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.Two, 0, _P2_Intensity);
    }

    private void P1_DialogueRumble(GameEvent myGE)
    {
        _fsm_P1.TransitionTo<P1_DialogueRumble_State>();
    }

    private void P2_DialogueRumble(GameEvent myGE)
    {
        _fsm_P2.TransitionTo<P2_DialogueRumble_State>();
    }

    //------------------------------------------------
    // States for P1
    //------------------------------------------------

    private class BasicState : FSM<RumbleManager>.State
    {
        public string name;
    }

    private class Standby_P1 : BasicState
    {
        public override void Init()
        {
            name = "Standby";
        }

        public override void OnEnter()
        {

        }

        public override void Update()
        {
            return;
        }

        public override void OnExit()
        {

        }
    }

    private class P1_DialogueRumble_State : BasicState
    {
        private float intensity;
        private float duration;

        private float timer;

        public override void Init()
        {
            name = "P1_DialogueRumble_State";
        }

        public override void OnEnter()
        {
            timer = 0;
            duration = .15f;
            Context._P1_Intensity = .5f;
        }

        public override void Update()
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                TransitionTo<Standby_P1>();
            }
        }

        public override void OnExit()
        {
            Context._P1_Intensity = 0;
        }
    }

    //------------------------------------------------
    // States for P2
    //------------------------------------------------

    private class Standby_P2 : BasicState
    {
        public override void Init()
        {
            name = "Standby";
        }

        public override void OnEnter()
        {

        }

        public override void Update()
        {
            return;
        }

        public override void OnExit()
        {

        }
    }

    private class P2_DialogueRumble_State : BasicState
    {
        private float intensity;
        private float duration;

        private float timer;

        public override void Init()
        {
            name = "P2_DialogueRumble_State";
        }

        public override void OnEnter()
        {
            timer = 0;
            duration = .15f;
            Context._P2_Intensity = .5f;
        }

        public override void Update()
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                TransitionTo<Standby_P1>();
            }
        }

        public override void OnExit()
        {
            Context._P2_Intensity = 0;
        }
    }
}