using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour, IControllable {

    public PlayerMovement myMovement;
    public PlayerAction myPA;
    public PlayerLightAngle myLA;
    public PlayerAir myPAir;

    public int charNum;

    //[SerializeField] private ControllerAdapter myAdapter;

    private bool inOut;

    #region Componenets and Objects toggled for in/out
    [Header("Things that get toggled on/off for in/out")]
    [SerializeField] private Collider myCol;
    [SerializeField] private Rigidbody myRB;
    [SerializeField] private GameObject[] modelAndBarks;
    [SerializeField] private PlayerMovement myPM;
    #endregion

    void Awake()
    {
        // Awake gets called before SceneLoaded

        myPA = GetComponent<PlayerAction>();
        myMovement = GetComponent<PlayerMovement>();
        myLA = GetComponentInChildren<PlayerLightAngle>();

        if (gameObject.name == "Character0")
            charNum = 0;
        else
            charNum = 1;

        //myAdapter = gameObject.AddComponent<ControllerAdapter>();
        //myAdapter.Initialize(charNum);

        //GameStateManager.onSetControls += SetControllerAdapter;
        //GameStateManager.onPreLoadLevel += UnSub;
        //GameStateManager.onEndDialogue += SetControllerAdapter;
        EventManager.instance.Register<Stick_GE>(LocalHandler);
        EventManager.instance.Register<Button_GE>(LocalHandler);
        EventManager.instance.Register<GE_PreLoadLevel>(LocalHandler);
        EventManager.instance.Register<GE_PlayerIngressEgress>(LocalHandler);

    }
    void Start()
    { 
        if (charNum == 0)
        {
            EventManager.instance.Fire(new GE_PlayerIngressEgress(PlayerID.p1, true));
        }
        else
        {
            EventManager.instance.Fire(new GE_PlayerIngressEgress(PlayerID.p2, true));
        }

    }

    private void LocalHandler(GameEvent e)
    {
        if (e.GetType() == typeof(GE_PreLoadLevel))
        {
            UnSub();
        }
        else if (e.GetType() == typeof(GE_PlayerIngressEgress))
        {
            GE_PlayerIngressEgress p = (GE_PlayerIngressEgress)e;
            if ((p.myID == PlayerID.p1 && charNum == 0) || (p.myID == PlayerID.p2 && charNum == 1))
            {
                inOutTimer = 0;

                inOut = p.inTrueOutFalse;
                SetInOut(p.inTrueOutFalse);
            }
        }
        else if (e.GetType() == typeof(Button_GE))
        {
            if (!inOut && inOutReady)
            {
                Button_GE b = (Button_GE)e;
                if ((b.thisPID == PlayerID.p1 && charNum == 0) || (b.thisPID == PlayerID.p2 && charNum == 1))
                {
                    if (b.button == Button.Action && b.pressedReleased)
                    {
                        AButton(b.pressedReleased, charNum);
                    }
                }
            }
        }
        else if (e.GetType() == typeof(Stick_GE))
        {
            if (!inOut)
            {
                Stick_GE s = (Stick_GE)e;
                if ((s.thisPID == PlayerID.p1 && charNum == 0) || (s.thisPID == PlayerID.p2 && charNum == 1))
                {
                    if (s.stick == Stick.Left)
                    {
                        LeftStick(s.upDown, s.leftRight, charNum);
                    }
                    if (s.stick == Stick.Right)
                    {
                        RightStick(s.upDown, s.leftRight, charNum);
                    }
                }
            }
        }
    }

    private void SetInOut (bool b)
    {
        myCol.enabled = !b;
        myPM.enabled = !b;
        foreach (GameObject g in modelAndBarks)
        {
            g.SetActive(!b);
        }

        if (!b)
        {
            transform.position = GameObject.Find("Sub").transform.position + Vector3.up * -2.5f + Vector3.right * -1;
        }
    }

    public bool inOutReady;
    private float inOutTimer;

    void Update()
    {
        if (inOut)
        {
            myRB.MovePosition(GameObject.Find("Sub").transform.position);
            myPAir.Supply(10 * Time.deltaTime);
        }
        inOutTimer += Time.deltaTime;
        if (inOutTimer >= 1)
            inOutReady = true;
        else
            inOutReady = false;

    }

    public void UnSub ()
    {
        EventManager.instance.Unregister<Stick_GE>(LocalHandler);
        EventManager.instance.Unregister<Button_GE>(LocalHandler);
        EventManager.instance.Unregister<GE_PreLoadLevel>(LocalHandler);
        EventManager.instance.Unregister<GE_PlayerIngressEgress>(LocalHandler);
        //GameStateManager.onSetControls -= SetControllerAdapter;
        //GameStateManager.onPreLoadLevel -= UnSub;
    }

    public void LeftStick(float upDown, float leftRight, int pNum) {

         myMovement.Movement(upDown, leftRight);
    }

    public void RightStick(float upDown, float leftRight, int pNum) {
        if (myMovement.myDir == facingDirection.left)
        {
            if (leftRight < -.25f || Mathf.Abs(upDown) > .25f)
                myLA.LookAngleCalc(-leftRight, upDown);
        }

        else if (myMovement.myDir == facingDirection.right)
        {
            if (leftRight > .25f || Mathf.Abs(upDown) > .25f)
                myLA.LookAngleCalc(leftRight, upDown);
        }


    }

    public void AButton(bool pushRelease, int pNum) {
        myPA.TryToInteract(pNum, pushRelease);
    }

    public void YButton(bool pushRelease, int pNum) { }

    public void LeftBumper(bool pushRelease, int pNum) { }

    public void RightBumper(bool pushRelease, int pNum) { }


    //public void SetControllerAdapter(int player, Controllables myControllable)
    //{
    //    if (myAdapter == null)
    //    {
    //        myAdapter = GetComponent<ControllerAdapter>();
    //        myAdapter.Initialize(charNum);
    //    }

    //    if (charNum == player)
    //    {
    //        Debug.Assert(myAdapter != null, "myAdapter is null");
    //        if (myControllable == Controllables.character)
    //            myAdapter.enabled = true;
    //        else if (myControllable == Controllables.dialogue)
    //            return;
    //        else
    //            myAdapter.enabled = false;
    //    }
    //}
}
