using UnityEngine;
using System.Collections;

public class DialogueEvents : MonoBehaviour, IDialogueEvent {

    public IDialogue[] myLines;

    private int linesCounter;

    private bool done;
    private bool DONE
    {
        get
        {
            return done;
        }
        set
        {
            if (value != done)
            {
                if (value)
                {
                    GameObject.Find("GameStateManager").GetComponent<GameStateManager>().EndDialogue(0);
                    GameObject.Find("GameStateManager").GetComponent<GameStateManager>().EndDialogue(1);
                }
                done = value;
            }
        }
    }

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

        GameObject.Find("GameStateManager").GetComponent<GameStateManager>().SetControls(0, Controllables.dialogue);
        GameObject.Find("GameStateManager").GetComponent<GameStateManager>().SetControls(1, Controllables.dialogue);
    }

    public void NextLine()
    {
        linesCounter++;
        if (linesCounter < transform.childCount)
        {
            myLines[linesCounter].StateChoices(dialogueStates.speaking);
        }
        else
            done = true;
    }
}
