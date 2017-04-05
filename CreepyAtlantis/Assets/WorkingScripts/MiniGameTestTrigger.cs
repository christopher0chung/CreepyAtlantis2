using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameTestTrigger : MonoBehaviour {

    private GameObject target;
    private GameObject reticle;

    [SerializeField] private float completion;
    [SerializeField] private float dist;

    void Start () {
        target = GameObject.Find("Target");
        reticle = GameObject.Find("Reticle");
    }
	
	void Update () {
        if (Vector3.Distance(target.transform.position, reticle.transform.position) < dist)
        {
            completion += (dist - Vector3.Distance(target.transform.position, reticle.transform.position)) * .25f * Time.deltaTime;
            target.GetComponent<Image>().fillAmount = completion;
        }

        if (completion >= 1)
        {
            target.GetComponent<Image>().color = Color.red;
        }
	}
}
