using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSlide : MonoBehaviour {

    private RectTransform myRT;
    private Text myT;

    private float xPos;
    private float timer;



	// Use this for initialization
	void Start () {
        myRT = GetComponent<RectTransform>();
        myT = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        float scalar = (timer - 3);
        scalar = Mathf.Clamp(scalar, 0, 1);
        myT.color = Color.Lerp(new Color(.706f, .706f, .706f, 0), new Color(.706f, .706f, .706f, .404f), scalar);

        xPos = -73 + 249 * ((timer - 3) / 2);
        xPos = Mathf.Clamp(xPos, -73, 176f);
        myRT.localPosition = new Vector3(xPos, -22, 0);

	}
}
