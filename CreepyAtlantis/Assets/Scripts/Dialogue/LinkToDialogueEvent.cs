using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkToDialogueEvent : MonoBehaviour {

    public GameObject eventLink;

    private DialogueManager myDM;

    private void Start()
    {
        myDM = transform.root.gameObject.GetComponent<DialogueManager>();
    }

    public void Link()
    {
        StartCoroutine(TriggerLink());
    }

    private IEnumerator TriggerLink()
    {
        yield return new WaitForSeconds(.4f);
        for (int i = 0; i < myDM.myEventsNames.Count; i++)
        {
            if (myDM.myEventsNames[i] == eventLink.name)
                myDM.myEvents[i].TRIGGER = true;
        }
    }
}
