using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoTextFade : MonoBehaviour {

    private TextMesh myTM;
    private float opacity;
    private float timer;

	// Use this for initialization
	void Start () {
        myTM = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        opacity = 6 - timer;

        myTM.color = new Color(1, 1, 1, opacity);

        if (opacity <= 0)
            Destroy(this.gameObject);
	}
}
