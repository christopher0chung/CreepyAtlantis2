using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelpAreaTrigger : MonoBehaviour {

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Sub")
        {
            other.gameObject.GetComponent<SubInKelp>().InKelp();
        }
    }
}
