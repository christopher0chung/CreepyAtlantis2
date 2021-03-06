namespace MultiplayerWithBindingsExample
{
	using UnityEngine;
    using UnityEngine.SceneManagement;


    // This is just a simple "player" script that rotates and colors a cube
    // based on input read from the actions field.
    //
    // See comments in PlayerManager.cs for more details.
    //
    public class Player : MonoBehaviour, IPlayer
    {
        public PlayerActions Actions { get; set; }

        private float stickThresh = .5f;

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
                }
                else
                {
                    myPID = PlayerID.p2;
                }
                _playerNum = value;
            }
        }

        Renderer cachedRenderer;

        private delegate void runningControlIO();
        private runningControlIO controlIO;

        public void assignControlIO(controlSchemes scheme)
        {
            switch (scheme)
            {
                case (controlSchemes.joinIO):
                    //join screen
                    controlIO = joinIOFunc;
                    break;
                case (controlSchemes.menuIO):
                    //menu screen
                    controlIO = menuIOFunc;
                    break;
                case (controlSchemes.playIO):
                    //play input
                    controlIO = playIOFunc;
                    break;
            }
        }



        public delegate void LeftStick(float upDown, float leftRight, int pNum);
        public event LeftStick onXmitLeftStick;

        public void XmitLeftStick(float upDown, float leftRight, int pNum)
        {
            onXmitLeftStick(upDown, leftRight, pNum);
        }

        public delegate void RightStick(float upDown, float leftRight, int pNum);
        public event RightStick onXmitRightStick;

        public void XmitRightStick(float upDown, float leftRight, int pNum)
        {
            onXmitRightStick(upDown, leftRight, pNum);
        }

        public delegate void AButton(bool AButtonDown, int pNum);
        public event AButton onXmitAButton;

        public void XmitAButton(bool AButtonDown, int pNum)
        {
            onXmitAButton(AButtonDown, playerNum);
        }

        public delegate void YButton(bool YButtonDown, int pNum);
        public event YButton onXmitYButton;

        public void XmitYButton(bool YButtonDown, int pNum)
        {
            onXmitYButton(YButtonDown, playerNum);
        }

        public delegate void RBumper(bool RBumperDown, int pNum);
        public event AButton onXmitRBumper;

        public void XmitRBumper(bool RBumperDown, int pNum)
        {
            onXmitRBumper(RBumperDown, playerNum);
        }

        public delegate void LBumper(bool LBumperDown, int pNum);
        public event AButton onXmitLBumper;

        public void XmitLBumper(bool LBumperDown, int pNum)
        {
            onXmitLBumper(LBumperDown, playerNum);
        }

        void OnDisable()
        {
            if (Actions != null)
            {
                Actions.Destroy();
            }
        }

        private TitleMenu myTM;

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            myTM = GameObject.Find("Canvas").GetComponent<TitleMenu>();
            SceneManager.sceneLoaded += newSceneLoaded;
            assignControlIO(controlSchemes.joinIO);
        }


        void Start()
        {
            cachedRenderer = GetComponent<Renderer>();
            if (playerNum == 0)
                this.gameObject.tag = "Player0";
            else
                this.gameObject.tag = "Player1";
            myTM.SetState(playerNum + 1);
        }


        void Update()
        {
            //controlIO();
            TestFunc();
        }


        private void TestFunc()
        {
            if (Mathf.Abs(Actions.Rotate.X) >= stickThresh || Mathf.Abs(Actions.Rotate.Y) >= stickThresh)
            {
                EventManager.instance.Fire(new Stick_GE(myPID, Stick.Left, Mathf.Abs(Actions.Rotate.X), Mathf.Abs(Actions.Rotate.Y)));
            }
            else
            {
                EventManager.instance.Fire(new Stick_GE(myPID, Stick.Left, 0, 0));
            }

            if (Mathf.Abs(Actions.RRotate.X) >= stickThresh || Mathf.Abs(Actions.RRotate.Y) >= stickThresh)
            {
                EventManager.instance.Fire(new Stick_GE(myPID, Stick.Right, Mathf.Abs(Actions.Rotate.X), Mathf.Abs(Actions.Rotate.Y)));
            }
            else
            {
                EventManager.instance.Fire(new Stick_GE(myPID, Stick.Left, 0, 0));
            }

            if (Actions.AButton.WasPressed)
            {
                EventManager.instance.Fire(new Button_GE(myPID, Button.Action, true));
            }
            else if (Actions.AButton.WasReleased)
            {
                EventManager.instance.Fire(new Button_GE(myPID, Button.Action, false));
            }

            if (Actions.YButton.WasPressed)
            {
                EventManager.instance.Fire(new Button_GE(myPID, Button.Dialogue, true));
            }
            else if (Actions.YButton.WasReleased)
            {
                EventManager.instance.Fire(new Button_GE(myPID, Button.Dialogue, false));
            }

            if (Actions.LBumper.WasPressed)
            {
                EventManager.instance.Fire(new Button_GE(myPID, Button.Choice1, true));
            }
            else if (Actions.LBumper.WasReleased)
            {
                EventManager.instance.Fire(new Button_GE(myPID, Button.Choice1, false));
            }

            if (Actions.RBumper.WasPressed)
            {
                EventManager.instance.Fire(new Button_GE(myPID, Button.Choice2, true));
            }
            else if (Actions.RBumper.WasReleased)
            {
                EventManager.instance.Fire(new Button_GE(myPID, Button.Choice2, false));
            }

            if (Actions.Start.WasPressed)
            {
                EventManager.instance.Fire(new Button_GE(myPID, Button.Start, true));
            }
            else if (Actions.Start.WasReleased)
            {
                EventManager.instance.Fire(new Button_GE(myPID, Button.Start, false));
            }
        }
    

        //------------------------------------------------------------------------------------------------------------------------------
        // Stuff
        //------------------------------------------------------------------------------------------------------------------------------

        private void joinIOFunc ()
        {
            if (Actions == null)
            {
                // If no controller exists for this cube, just make it translucent.
                cachedRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
            }
            else
            {
                // Set object material color.
                cachedRenderer.material.color = GetColorFromInput();

                // Rotate target object.
                transform.Rotate(Vector3.down, 500.0f * Time.deltaTime * Actions.Rotate.X, Space.World);
                transform.Rotate(Vector3.right, 500.0f * Time.deltaTime * Actions.Rotate.Y, Space.World);
            }

            if (Actions.Start)
            {
                //GameObject.Find("GameStateManager").GetComponent<LevelLoader>().LoadLevel(1);
                EventManager.instance.Fire(new GE_LoadLevelRequest(1));
            }
        }

        private void menuIOFunc()
        {
            if (Actions.Start)
            {
                //GameObject.Find("GameStateManager").GetComponent<LevelLoader>().DeathLoad();
            }
        }

        private void playIOFunc()
        {
            if (Mathf.Abs(Actions.Rotate.X) >= stickThresh || Mathf.Abs(Actions.Rotate.Y) >= stickThresh)
            {
                if (onXmitLeftStick != null)
                    XmitLeftStick(Actions.Rotate.Y, Actions.Rotate.X, playerNum);
            }
            else
            {
                if (onXmitLeftStick != null)
                    XmitLeftStick(0, 0, playerNum);
            }

            if (Mathf.Abs(Actions.RRotate.X) >= stickThresh || Mathf.Abs(Actions.RRotate.Y) >= stickThresh)
            {
                if (onXmitRightStick != null)
                    XmitRightStick(Actions.RRotate.Y, Actions.RRotate.X, playerNum);
            }
            else
            {
                if (onXmitRightStick != null)
                    XmitRightStick(0, 0, playerNum);
            }

            if (Actions.AButton.WasPressed)
            {
                if (onXmitAButton != null)
                    XmitAButton(true, playerNum);
            }
            else if (Actions.AButton.WasReleased)
            {
                if (onXmitAButton != null)
                    XmitAButton(false, playerNum);
            }

            if (Actions.YButton.WasPressed)
            {
                if (onXmitYButton != null)
                    XmitYButton(true, playerNum);
            }
            else if (Actions.YButton.WasReleased)
            {
                if (onXmitYButton != null)
                    XmitYButton(false, playerNum);
            }

            if (Actions.RBumper.WasPressed)
            {
                if (onXmitRBumper != null)
                    XmitRBumper(true, playerNum);
            }
            else if (Actions.RBumper.WasReleased)
            {
                if (onXmitRBumper != null)
                    XmitRBumper(false, playerNum);
            }

            if (Actions.LBumper.WasPressed)
            {
                if (onXmitLBumper != null)
                    XmitLBumper(true, playerNum);
            }
            else if (Actions.LBumper.WasReleased)
            {
                if (onXmitLBumper != null)
                    XmitLBumper(false, playerNum);
            }
        }


        private void newSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex ==0)
            {
                assignControlIO(controlSchemes.joinIO);
            }
            if (scene.buildIndex == 1)
            {
                assignControlIO(controlSchemes.menuIO);
            }
            else
            {
                assignControlIO(controlSchemes.playIO);
                if (GetComponent<MeshRenderer>().enabled)
                {
                    GetComponent<MeshRenderer>().enabled = false;
                }
                
            }
        }

        Color GetColorFromInput()
        {
            if (Actions.Green)
            {
                return Color.green;
            }

            if (Actions.Red)
            {
                return Color.red;
            }

            if (Actions.Blue)
            {
                return Color.blue;
            }

            if (Actions.Yellow)
            {
                return Color.yellow;
            }

            return Color.white;
        }
    }
}

public enum controlSchemes { joinIO, menuIO, playIO }


