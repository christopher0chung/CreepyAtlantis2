using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelpInProps : SSInteractableObject {

    Transform sub;
    public Vector3 offset;

    private int interactCounter;

    void Awake()
    {
        Init(TriggerShape.sphere, Vector3.up, 1.5f);
        InitSphere(2, transform.position);
        sub = GameObject.Find("Sub").transform;
        Instantiate(Resources.Load("OLI"), transform.position, Quaternion.identity, transform);
    }

    public override void OnPress(int pNum)
    {
        if (pNum == 0)
        {
            if (interactCounter <= 5)
            {
                interactCounter++;
                GetComponentInChildren<ParticleSystem>().Play();
            }
            else
            {
                HideInteractable();
                Destroy(this.gameObject);
                GetComponent<Objective>().Trigger();
            }
        }
    }

    void Update()
    {
        _detectFunc();
        CleanUp();
        transform.position = sub.position + offset;
        //Debug.Log(sub.position);
    }

    public override void SphereCastDetect()
    {
        base.SphereCastDetect();
    }

    public override void Interact(int pNum, bool pressRelease)
    {
        //Instantiate(Resources.Load("KickSand"), transform.position + transform.up, Quaternion.identity);
        base.Interact(pNum, pressRelease);
        Debug.Log("interact");
    }
}
