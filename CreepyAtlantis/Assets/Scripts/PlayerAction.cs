using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {

    public float interactionRange;

    public void TryToInteract(int playerNum, bool pushRelease)
    {
        //Debug.Log("Trying to interact with stuff in range");
        if (pushRelease)
        {
            
            RaycastHit[] allInRange = Physics.SphereCastAll(transform.position + Vector3.down * .25f - transform.forward * .5f, interactionRange, Vector3.up, 1.5f);
            Debug.DrawLine(transform.position + Vector3.down * .25f - transform.forward * .5f, transform.position + Vector3.up * 1.75f - transform.forward * .5f);

            //Debug
            foreach (RaycastHit aHit in allInRange)
            {
                Debug.Log(aHit.collider.gameObject.name);
            }


            //highest priority is climbing into sub
            foreach (RaycastHit aHit in allInRange)
            {
                if (aHit.collider.gameObject.tag == "Sub")
                {
                    aHit.transform.GetComponentInChildren<IInteractable>().Interact(playerNum, pushRelease);
                    return;
                }
            }
            //next highest priority is topping off partner's air (checks to make sure that you're not triggering yourself
            foreach (RaycastHit aHit in allInRange)
            {
                if (aHit.collider.gameObject.tag == "Character" && aHit.collider.gameObject != gameObject)
                {
                    aHit.transform.GetComponentInChildren<IInteractable>().Interact(playerNum, pushRelease);
                    return;
                }
            }
            //next is everything else
            foreach (RaycastHit aHit in allInRange)
            {
                if (aHit.collider.transform.root.gameObject.GetComponent<IInteractable>() != null)
                {
                    aHit.collider.transform.root.gameObject.GetComponent<IInteractable>().Interact(playerNum, pushRelease);
                    return;
                }
            }
        }
    }
}
