using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deeper_DialogueLineStandard : Deeper_DialogueLine_Base
{
    [Header("What they're saying")]
    public string line;
    [Header("Optional")]
    public Deeper_DialogueEvent followOnDialogueEvent;
}