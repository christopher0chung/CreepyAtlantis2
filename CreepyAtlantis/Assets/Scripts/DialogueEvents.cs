using UnityEngine;
using System.Collections;

public class DialogueEvents : MonoBehaviour, IDialogueEvent {

    public IDialogue[] myLines;

    private int linesCounter;

	// Use this for initialization
	void Start () {
        GetMyLines();
	}
	
    public void GetMyLines()
    {
        myLines = new IDialogue[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            myLines[i] = transform.GetChild(i).GetComponent<IDialogue>();
            //myLines[i].DebugPrint();
        }
    }

    public void StartLines()
    {
        myLines[linesCounter].StateChoices(dialogueStates.speaking);
    }

    public void NextLine()
    {
        linesCounter++;
        if (linesCounter < transform.childCount)
        {
            myLines[linesCounter].StateChoices(dialogueStates.speaking);
        }
        else
            return;
    }
}
