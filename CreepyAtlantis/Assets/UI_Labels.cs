using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Labels : UI_Base {

    [Header ("Optional - Will default to name of speaker")]
    [SerializeField] private bool overrideTextOption;
    [SerializeField] private string overrideText;

    private TextMesh myTM;
    private string label;

	void Start () {
        myTM = GetComponent<TextMesh>();

        _Init();
        _GetSpeaker();
        _GetColor();

        if (overrideTextOption)
            myTM.text = overrideText;
        else if (thisSpeaker == Speaker.DANI)
            myTM.text = "D.A.N.I.";
        else if (thisSpeaker == Speaker.Doc)
            myTM.text = "DOC";
        else if (thisSpeaker == Speaker.Ops)
            myTM.text = "OPS";

        //Handled in base
        myTM.color = litColor;
    }

}
