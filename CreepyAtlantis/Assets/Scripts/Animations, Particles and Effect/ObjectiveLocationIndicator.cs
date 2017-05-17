using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveLocationIndicator : MonoBehaviour {

    public Interactors _who;
    private Interactors who
    {
        get
        {
            return _who;
        }
        set
        {
            if (value != _who)
            {
                Debug.Log("Color change");
                _who = value;
                if (_who == Interactors.Ops)
                {
                    myC = GameObject.Find("Managers").GetComponent<ColorManager>().Ops;
                }
                else if (_who == Interactors.Doc)
                {
                    myC = GameObject.Find("Managers").GetComponent<ColorManager>().Doc;
                }
                else if (_who == Interactors.Either)
                {
                    myC = GameObject.Find("Managers").GetComponent<ColorManager>().Either;
                }
                else if (_who == Interactors.Sub)
                {
                    myC = GameObject.Find("Managers").GetComponent<ColorManager>().DANI;
                }
            }
        }
    }
    private Color myC;

    private Camera myCam;
    private Transform target;
    public LayerMask myLM;

    private GameObject arrow;
    private GameObject circle;

    private bool _circleTArrowF;
    private bool circleTArrowF
    {
        get
        {
            return _circleTArrowF;
        }
        set
        {
            if (value != _circleTArrowF)
            {
                _circleTArrowF = value;
                if (_circleTArrowF)
                {
                    circle.SetActive(true);
                    arrow.SetActive(false);
                }
                else
                {
                    circle.SetActive(false);
                    arrow.SetActive(true);
                }

            }
        }
    }


	// Use this for initialization
	void Start () {
        myCam = Camera.main;
        if (GameObject.Find("Sub") != null)
            target = GameObject.Find("Sub").transform;
        arrow = transform.GetChild(0).gameObject;
        circle = transform.GetChild(1).gameObject;


        arrow.GetComponent<Image>().color = myC;
        Material indMat = (Material)Instantiate(Resources.Load("IndicatorMat"));
        indMat.SetColor("_IndColor", myC);
        circle.GetComponent<MeshRenderer>().material = indMat;

        arrow.SetActive(false);
        circle.SetActive(false);
        circleTArrowF = true;
        circleTArrowF = false;
	}
	
	void LateUpdate() {
        if (target != null)
        {
            if (!CheckOnScreen(transform.position))
            {
                RaycastHit theHit = InterceptHit();
                ArrowPosAndAng(theHit.point);
            }

            Debug.DrawRay(transform.position, target.position - transform.position, Color.green);
        }
    }

    private bool CheckOnScreen (Vector3 pos)
    {
        if (myCam.WorldToViewportPoint(pos).x <= 1 && myCam.WorldToViewportPoint(pos).x >= 0 && myCam.WorldToViewportPoint(pos).y >= 0)
        {
            circleTArrowF = true;
            return true;
        }
        else
        {
            circleTArrowF = false;
            return false;
        }
    }

    private RaycastHit InterceptHit ()
    {
        RaycastHit emptyHit = new RaycastHit();
        RaycastHit[] myHits = Physics.RaycastAll(new Ray(transform.position, Vector3.Normalize(target.position - transform.position)), Mathf.Infinity, myLM, QueryTriggerInteraction.Collide);
        //Debug.Log(myHits.Length);
        foreach (RaycastHit myHit in myHits)
        {
            if (myHit.transform.name == "MarkerBounds")
            {
                //Debug.Log("Hit");
                return myHit;
            }
        }
        return emptyHit;
    }

    private void ArrowPosAndAng(Vector3 pos)
    {
        transform.GetChild(0).position = pos;
        transform.GetChild(0).rotation = Quaternion.Euler(0, 0, Mathf.Atan2(-target.position.y + transform.position.y, -target.position.x + transform.position.x) * Mathf.Rad2Deg);
    }

    public void AssignWho (Interactors i)
    {
        //Debug.Log("Assigning a new interactor");
        who = i;
    }
}
