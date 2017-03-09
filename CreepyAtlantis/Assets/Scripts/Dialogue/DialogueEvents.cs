using UnityEngine;
using System.Collections;

public class DialogueEvents : MonoBehaviour, IDialogueEvent {

    public IDialogue[] myLines;

    private int linesCounter;

    private bool _done;
    private bool done
    {
        get
        {
            return _done;
        }
        set
        {
            if (value != _done)
            {
                if (value)
                {
                    GameObject.Find("GameStateManager").GetComponent<GameStateManager>().EndDialogue(0);
                    GameObject.Find("GameStateManager").GetComponent<GameStateManager>().EndDialogue(1);
                }
                _done = value;
            }
        }
    }

    public int lineCount;

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

        lineCount = transform.childCount;
    }

    public void StartLines()
    {
        //Debug.Log("Got to startLines");
        myLines[linesCounter].StateChoices(dialogueStates.speaking);

        GameObject.Find("GameStateManager").GetComponent<GameStateManager>().SetControls(0, Controllables.dialogue);
        GameObject.Find("GameStateManager").GetComponent<GameStateManager>().SetControls(1, Controllables.dialogue);
    }

    public void NextLine()
    {
        StartCoroutine(DelayedNextLine());
    }

    private IEnumerator DelayedNextLine ()
    {
        yield return new WaitForSeconds(.3f);
        linesCounter++;
        if (linesCounter < transform.childCount)
        {
            myLines[linesCounter].StateChoices(dialogueStates.speaking);
        }
        else
        {
            done = true;
        }
    }
}
