using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIPosition { Left, Center, Right }

public class UI_Base : MonoBehaviour {

    [Header("Setup for proper region response")]
    public UIPosition thisPos;
    public float defaultOpacity = 100;

    [Header("Optional - default to using ColorManager colors")]
    public bool overrideLit;
    public Color overrideLitColor;
    public bool overrideUnlit;
    public Color overrideUnlitColor;

    [HideInInspector] public Speaker thisSpeaker;
    [HideInInspector] public Color litColor;
    [HideInInspector] public Color unlitColor;

    [HideInInspector] public ColorManager myCM;

    #region Internal Functions

    virtual public void _Init()
    {
        myCM = GameObject.Find("Managers").GetComponent<ColorManager>();
    }

    public virtual void _GetSpeaker()
    {
        if (thisPos == UIPosition.Left)
        {
            if (GameObject.Find("Managers").GetComponent<SelectionManager>().C1 == SelectChoice.Doc)
                thisSpeaker = Speaker.Doc;
            else
                thisSpeaker = Speaker.Ops;
        }
        else if (thisPos == UIPosition.Right)
        {
            if (GameObject.Find("Managers").GetComponent<SelectionManager>().C2 == SelectChoice.Doc)
                thisSpeaker = Speaker.Doc;
            else
                thisSpeaker = Speaker.Ops;
        }
        else
            thisSpeaker = Speaker.DANI;
    }

    public virtual void _GetColor()
    {
        if (overrideLit)
            litColor = overrideLitColor;
        else if (thisSpeaker == Speaker.DANI)
            litColor = new Color(myCM.DANI.r, myCM.DANI.g, myCM.DANI.b, defaultOpacity);
        else if (thisSpeaker == Speaker.Doc)
            litColor = new Color(myCM.Doc.r, myCM.Doc.g, myCM.Doc.b, defaultOpacity);
        else if (thisSpeaker == Speaker.Ops)
            litColor = new Color(myCM.Ops.r, myCM.Ops.g, myCM.Ops.b, defaultOpacity);

        if (overrideUnlit)
            unlitColor = overrideUnlitColor;
        else
            unlitColor = new Color(0, 0, 0, 0);
    }
    #endregion
}
