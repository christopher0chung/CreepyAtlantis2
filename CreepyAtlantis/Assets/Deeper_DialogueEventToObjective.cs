using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deeper_DialogueEventToObjective : Deeper_DialogueEvent {

    [Header ("The Objective to trigger")]
    public Deeper_ObjectiveObject myO;

    [Header("Optional - Subsequent Dialogue to trigger")]
    public Deeper_DialogueEvent dEvent;

    public override void Fire()
    {
        myO.WasInteracted();
        if (dEvent != null)
            dEvent.Fire();
    }
}
