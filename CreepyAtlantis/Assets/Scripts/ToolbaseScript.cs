using UnityEngine;
using System.Collections;

public class ToolbaseScript : MonoBehaviour {

    public KeyCode drillOn;
    public MonoBehaviour placeHolder;
    private ToolBitInterface myCurrentToolScript;

	// Use this for initialization
	void Start () {
        myCurrentToolScript = GetComponentInChildren<ToolBitInterface>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey(drillOn))
            myCurrentToolScript.OnState();
	
	}
}
