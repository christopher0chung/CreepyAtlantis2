using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {

    public LayerMask myLM;
    public float observeRange;
    public float actionRange;

    private Collider[] myColls;
    private List<GameObject> observables = new List<GameObject>();
    private Transform head;

    void Start()
    {
        head = transform.Find("PlayerHead");
    }

    void FixedUpdate ()
    {
        observables.Clear();
        myColls = Physics.OverlapSphere(transform.position, observeRange, myLM, QueryTriggerInteraction.Ignore);
        foreach (Collider observable in myColls)
        {
            if (CheckAngFromLight(observable.gameObject.transform.position, head))
            {
                observables.Add(observable.gameObject);
            }
        }
    }

    public bool CheckAngFromLight (Vector3 thingToBeChecked, Transform refPos)
    {
        float angBetween = Mathf.Atan2(refPos.position.y - thingToBeChecked.y, refPos.position.x - thingToBeChecked.x) * Mathf.Rad2Deg;
        if (Mathf.Abs(refPos.localRotation.z - angBetween) < 15)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Action (bool pushRelease)
    {
        float range = observeRange;
        GameObject closestObservable = null;
        foreach(GameObject observable in observables)
        {
            if (Vector3.Distance(observable.transform.position, head.transform.position) < range)
            {
                range = Vector3.Distance(observable.transform.position, head.transform.position);
                closestObservable = observable;
            }
        }

        if (closestObservable != null)
        {
            return;
        }
    }
}
