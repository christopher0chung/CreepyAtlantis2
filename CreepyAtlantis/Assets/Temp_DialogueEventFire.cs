using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_DialogueEventFire : MonoBehaviour {

	void Update () {
        if (Input.GetKeyDown(KeyCode.U))
        {
            GetComponent<Deeper_DialogueEvent>().Fire();
        }		
	}
}
