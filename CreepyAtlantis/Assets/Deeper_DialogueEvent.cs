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
            for (int i = 0; i < myLines.Length; i++)
            {
                EventManager.instance.Fire(new GE_Dia_Line(myLines[i].speaker, myLines[i].line, DialogueLinePriority.Normal, myLines[i].gameObject.name));
            }
        }
        else
        {
            for (int i = myLines.Length -1; i >= 0; i--)
            {
                EventManager.instance.Fire(new GE_Dia_Line(myLines[i].speaker, myLines[i].line, DialogueLinePriority.Interrupt, myLines[i].gameObject.name));
            }
        }
    }
}
