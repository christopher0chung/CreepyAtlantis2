using UnityEngine;
using System.Collections;

public class ObstructionFader : MonoBehaviour {

    public GameObject myCam;
    public LayerMask myLM;
    public RaycastHit myRCH;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Ray rayToCam = new Ray (transform.position, myCam.transform.position);
        Debug.DrawRay(transform.position, myCam.transform.position);

        if(Physics.Raycast(rayToCam, out myRCH, 100, myLM))
        {
            Debug.Log(myRCH.collider.gameObject.name);
            myRCH.collider.gameObject.GetComponent<FadeScript>().Fade();
         }
    }
}
