using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GE_WaterSub : UI_Base {

    [HideInInspector]
    private SpriteRenderer mySR;

    [Header("Icon Type")]
    [SerializeField] IngressEgressIcon myType;

    void Start()
    {
        EventManager.instance.Register<GE_PlayerIngressEgress>(LocalHandler);

        _Init();
        mySR = GetComponent<SpriteRenderer>();

        _GetSpeaker();
        _GetColor();
    }

    void LocalHandler(GameEvent e)
    {
        if (e.GetType() == typeof(GE_PlayerIngressEgress))
        {
            GE_PlayerIngressEgress i = (GE_PlayerIngressEgress)e;
            if ((i.myID == PlayerID.p1 && thisSpeaker == Speaker.Ops) || (i.myID == PlayerID.p2 && thisSpeaker == Speaker.Doc))
            {
                if ((i.inTrueOutFalse && myType == IngressEgressIcon.Sub) || (!i.inTrueOutFalse && myType == IngressEgressIcon.Water))
                    mySR.color = litColor;
                else
                    mySR.color = unlitColor;
            }
        }
    }
}
public enum IngressEgressIcon { Sub, Water }