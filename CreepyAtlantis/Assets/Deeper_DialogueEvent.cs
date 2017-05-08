using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deeper_DialogueEvent : MonoBehaviour {

    public bool fireAsInterrupt;

    private Deeper_DialogueLine_Base[] myLines;

	void Start () {
        myLines = new Deeper_DialogueLine_Base[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            myLines[i] = transform.GetChild(i).GetComponent<Deeper_DialogueLine_Base>();
        }
	}

    public void Fire()
    {
        if (fireAsInterrupt)
        {
            for (int i = 0; i < myLines.Length; i++)
            {
                if (myLines[i].GetType() == typeof(Deeper_DialogueStandard))
                {
                    Deeper_DialogueStandard lineRef = (Deeper_DialogueStandard)myLines[i];
                    DialogueLineTag lineTage;
                    if (myLines.Length == 1)
                        lineTage = DialogueLineTag.FirstAndLast;
                    else if (i == 0)
                        lineTage = DialogueLineTag.First;
                    else if (i == myLines.Length)
                        lineTage = DialogueLineTag.Last;
                    else
                        lineTage = DialogueLineTag.Middle;
                    EventManager.instance.Fire(new GE_Dia_Line(DialogueLinePriority.Interrupt, lineTage, lineRef.speaker, lineRef.description, lineRef.line, lineRef.gameObject.name, lineRef.followOnDialogueEvent));
                }
                else if (myLines[i].GetType() == typeof(Deeper_DialogueChoice))
                {
                    Deeper_DialogueChoice lineRef = (Deeper_DialogueChoice)myLines[i];
                    DialogueLineTag lineTag;
                    if (myLines.Length == 1)
                        lineTag = DialogueLineTag.FirstAndLast;
                    else if (i == 0)
                        lineTag = DialogueLineTag.First;
                    else if (i == myLines.Length)
                        lineTag = DialogueLineTag.Last;
                    else
                        lineTag = DialogueLineTag.Middle;
                    EventManager.instance.Fire(new GE_Dia_Line(DialogueLinePriority.Interrupt, lineTag, lineRef.speaker, lineRef.description, lineRef.choice1, lineRef.choice2, lineRef.choice1Event, lineRef.choice2event));
                }
            }
        }
        else
        {
            for (int i = 0; i < myLines.Length; i++)
            {
                if (myLines[i].GetType() == typeof(Deeper_DialogueStandard))
                {
                    Deeper_DialogueStandard lineRef = (Deeper_DialogueStandard)myLines[i];
                    DialogueLineTag lineTag;
                    if (myLines.Length == 1)
                        lineTag = DialogueLineTag.FirstAndLast;
                    else if (i == 0)
                        lineTag = DialogueLineTag.First;
                    else if (i == myLines.Length)
                        lineTag = DialogueLineTag.Last;
                    else
                        lineTag = DialogueLineTag.Middle;
                    EventManager.instance.Fire(new GE_Dia_Line(DialogueLinePriority.Normal, lineTag, lineRef.speaker, lineRef.description, lineRef.line, lineRef.gameObject.name, lineRef.followOnDialogueEvent));
                }
                else if (myLines[i].GetType() == typeof(Deeper_DialogueChoice))
                {
                    Deeper_DialogueChoice lineRef = (Deeper_DialogueChoice)myLines[i];
                    DialogueLineTag lineTag;
                    if (myLines.Length == 1)
                        lineTag = DialogueLineTag.FirstAndLast;
                    else if (i == 0)
                        lineTag = DialogueLineTag.First;
                    else if (i == myLines.Length)
                        lineTag = DialogueLineTag.Last;
                    else
                        lineTag = DialogueLineTag.Middle;
                    EventManager.instance.Fire(new GE_Dia_Line(DialogueLinePriority.Normal, lineTag, lineRef.speaker, lineRef.description, lineRef.choice1, lineRef.choice2, lineRef.choice1Event, lineRef.choice2event));
                }
            }
        }
    }
}
