using UnityEngine;
using System.Collections;

public class OpenDoorTrigger : MonoBehaviour {

    void OnTriggerStay (Collider other)
    {
        if (other.gameObject.name == "Character0" || other.gameObject.name == "Character1")
        {
            transform.parent.GetComponent<TrapDoorScript>().closingTimer = .25f;
        }
    }
}
