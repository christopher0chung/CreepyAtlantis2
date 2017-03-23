using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VignetteController : MonoBehaviour {
    [ExecuteInEditMode]

    [SerializeField] private SpriteRenderer MaskFull;
    [SerializeField] private SpriteRenderer MaskVign;
    [SerializeField] private bool shouldScale;

    private float _vignSlider;
    private float vignSlider
    {
        get
        {
            return _vignSlider;
        }
        set
        {
            if (value != _vignSlider)
            {
                _vignSlider = value;
                MaskFull.color = new Color(MaskFull.color.r, MaskFull.color.g, MaskFull.color.b, 1 - _vignSlider);
                MaskVign.color = new Color(MaskVign.color.r, MaskVign.color.g, MaskVign.color.b, 1 - (_vignSlider * _vignSlider));
                if (shouldScale)
                    MaskVign.transform.localScale = Vector3.Lerp(new Vector3(.3f, .3f, 1), new Vector3(2, 1, 1), _vignSlider * _vignSlider);
                else
                    MaskVign.transform.localScale = new Vector3(2, 1, 1);
            }
        }
    }

    public float slider;

    void Update()
    {
        vignSlider = slider;
    }
}
