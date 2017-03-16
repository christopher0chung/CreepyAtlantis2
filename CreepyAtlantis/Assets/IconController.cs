using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour {

    [SerializeField] private SelectChoice Position;
    private SelectionManager mySM;
    [SerializeField] private int myNum;

	// Use this for initialization
	void Start () {
        Position = SelectChoice.None;
        mySM = GameObject.FindGameObjectWithTag("Managers").GetComponent<SelectionManager>();
        if (gameObject.name == "Controller1")
            myNum = 0;
        else
            myNum = 1;

        mySM.UpdateSelection(Position, myNum);
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

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("read left");
            Position = Selections(SelectInputs.Left);
            mySM.UpdateSelection(Position, myNum);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("read right");
            Position = Selections(SelectInputs.Right);
            mySM.UpdateSelection(Position, myNum);
        }
        LerpToSpot(Position);
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
}

public enum SelectInputs { Left, Right }
public enum SelectChoice { Ops, None, Doc }
