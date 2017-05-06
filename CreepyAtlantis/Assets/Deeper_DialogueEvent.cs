using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deeper_DialogueEvent : MonoBehaviour {

    public bool fireAsInterrupt;

    private Deeper_DialogueLine[] myLines;

	void Start () {
        myLines = new Deeper_DialogueLine[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            myLines[i] = transform.GetChild(i).GetComponent<Deeper_DialogueLine>();
        }
	}

    public void Fire()
    {
        if (fireAsInterrupt)
        {
            //for (int i = 0; i < myLines.Length; i++)
            //{
            //    if (myLines[i].GetType() == typeof(Deeper_DialogueStandard))
            //    {
            //        Deeper_DialogueStandard lineRef = (Deeper_DialogueStandard) myLines[i];
            //        DialogueLineTag choiceTag;
            //        if (myLines.Length == 1)
            //            EventManager.instance.Fire(new GE_Dia_Line(DialogueLinePriority.Normal, DialogueLineTag.FirstAndLast, lineRef.speaker, lineRef.description, lineRef.line, lineRef.gameObject.name, lineRef.followOnDialogueEvent));
            //        else if (i == 0)
            //            choiceTag = DialogueLineTag.Middle;
            //        else if (i == myLines.Length)
            //            choiceTag = DialogueLineTag.Middle;
            //        else
            //            choiceTag = DialogueLineTag.Middle;
            //            EventManager.instance.Fire(new GE_Dia_Line(DialogueLinePriority.Normal, choiceTag, lineRef.speaker, lineRef.description, lineRef.line, lineRef.gameObject.name, lineRef.followOnDialogueEvent));
            //    }
            //    else if (myLines[i].GetType() == typeof(Deeper_DialogueChoice))
            //    {
            //        Deeper_DialogueChoice lineRef = (Deeper_DialogueChoice)myLines[i];
            //        if (myLines.Length == 1)
            //            EventManager.instance.Fire(new GE_Dia_Line(DialogueLinePriority.Normal, DialogueLineTag.FirstAndLast, lineRef.speaker, lineRef.description, lineRef.choice1, lineRef.choice2, lineRef.choice1Event, lineRef.choice2event));
            //        else if (i == 0)
            //        else if (i == myLines.Length)
            //        else
            //    }
            //}
        }
        else
        {
            for (int i = myLines.Length -1; i >= 0; i--)
            {
                //EventManager.instance.Fire(new GE_Dia_Line(myLines[i].speaker, myLines[i].line, DialogueLinePriority.Interrupt, myLines[i].gameObject.name));
            }
        }
    }
}
