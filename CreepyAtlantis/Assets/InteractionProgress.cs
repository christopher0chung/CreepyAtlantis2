using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionProgress : MonoBehaviour {

    [SerializeField]
    private Image progressBar;

    public void SetProgress(float perc)
    {
        progressBar.fillAmount = perc;
    }
}
