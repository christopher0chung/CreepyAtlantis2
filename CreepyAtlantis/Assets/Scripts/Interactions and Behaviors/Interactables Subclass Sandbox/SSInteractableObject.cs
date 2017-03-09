using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSInteractableObject : MonoBehaviour, IInteractable {

    //Subclass Sandbox - Interactable Objects

    [HideInInspector] public TriggerShape triggerShape;

    [HideInInspector] public AssetDepot myAD;

    public bool _detectFuncActive;
    public delegate void detectFunc();
    public detectFunc _detectFunc;

    [HideInInspector] public Vector3 iconOffset;

    [HideInInspector] public Vector3 boxDim;
    [HideInInspector] public Vector3 boxLoc;

    [HideInInspector] public float sphereRad;
    [HideInInspector] public Vector3 sphereLoc;

    public RaycastHit[] myHits;

    public Dictionary<int, string> numToName = new Dictionary<int, string>();
    public Dictionary<string, int> nameToNum = new Dictionary<string, int>();

    [HideInInspector] public GameObject myIcon;
    private bool _showInteractableIcon;
    [HideInInspector] public bool showInteractableIcon
    {
        get
        {
            return _showInteractableIcon;
        }
        set
        {
            if (value != _showInteractableIcon)
            {
                _showInteractableIcon = value;
                if (_showInteractableIcon)
                {
                    myIcon = myAD.DepotRequest(DepotObjects.interactIcon);
                    myIcon.transform.position = transform.position + iconOffset;
                    //Debug.Log("CreateIcon");
                }
                else
                {
                    myAD.DepotDeposit(DepotObjects.interactIcon, myIcon);
                    //Debug.Log("DestroyIcon");
                }
            }
        }
    }

    //-----------------------------------------------------------------
    // Init and either InitBox or InitSphere is necessary to be run in Awake()
    //-----------------------------------------------------------------

    virtual public void Init(TriggerShape shape, Vector3 offset)
    {
        //Should run on awake
        triggerShape = shape;

        iconOffset = offset;

        numToName.Add(0, "Character0");
        numToName.Add(1, "Character1");

        nameToNum.Add(numToName[0], 0);
        nameToNum.Add(numToName[1], 1);

        myAD = GameObject.Find("AssetDepot").GetComponent<AssetDepot>();
    }
    virtual public void InitBox(Vector3 dim, Vector3 loc)
    {
        boxDim = dim;
        boxLoc = loc;
        _detectFunc = BoxCastDetect;
    }
    virtual public void InitSphere(float rad, Vector3 loc)
    {
        sphereRad = rad;
        sphereLoc = loc;
        _detectFunc = SphereCastDetect;
    }

    //-----------------------------------------------------------------
    // Part of interface
    // Interact will be handled by parent class
    // Functionality should be overridden in OnPress and OnRelease
    //-----------------------------------------------------------------

    virtual public void Interact (int pNum, bool pressRelease)
    {
        if (pressRelease)
            OnPress(pNum);
        else
            OnRelease(pNum);
    }

    virtual public void OnPress(int pNum)
    {

    }

    virtual public void OnRelease(int pNum)
    {

    }

    //-----------------------------------------------------------------
    // _detectFunc should be run in Update;
    // _detectFuncActive is a bool to use if not always active.
    //-----------------------------------------------------------------

    virtual public void SetInteractionActive (bool active)
    {
        _detectFuncActive = active;
    }

    virtual public void BoxCastDetect ()
    {
        myHits = Physics.BoxCastAll(boxLoc, boxDim / 2, Vector3.up * .05f);
        if (CastCheck(myHits) >=0)
        {
            ShowInteractable();
        }
        else
        {
            HideInteractable();
        }
    }

    virtual public void SphereCastDetect ()
    {
        myHits = Physics.SphereCastAll(sphereLoc, sphereRad, Vector3.up * .05f);
        //Debug.Log(CastCheck(myHits));
        if (CastCheck(myHits) >= 0)
        {
            ShowInteractable();
        }
        else
        {
            HideInteractable();
        }
    }

    protected int CastCheck (RaycastHit[] hits)
    {
        foreach (RaycastHit hit in hits)
        {
            //Debug.Log(hit.collider.transform.root.gameObject.name);
            if (hit.collider.transform.root.gameObject.name == numToName[0])
            {
                return 0;
            }
            else if (hit.collider.transform.root.gameObject.name == numToName[1])
            {
                return 1;
            }
        }
        return -1;
    }

    protected void ShowInteractable()
    {
        showInteractableIcon = true;
    }
    
    public void HideInteractable()
    {
        showInteractableIcon = false;
    }
}
public enum TriggerShape { box, sphere }