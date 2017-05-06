using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deeper_DialogueLine : MonoBehaviour
{
    [Header ("Who is speaking")]
    public Speaker speaker;
    [Header("What it says before event starts")]
    public string description;
}

public class Deeper_DialogueStandard : Deeper_DialogueLine
{
    [Header ("What they're saying")]
    public string line;
    [Header("Optional")]
    public Deeper_DialogueEvent followOnDialogueEvent;
}

public class Deeper_DialogueChoice : Deeper_DialogueLine
{
    [Header("What they're saying")]
    public string choice1;
    public string choice2;
    [Header("Optional")]
    public Deeper_DialogueEvent choice1Event;
    public Deeper_DialogueEvent choice2event;
}
