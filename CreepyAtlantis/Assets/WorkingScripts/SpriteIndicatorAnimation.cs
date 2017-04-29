using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteIndicatorAnimation : MonoBehaviour {

    private Image myImg;
    private float opacity;
    private float size;

    private float timer;
    // Use this for initialization
    void Start()
    {
        myImg = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= .6f)
            timer -= .6f;

        OpacityCalc();
        SizeCalc();

        myImg.color = new Color(myImg.color.r, myImg.color.g, myImg.color.b, opacity);
        transform.localScale = Vector3.one * size;
    }

    private void OpacityCalc()
    {
        opacity = (2 - timer) / 1.4f;
    }
    private void SizeCalc()
    {
        size = 1- .75f * timer;
    }
}
