using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deeper_IlluminableObject : MonoBehaviour, IIlluminable
{

    //Subclass Sandbox - Interactable Objects
    [Header("Required Fields")]
    public MeshRenderer myMR;
    public string nameOfBaseMat;

    [ContextMenu("Do Something")]
    void DoSomething()
    {
        Debug.Log("Perform operation");
    }

    #region Hidden Properties
    private Material newMat;

    private bool _showInteractable;
    [HideInInspector] public bool showInteractable
    {
        get
        {
            return _showInteractable;
        }
        set
        {
            if (value)
                letGoTimer = 0;

            if (value != _showInteractable)
            {
                _showInteractable = value;
                if (_showInteractable)
                {
                    newMat.SetFloat("_OutlineWidth", .0003f);
                }
                else
                {
                    newMat.SetFloat("_OutlineWidth", 0);
                }
            }
        }
    }
    private float letGoTimer;
    #endregion

    void Awake()
    {
        
    }

    void Start()
    {
        newMat = (Material)Instantiate(Resources.Load(nameOfBaseMat));
        myMR.material = newMat;
        showInteractable = false;
    }

    void Update()
    {
        CleanUp();
    }

    //-----------------------------------------------------------------
    // Part of IIlluminable
    //-----------------------------------------------------------------

    virtual public void Illuminate(GameObject who)
    {
        ShowInteractable();
    }

    #region Internal Functions
    private void CleanUp()
    {
        letGoTimer += Time.deltaTime;
        if (letGoTimer >= .25f)
        {
            HideInteractable();
        }
    }

    private void ShowInteractable()
    {
        showInteractable = true;
    }

    private void HideInteractable()
    {
        showInteractable = false;
    }
    #endregion
}
