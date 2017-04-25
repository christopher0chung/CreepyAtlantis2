using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deeper_InteractableObject : MonoBehaviour, IInteractable, IIlluminable
{

    //Subclass Sandbox - Interactable Objects
    public bool _detectFuncActive;
    public delegate void detectFunc();
    public detectFunc _detectFunc;

    public Dictionary<int, string> numToName = new Dictionary<int, string>();
    public Dictionary<string, int> nameToNum = new Dictionary<string, int>();

    public RaycastHit[] myHits;

    #region Hidden Properties
    [HideInInspector] public TriggerShape triggerShape;

    [HideInInspector] public Vector3 iconOffset;

    [HideInInspector] public Vector3 boxDim;
    [HideInInspector] public Vector3 boxLoc;

    [HideInInspector] public float sphereRad;
    [HideInInspector] public Vector3 sphereLoc;

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
            if (value)
                letGoTimer = 0;

            if (value != _showInteractableIcon)
            {
                _showInteractableIcon = value;
                if (_showInteractableIcon)
                {
                    myIcon = (GameObject)Instantiate(Resources.Load("interactIcon"), transform.position + iconOffset, Quaternion.identity, transform);
                    myIcon.transform.localScale = Vector3.one * iconScale;
                }
                else
                {
                    Destroy(myIcon);
                }
            }
        }
    }

    private float iconScale;

    private float letGoTimer;
    #endregion

    //-----------------------------------------------------------------
    // Init and either InitBox or InitSphere is necessary to be run in Awake()
    //-----------------------------------------------------------------

    virtual public void Init(TriggerShape shape, Vector3 offset, float iScale)
    {
        //Should run on awake
        triggerShape = shape;

        iconOffset = offset;

        iconScale = iScale;

        numToName.Add(0, "Character0");
        numToName.Add(1, "Character1");

        nameToNum.Add(numToName[0], 0);
        nameToNum.Add(numToName[1], 1);
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
    // Part of IIlluminable
    //-----------------------------------------------------------------

    virtual public void Illuminate(GameObject who)
    {
        ShowInteractable();
    }


    //-----------------------------------------------------------------
    // Part of interface
    // Interact will be handled by parent class
    // Functionality should be overridden in OnPress and OnRelease
    //-----------------------------------------------------------------

    virtual public void Interact(int pNum, bool pressRelease)
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

    virtual public void SetInteractionActive(bool active)
    {
        _detectFuncActive = active;
    }

    virtual public void BoxCastDetect()
    {
        myHits = Physics.BoxCastAll(boxLoc, boxDim / 2, Vector3.up * .05f);
        if (CastCheck(myHits) >= 0)
        {
            ShowInteractable();
        }
    }

    virtual public void SphereCastDetect()
    {
        myHits = Physics.SphereCastAll(sphereLoc, sphereRad, Vector3.up * .05f);
        if (CastCheck(myHits) >= 0)
        {
            ShowInteractable();
        }
    }

    protected int CastCheck(RaycastHit[] hits)
    {
        foreach (RaycastHit hit in hits)
        {
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

    protected void CleanUp()
    {
        letGoTimer += Time.deltaTime;
        if (letGoTimer >= .25f)
        {
            HideInteractable();
        }
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
