using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeIn : MonoBehaviour {

    private Text myT;
    private float timer;

	// Use this for initialization
	void Start () {
        myT = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        float scalar = (timer) / 5f;
        scalar = Mathf.Clamp(scalar, 0, 1);
        myT.color = Color.Lerp(new Color(.706f, .706f, .706f, 0), new Color(.706f, .706f, .706f, .404f), scalar);

    }
}
