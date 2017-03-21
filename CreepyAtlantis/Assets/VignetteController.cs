using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VignetteController : MonoBehaviour {
    [ExecuteInEditMode]

    [SerializeField] private SpriteRenderer MaskFull;
    [SerializeField] private SpriteRenderer MaskVign;

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
                MaskFull.color = new Color(1, 1, 1, 1 - _vignSlider);
                MaskVign.color = new Color(1, 1, 1, 1 - (_vignSlider * _vignSlider));
                MaskVign.transform.localScale = Vector3.Lerp(new Vector3(.3f, .3f, 1), new Vector3(2, 1, 1), _vignSlider * _vignSlider);
            }
        }
    }

    public float slider;

    void Update()
    {
        vignSlider = slider;
    }
}
