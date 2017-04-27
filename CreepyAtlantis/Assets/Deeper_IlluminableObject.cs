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

    private bool _showHighlight;
    [HideInInspector] public bool showHighlight
    {
        get
        {
            return _showHighlight;
        }
        set
        {
            if (value)
                letGoTimer = 0;

            if (value != _showHighlight)
            {
                _showHighlight = value;
                if (_showHighlight)
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
        showHighlight = false;
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
        ShowHighlight();
    }

    #region Internal Functions
    private void CleanUp()
    {
        letGoTimer += Time.deltaTime;
        if (letGoTimer >= .25f)
        {
            HideHighlight();
        }
    }

    private void ShowHighlight()
    {
        showHighlight = true;
    }

    private void HideHighlight()
    {
        showHighlight = false;
    }
    #endregion
}
