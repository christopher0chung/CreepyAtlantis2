using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityAnimation : MonoBehaviour {

    private Material myMat;
    private float opacity;
    private float size;

    private float timer;
	// Use this for initialization
	void Start () {
        myMat = GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 2)
            timer -= 2;

        OpacityCalc();
        SizeCalc();

        myMat.SetFloat("_Opacity", opacity);
        transform.localScale = Vector3.one * size;
	}

    private void OpacityCalc()
    {
        opacity = .1f * (2 - timer);
    }
    private void SizeCalc()
    {
        size = 6 - 2 * timer;
    }
}
