namespace MultiplayerBasicExample
{
	using InControl;
	using UnityEngine;
    using UnityEngine.SceneManagement;

	public class Player : MonoBehaviour
	{
		public InputDevice Device { get; set; }

		Renderer cachedRenderer;

        [SerializeField] private float stickThresh;

        public PlayerID myPID;
        private int _playerNum;
        public int playerNum
        {
            get
            {
                return _playerNum;
            }
            set
            {
                if (value == 0)
                {
                    myPID = PlayerID.p1;
                    gameObject.tag = "Player0";
                }
                else
                {
                    myPID = PlayerID.p2;
                    gameObject.tag = "Player1";
                }
                _playerNum = value;
            }
        }

        private TitleMenu myTM;

        private FSM<MultiplayerBasicExample.Player> _fsm;
        private float intensity;

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            myTM = GameObject.Find("Canvas").GetComponent<TitleMenu>();
            EventManager.instance.Register<P1_DialogueChoiceRumble_GE>(Rumble);
        }

        void Start()
		{
			cachedRenderer = GetComponent<Renderer>();
            Debug.Log(playerNum);
            myTM.setState(playerNum + 1);
            _fsm = new FSM<MultiplayerBasicExample.Player>(this);
            _fsm.TransitionTo<Standby>();
        }

        void Update()
        {
            InputFunc();
            _fsm.Update();
            //EventManager.instance.Fire(new Device_GE(myPID, Device));

            //if (Input.GetKeyDown(KeyCode.T))
            //{
            //    EventManager.instance.Fire(new Test_GE(Random.Range(0.00f, 1f), Random.Range(-1.0000f, 1.0000f), "ardkdk", Random.Range(2, 500)));
            //}
        }

        void Rumble(GameEvent e)
        {
            if (e.GetType() == typeof(P1_DialogueChoiceRumble_GE) && myPID == PlayerID.p1)
                _fsm.TransitionTo<RumbleState>();
            if (e.GetType() == typeof(P2_DialogueChoiceRumble_GE) && myPID == PlayerID.p2)
                _fsm.TransitionTo<RumbleState>();
        }

        private void InputFunc()
        {
            if (Mathf.Abs((float)Device.LeftStickY) >= stickThresh || Mathf.Abs((float)Device.LeftStickX) >= stickThresh)
            {
                EventManager.instance.Fire(new Stick_GE(myPID, Stick.Left, (float)Device.LeftStickY, (float)Device.LeftStickX));
            }
            else
            {
                EventManager.instance.Fire(new Stick_GE(myPID, Stick.Left, 0, 0));
            }

            if (Mathf.Abs((float)Device.RightStickY) >= stickThresh || Mathf.Abs((float)Device.RightStickX) >= stickThresh)
            {
                EventManager.instance.Fire(new Stick_GE(myPID, Stick.Left, (float)Device.RightStickY, (float)Device.RightStickX));
            }
            else
            {
                EventManager.instance.Fire(new Stick_GE(myPID, Stick.Left, 0, 0));
            }

            if (Device.Action1.WasPressed)
            {
                EventManager.instance.Fire(new Button_GE(myPID, Button.Action, true));
            }
            else if (Device.Action1.WasReleased)
            {
                EventManager.instance.Fire(new Button_GE(myPID, Button.Action, false));
            }

            if (Device.Action4.WasPressed)
            {
                EventManager.instance.Fire(new Button_GE(myPID, Button.Dialogue, true));
            }
            else if (Device.Action4.WasReleased)
            {
                EventManager.instance.Fire(new Button_GE(myPID, Button.Dialogue, false));
            }

            if (Device.LeftBumper.WasPressed)
            {
                EventManager.instance.Fire(new Button_GE(myPID, Button.Choice1, true));
            }
            else if (Device.LeftBumper.WasReleased)
            {
                EventManager.instance.Fire(new Button_GE(myPID, Button.Choice1, false));
            }

            if (Device.RightBumper.WasPressed)
            {
                EventManager.instance.Fire(new Button_GE(myPID, Button.Choice2, true));
            }
            else if (Device.RightBumper.WasReleased)
            {
                EventManager.instance.Fire(new Button_GE(myPID, Button.Choice2, false));
            }

            if (Device.Command.WasPressed)
            {
                EventManager.instance.Fire(new Button_GE(myPID, Button.Start, true));
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    if (myTM != null)
                    {
                        if (myTM.GetState() == 2)
                        {
                            GameObject.Find("GameStateManager").GetComponent<LevelLoader>().LoadLevel(GameObject.Find("GameStateManager").GetComponent<LevelLoader>().GetLevel()+1);
                            //Debug.Log(GameObject.Find("GameStateManager").GetComponent<LevelLoader>().GetLevel());
                        }
                    }
                }
            }
            else if (Device.Command.WasReleased)
            {
                EventManager.instance.Fire(new Button_GE(myPID, Button.Start, false));
            }
        }
        //------------------------------------------------
        // States for P1
        //------------------------------------------------

        private class BasicState : FSM<MultiplayerBasicExample.Player>.State
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

            }

            public override void Update()
            {
                return;
            }

            public override void OnExit()
            {

            }
        }

        private class RumbleState : BasicState
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
                Context.intensity = .5f;
            }

            public override void Update()
            {
                timer += Time.deltaTime;
                if (timer >= duration)
                {
                    TransitionTo<Standby>();
                }
            }

            public override void OnExit()
            {
                Context.intensity = 0;
            }
        }
	}
}

