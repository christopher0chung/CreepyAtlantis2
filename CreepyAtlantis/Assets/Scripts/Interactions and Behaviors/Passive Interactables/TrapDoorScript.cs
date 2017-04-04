using UnityEngine;
using System.Collections;
using MultiplayerWithBindingsExample;
using System.Collections.Generic;

public class TrapDoorScript : MonoBehaviour, IInteractable {

    public Transform trapDoorPort;
    public Transform trapDoorStbd;

    public delegate void currentDoorOperation();
    currentDoorOperation CDO;

    public float doorOpeningRate;

    public float closingTimer;

    private Dictionary<string, int> nameToNum = new Dictionary<string, int>();

    public GameObject myLight;
    private bool lightOnOff = true;
    private bool lightSwitch
    {
        get
        {
            return lightOnOff;
        }
        set
        {
            if (value != lightOnOff)
            {
                if (value)
                {
                    myLight.SetActive(true);
                }
                else
                {
                    myLight.SetActive(false);
                }
                lightOnOff = value;
            }
        }
    }


	// Use this for initialization
	void Start () {
        CDO = Closing;

        nameToNum.Add("Character0", 0);
        nameToNum.Add("Character1", 1);
        myLight = transform.Find("BellyGlow").gameObject;
        lightSwitch = false;
	}
	
	// Update is called once per frame
	void Update () {

        closingTimer -= Time.deltaTime;

        if (closingTimer <= 0)
        {
            CDO = Closing;
        }
        else
        {
            CDO = Opening;
        }

        CDO();
	}


    private void Opening()
    {
        trapDoorPort.localPosition = Vector3.MoveTowards(trapDoorPort.localPosition, new Vector3(-2.331184f, -1.295489f, 1.325f), doorOpeningRate / 100f);
        trapDoorStbd.localPosition = Vector3.MoveTowards(trapDoorStbd.localPosition, new Vector3(-2.331184f, -1.295489f, -1.325f), doorOpeningRate / 100f);

        trapDoorPort.localRotation = Quaternion.RotateTowards(trapDoorPort.localRotation, Quaternion.Euler(-125, 0, 90), doorOpeningRate);
        trapDoorStbd.localRotation = Quaternion.RotateTowards(trapDoorStbd.localRotation, Quaternion.Euler(-125, 0, 90), doorOpeningRate);
        lightSwitch = true;
    }

    private void Closing()
    {
        trapDoorPort.localPosition = Vector3.MoveTowards(trapDoorPort.localPosition, new Vector3(-2.331184f, -1.295489f, 0.3802599f), doorOpeningRate / 100f);
        trapDoorStbd.localPosition = Vector3.MoveTowards(trapDoorStbd.localPosition, new Vector3(-2.331184f, -1.295489f, -0.3802599f), doorOpeningRate / 100f);

        trapDoorPort.localRotation = Quaternion.RotateTowards(trapDoorPort.localRotation, Quaternion.Euler(-90, 0, 90), doorOpeningRate);
        trapDoorStbd.localRotation = Quaternion.RotateTowards(trapDoorStbd.localRotation, Quaternion.Euler(-90, 0, 90), doorOpeningRate);
        if (trapDoorPort.localRotation.eulerAngles.z < 1)
            lightSwitch = false;
    }

    public void Interact (int playerNum, bool pushRelease)
    {
        GameObject.Find("GameStateManager").GetComponent<GameStateManager>().SetControls(playerNum, Controllables.submarine);
        GameObject.Find("GameStateManager").GetComponent<GameStateManager>().SubInteract(playerNum, true);
    }
}
