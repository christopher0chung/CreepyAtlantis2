using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deeper_DialogueChoice : Deeper_DialogueLine_Base
{
    [Header("What they're saying")]
    public string choice1;
    public string choice2;
    [Header("Optional")]
    public Deeper_DialogueEvent_Base choice1Event;
    public Deeper_DialogueEvent_Base choice2Event;
}