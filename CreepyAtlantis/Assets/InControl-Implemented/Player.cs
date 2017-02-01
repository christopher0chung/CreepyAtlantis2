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

        public int playerNum;

		Renderer cachedRenderer;

        private delegate void runningControlIO();
        private runningControlIO controlIO;

        public void assignControlIO (controlSchemes scheme)
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



        public delegate void LeftStick (float upDown, float leftRight, int pNum);
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
            onXmitRBumper(LBumperDown, playerNum);
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
            myTM.setState(playerNum + 1);
        }


		void Update()
		{
            controlIO();
		}


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

            if (Actions.Start || Input.GetKeyDown(KeyCode.Space))
            {
                GameObject.Find("GameStateManager").GetComponent<LevelLoader>().LoadLevel(1);
            }
        }

        private void menuIOFunc()
        {

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
            else if (scene.buildIndex == 1)
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


