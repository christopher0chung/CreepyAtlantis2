using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuctionZone : MonoBehaviour {

    [SerializeField] private PropellerSpeedScript myPSS;
    [SerializeField] private SuctionDir thisSD;
    [SerializeField] private float threshold;

    [SerializeField] private bool deadly;

    void Update()
    {
        if (thisSD == SuctionDir.Fwd && myPSS.rotSpeed >= threshold)
        {
            deadly = true;
        }
        else if (thisSD == SuctionDir.Aft && myPSS.rotSpeed <= -threshold)
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
            other.transform.root.GetComponent<Rigidbody>().AddForce((transform.parent.position - other.transform.position) * 60);
        }
    }

    private enum SuctionDir { Fwd, Aft }
}
