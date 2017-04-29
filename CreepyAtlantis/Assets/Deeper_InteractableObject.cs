using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Interactors {Unassigned, Ops, Doc, Either }


public class Deeper_InteractableObject : MonoBehaviour, IInteractable
{
    public Interactors whoCanInteract;
    public float iconScale;
    public float sphereRad;
    public Vector3 sphereOffset;

    [Header("Fill in to spawn on Interaction")]
    public string NameOfToSpawn;
    public Vector3 spawnOffset;

    private bool _detectFuncActive;
    private RaycastHit[] myHits;
    private Dictionary<int, string> numToName = new Dictionary<int, string>();
    private Dictionary<string, int> nameToNum = new Dictionary<string, int>();



    private GameObject myIcon;
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
                    Debug.Log("Making");
                    myIcon = (GameObject)Instantiate(Resources.Load("interactIcon"), transform.position + spawnOffset, Quaternion.identity);
                    myIcon.transform.localScale = Vector3.one * iconScale;
                    myIcon.transform.parent = transform;
                }
                else
                {
                    Destroy(myIcon);
                }
            }
        }
    }

    //-----------------------------------------------------------------
    // Init and either InitBox or InitSphere is necessary to be run in Awake()
    //-----------------------------------------------------------------

    public void Awake()
    {
        numToName.Add(0, "Character0");
        numToName.Add(1, "Character1");

        nameToNum.Add(numToName[0], 0);
        nameToNum.Add(numToName[1], 1);
    }

    void Update()
    {
        if (_detectFuncActive)
        {
            SphereCastDetect();
        }
    }

    #region Internal Functions
    //-----------------------------------------------------------------
    // Part of interface
    // Interact will be handled by parent class
    // Functionality should be overridden in OnPress and OnRelease
    //-----------------------------------------------------------------

    public void Interact(int pNum, bool pressRelease)
    {
        if ((whoCanInteract == Interactors.Ops && pNum == 0) || (whoCanInteract == Interactors.Doc && pNum == 1) || whoCanInteract == Interactors.Either)
        {
            if (pressRelease)
                OnPress(pNum);
            else
                OnRelease(pNum);
        }
    }

    public void OnPress(int pNum)
    {
        if (GetComponent<Deeper_ObjectiveObject>() != null)
        {
            GetComponent<Deeper_ObjectiveObject>().WasInteracted();
        }
        if (NameOfToSpawn != "")
        {
            Instantiate(Resources.Load(NameOfToSpawn), transform.position, Quaternion.identity);
        }
    }

    public void OnRelease(int pNum)
    {

    }

    //-----------------------------------------------------------------
    // _detectFunc should be run in Update;
    // _detectFuncActive is a bool to use if not always active.
    //-----------------------------------------------------------------

    public void SetInteractionActive(bool active)
    {
        _detectFuncActive = active;
    }

    public void SphereCastDetect()
    {
        myHits = Physics.SphereCastAll(transform.position + sphereOffset, sphereRad, Vector3.up * .05f);
        if (CastCheck(myHits) >= 0)
        {
            showInteractableIcon = true;
        }
        else
        {
            showInteractableIcon = false;
        }
    }

    int CastCheck(RaycastHit[] hits)
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
    #endregion
}