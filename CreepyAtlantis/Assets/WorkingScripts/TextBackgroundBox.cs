using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBackgroundBox : MonoBehaviour {

    Text parent;
    Image myImg;

	// Use this for initialization
	void Start () {
        parent = transform.parent.gameObject.GetComponent<Text>();
        myImg = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if (parent.text != "")
        {
            myImg.enabled = true;
        }
        else
        {
            myImg.enabled = false;
        }
    }
}
