using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_DialogueEventFire : MonoBehaviour {

	void Start () {
            GetComponent<Deeper_DialogueEvent_Base>().Fire();	
	}
}
