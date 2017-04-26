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

    private float letGoTimer;
    //private float iconScale = 1.3f;
    float iconScale = 0;
    private Vector3 iconOffset = new Vector3(-2.44f, -2.56f, 0);

    [HideInInspector]
    public GameObject myIcon;
    private bool _showInteractableIcon;
    [HideInInspector]
    public bool showInteractableIcon
    {
        get
        {
            return _showInteractableIcon;
        }
        set
        {
            if (value)
                letGoTimer = 0;

            if (value != _showInteractableIcon)
            {
                _showInteractableIcon = value;
                if (_showInteractableIcon)
                {
                    myIcon = (GameObject)Instantiate(Resources.Load("interactIcon"), transform.position + iconOffset, Quaternion.identity, transform);
                    myIcon.transform.localScale = Vector3.one * iconScale;

                }
                else
                {
                    Destroy(myIcon);
                }
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

        letGoTimer += Time.deltaTime;

        if (GameObject.FindGameObjectsWithTag("Character") != null)
        {
            GameObject[] theGOs = GameObject.FindGameObjectsWithTag("Character");
            foreach (GameObject thisChar in theGOs)
            {
                if (thisChar.activeSelf)
                {
                    ShowInteractable();
                    return;
                }
            }
        }
        HideInteractable();
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

    protected void ShowInteractable()
    {
        showInteractableIcon = true;
    }

    public void HideInteractable()
    {
        showInteractableIcon = false;
    }
}
