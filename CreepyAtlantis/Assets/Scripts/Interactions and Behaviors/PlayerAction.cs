using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {

    public float interactionRange;
    public LayerMask myLM;
    public void TryToInteract(int playerNum, bool pushRelease)
    {
        //Debug.Log("Trying to interact with stuff in range");
        if (pushRelease)
        {
            //RaycastHit[] allInRange = Physics.SphereCastAll(transform.position + Vector3.down * .25f - transform.forward * .5f, interactionRange, Vector3.up, 1.5f);

            //From player's feet, down .25, and in front by .5
            //At a radius of interactionRange
            //To upward direction for 1.5
            RaycastHit[] allInRange = Physics.SphereCastAll(transform.position + Vector3.down *.25f - transform.forward * .5f, interactionRange, Vector3.up, 2f, myLM, QueryTriggerInteraction.Collide);
            Debug.DrawLine(transform.position + Vector3.down * .25f - transform.forward * .5f, transform.position + Vector3.up * 1.75f - transform.forward * .5f);

            //Debug
            //foreach (RaycastHit aHit in allInRange)
            //{
            //    Debug.Log(aHit.collider.gameObject.name);
            //}


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
            
            //next is everything else on each collider
            foreach (RaycastHit aHit in allInRange)
            {
                if (aHit.collider.gameObject.GetComponent<IInteractable>() != null)
                {
                    aHit.collider.GetComponent<IInteractable>().Interact(playerNum, pushRelease);
                    return;
                }
            }

            //last is something that might be on the parent
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
