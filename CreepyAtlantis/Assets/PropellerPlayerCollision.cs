using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerPlayerCollision : MonoBehaviour {

    [SerializeField] private PropellerSpeedScript myPSS;
    [SerializeField] private bool deadly;

    [SerializeField] private float threshold;

    void Update()
    {
        if (Mathf.Abs(myPSS.rotSpeed) >= threshold)
        {
            deadly = true;
        }
        else
        {
            deadly = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.root.gameObject.tag == "Character" && deadly)
        {
            other.transform.root.GetComponent<CollisionDeath>().StartDeathSeq();
        }
    }
}
