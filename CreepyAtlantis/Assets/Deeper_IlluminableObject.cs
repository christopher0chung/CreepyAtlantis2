﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deeper_IlluminableObject : MonoBehaviour, IIlluminable
{
    #region ColorSet
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
                    myC = GameObject.Find("GameStateManager").GetComponent<ColorManager>().Ops;
                }
                else if (_who == Interactors.Doc)
                {
                    myC = GameObject.Find("GameStateManager").GetComponent<ColorManager>().Doc;
                }
                else if (_who == Interactors.Either)
                {
                    myC = GameObject.Find("GameStateManager").GetComponent<ColorManager>().Either;
                }
            }
        }
    }
    private Color myC;
    #endregion

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
                    newMat.SetColor("_OutlineColor", myC);
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

    public void AssignWho(Interactors i)
    {
        who = i;
    }
}
