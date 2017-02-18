using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetVisibilityCheck : MonoBehaviour {

    private GameObject myIcon;
    private bool _canSee;
    private bool canSee
    {
        get
        {
            return _canSee;
        }
        set
        {
            if(value != _canSee)
            {
                _canSee = value;
                if (_canSee)
                {
                    myIcon.SetActive(true);
                }
                else
                {
                    myIcon.SetActive(false);
                }
            }
        }
    }

	void Start () {
        myIcon = transform.GetChild(0).gameObject;
	}
	
	void Update () {
        if (Mathf.Abs(Camera.main.transform.position.x - transform.position.x) < 26
            && Camera.main.transform.position.y < transform.position.y)
        {
            canSee = true;
            myIcon.transform.position = (Camera.main.transform.position + Vector3.Normalize(transform.position - Camera.main.transform.position) * 20);
            myIcon.transform.LookAt(transform.position);
        }
        else
        {
            canSee = false;
        }
	}
}
