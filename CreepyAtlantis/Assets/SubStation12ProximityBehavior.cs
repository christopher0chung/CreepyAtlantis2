using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubStation12ProximityBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Instantiate(Resources.Load("OLI"), transform.position + Vector3.up * 15, Quaternion.identity, transform);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.name =="Sub")
        {
            GetComponent<SubStation12ProximityObjective>().Trigger();
            GameObject.Find("SubStation12Point1").GetComponent<SubStationInteraction>().SetInteractionActive(true);
        }
    }
}
