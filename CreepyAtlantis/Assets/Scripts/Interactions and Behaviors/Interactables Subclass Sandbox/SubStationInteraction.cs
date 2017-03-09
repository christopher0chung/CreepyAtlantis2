using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubStationInteraction : SSInteractableObject {

    private DialogueManager myDM;
    public SSInteractableObject nextInteractable;
    public DialogueEvents lineToStart;

    void Awake()
    {
        Init(TriggerShape.sphere, Vector3.up);
        InitSphere(2, transform.position);
    }

    void Start()
    {
        myDM = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    public override void OnPress(int pNum)
    {
        HideInteractable();
        this.enabled = false;
    }

    void Update()
    {
        if (_detectFuncActive)
            _detectFunc();
    }

    public override void SphereCastDetect()
    {
        base.SphereCastDetect();
    }

    public override void Interact(int pNum, bool pressRelease)
    {
        base.Interact(pNum, pressRelease);

        // set dialogue line to true
        if (lineToStart != null)
            myDM.FireEvent(myDM.ReturnEventIndex(lineToStart));

        // make next interactable interactable
        if (nextInteractable != null)
            nextInteractable.SetInteractionActive(true);
        GetComponent<SubStationInteraction>().SetInteractionActive(false);

        Debug.Log("interact");
    }
}
