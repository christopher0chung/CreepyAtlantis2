using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene1DialogueTrigger : MonoBehaviour {

    void Start()
    {
        Invoke("TriggerDialogue", 1);
    }

    private void TriggerDialogue()
    {
        GetComponent<DialogueManager>().myEvents[0].TRIGGER = true;
    }
}
