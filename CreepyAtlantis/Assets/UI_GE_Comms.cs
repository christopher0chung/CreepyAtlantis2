using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GE_Comms : UI_Base {

    [HideInInspector] private SpriteRenderer mySR;

    void Start () {
        EventManager.instance.Register<GE_UI_Dia>(LocalHandler);
 
        _Init();
        mySR = GetComponent<SpriteRenderer>();

        _GetSpeaker();
        _GetColor();
    }

    void LocalHandler(GameEvent e)
    {
        if (e.GetType() == typeof(GE_UI_Dia))
        {
            GE_UI_Dia u = (GE_UI_Dia)e;
            if (u.speaker == thisSpeaker)
            {
                if (u.status == DialogueStatus.Start)
                    mySR.color = litColor;
                else
                    mySR.color = unlitColor;
            }
            else
                mySR.color = unlitColor;
        }
    }
}
