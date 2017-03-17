using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour, IControllable {

    [SerializeField] private SelectChoice Position;
    private SelectionManager mySM;
    [SerializeField] private int myNum;

    private ControllerAdapter myCA;

    private bool _pressRight;
    private bool pressRight
    {
        get
        {
            return _pressRight;
        }
        set
        {
            if (value != _pressRight)
            {
                //Debug.Log("PressRight" + value);
                _pressRight = value;
                if (_pressRight)
                {
                    Position = Selections(SelectInputs.Right);
                    mySM.UpdateSelection(Position, myNum);
                    LerpToSpot(Position);
                }
            }
        } 
    }

    private bool _pressLeft;
    private bool pressLeft
    {
        get
        {
            return _pressLeft;
        }
        set
        {
            if (value != _pressLeft)
            {
                _pressLeft = value;
                if (_pressLeft)
                {
                    Position = Selections(SelectInputs.Left);
                    mySM.UpdateSelection(Position, myNum);
                    LerpToSpot(Position);
                }
            }
        }
    }

    void Start () {
        Position = SelectChoice.None;
        mySM = GameObject.FindGameObjectWithTag("Managers").GetComponent<SelectionManager>();
        if (gameObject.name == "Controller1")
            myNum = 0;
        else
            myNum = 1;

        mySM.UpdateSelection(Position, myNum);

        gameObject.AddComponent<ControllerAdapter>();
        myCA = GetComponent<ControllerAdapter>();
        Debug.Log(gameObject.name + " run Initialize for " + myNum);
        myCA.Initialize(myNum);
        GameStateManager.onSetControls += SetControllerAdapter;
        GameStateManager.onPreLoadLevel += UnSub;
    }

    public void UnSub()
    {
        GameStateManager.onSetControls -= SetControllerAdapter;
        GameStateManager.onPreLoadLevel -= UnSub;
    }

    void OnEnable()
    {
        if (myCA != null)
        myCA.enabled = true;
    }

    void OnDisable()
    {
        if (myCA != null)
            myCA.enabled = false;
    }
	
    private SelectChoice Selections (SelectInputs sI)
    {
        if (sI == SelectInputs.Left)
        {
            if (Position == SelectChoice.Ops)
            {
                return SelectChoice.Ops;
            }
            else if (Position == SelectChoice.None)
            {
                return SelectChoice.Ops;
            }
            else
            {
                return SelectChoice.None;
            }
        }
        else
        {
            if (Position == SelectChoice.Ops)
            {
                return SelectChoice.None;
            }
            else if (Position == SelectChoice.None)
            {
                return SelectChoice.Doc;
            }
            else
            {
                return SelectChoice.Doc;
            }
        }
    }

    private void LerpToSpot (SelectChoice mySP)
    {
        if (mySP == SelectChoice.Ops)
        {
            //transform.position = Vector3.Lerp(transform.position, new Vector3(-256, transform.position.y, transform.position.z), 8 * Time.deltaTime);
            transform.position = new Vector3(-250, transform.position.y, transform.position.z);
        }
        else if (mySP == SelectChoice.None)
        {
            //transform.position = Vector3.Lerp(transform.position, new Vector3(0, transform.position.y, transform.position.z), 8 * Time.deltaTime);
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
        else
        {
            //transform.position = Vector3.Lerp(transform.position, new Vector3(256, transform.position.y, transform.position.z), 8 * Time.deltaTime);
            transform.position = new Vector3(250, transform.position.y, transform.position.z);
        }
    }

    public void LeftStick (float upDown, float leftRight, int pNum)
    {
        //Debug.Log(leftRight + " " + upDown + " " + pNum);
        if ((gameObject.name == "Controller1" && pNum == 0) || (gameObject.name == "Controller2" && pNum == 1))
        {
            if (leftRight > .7f)
            {
                pressRight = true;
            }
            else if (leftRight < -.7f)
            {
                pressLeft = true;
            }
            else
            {
                pressRight = false;
                pressLeft = false;
            }
        }

    }

    public void RightStick(float upDown, float leftRight, int pNum) { }

    public void AButton(bool pushRelease, int pNum)
    {
        if (pNum == myNum && pushRelease)
        {
            mySM.ConfirmSelect(myNum);
        }
    }

    public void YButton(bool pushRelease, int pNum) { }

    public void LeftBumper(bool pushRelease, int pNum) { }

    public void RightBumper(bool pushRelease, int pNum) { }

    public void SetControllerAdapter(int player, Controllables myControllable)
    {
        myCA.enabled = true;
    }
}

public enum SelectInputs { Left, Right }
public enum SelectChoice { Ops, None, Doc }
