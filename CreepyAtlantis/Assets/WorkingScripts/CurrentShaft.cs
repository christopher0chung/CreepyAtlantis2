using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentShaft : MonoBehaviour {

    public float currentMag;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<ICurrent>() != null)
        {
            other.gameObject.GetComponent<ICurrent>().PushWithCurrent(transform.right * currentMag);
        }
    }
}
