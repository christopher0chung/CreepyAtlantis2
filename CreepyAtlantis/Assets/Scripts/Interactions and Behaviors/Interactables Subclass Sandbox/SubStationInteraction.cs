using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubStationInteraction : SSInteractableObject {

    private DialogueManager myDM;
    public SSInteractableObject nextInteractable;
    public DialogueEvents lineToStart;

    //0 - player1
    //1 - player2
    //2 - either
    public int whoCanInteract;

    void Awake()
    {
        Init(TriggerShape.sphere, Vector3.up, 1f);
        InitSphere(2, transform.position);
    }

    void Start()
    {
        if (GameObject.Find("DialogueManager"))
            myDM = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    public override void OnPress(int pNum)
    {
        base.OnPress(pNum);
        Debug.Log("On Press registered");
        if(pNum == whoCanInteract)
        {
            // set dialogue line to true
            if (lineToStart != null)
                myDM.FireEvent(myDM.ReturnEventIndex(lineToStart));

            // make next interactable interactable
            if (nextInteractable != null)
                nextInteractable.SetInteractionActive(true);
            GetComponent<SubStationInteraction>().SetInteractionActive(false);

            HideInteractable();
            SetInteractionActive(false);
            GetComponent<SubStationObjective>().Trigger();
        }
    }

    void Update()
    {
        if (_detectFuncActive)
            _detectFunc();
        CleanUp();
    }

    public override void SetInteractionActive(bool active)
    {
        Instantiate(Resources.Load("OLI"), transform.position, Quaternion.identity, transform);
        base.SetInteractionActive(active);
    }

    public override void SphereCastDetect()
    {
        base.SphereCastDetect();
    }

    public override void Interact(int pNum, bool pressRelease)
    {
        base.Interact(pNum, pressRelease);
        Debug.Log("interact");
    }
}
