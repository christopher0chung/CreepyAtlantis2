using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSpawner : MonoBehaviour {

    int counter;
    float timer;

    private DialogueManager myDM;
    public DialogueEvents theDE;

	void Start() {
        myDM = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (counter >= 20)
            return;


        timer += Time.deltaTime;
        if (timer > 7)
        {
            timer -= 8;
            counter++;
            if (counter == 8)
                myDM.FireEvent(myDM.ReturnEventIndex(theDE));
            Instantiate(Resources.Load("Shark"), new Vector3(-30, Random.Range(5f, 30f), Random.Range(6, 10)), Quaternion.identity);
        }


	}
}
