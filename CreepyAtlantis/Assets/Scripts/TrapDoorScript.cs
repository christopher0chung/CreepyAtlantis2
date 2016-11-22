using UnityEngine;
using System.Collections;
using MultiplayerWithBindingsExample;
using System.Collections.Generic;

public class TrapDoorScript : MonoBehaviour {

    public Transform trapDoorPort;
    public Transform trapDoorStbd;

    public delegate void currentDoorOperation();
    currentDoorOperation CDO;

    public float doorOpeningRate;

    public MeshRenderer myLightMask;

    public float closingTimer;

    private Dictionary<string, int> nameToNum = new Dictionary<string, int>();

	// Use this for initialization
	void Start () {
        CDO = Closing;
        myLightMask = GameObject.Find("TestCam").transform.Find("Multiple Masks Sample").transform.Find("Masks").transform.Find("SubBellyMask").GetComponent<MeshRenderer>();
        myLightMask.enabled = false;

        nameToNum.Add("Character0", 0);
        nameToNum.Add("Character1", 0);
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

    //public void OpenTheDoors (bool openClose)
    //{
    //    if (openClose)
    //        CDO = Opening;
    //    else
    //        CDO = Closing;
    //}

    private void Opening()
    {
        trapDoorPort.rotation = Quaternion.RotateTowards(trapDoorPort.rotation, Quaternion.Euler(0, 90, -90), doorOpeningRate);
        trapDoorStbd.rotation = Quaternion.RotateTowards(trapDoorStbd.rotation, Quaternion.Euler(0, 90, 90), doorOpeningRate);

        if (trapDoorStbd.rotation.eulerAngles.z >= 9)
        {
            myLightMask.enabled = true;
            //Debug.Log("my belly glow");

        }

    }

    private void Closing()
    {
        trapDoorPort.rotation = Quaternion.RotateTowards(trapDoorPort.rotation, Quaternion.Euler(0, 90, 0), doorOpeningRate);
        trapDoorStbd.rotation = Quaternion.RotateTowards(trapDoorStbd.rotation, Quaternion.Euler(0, 90, 0), doorOpeningRate);

        if (trapDoorStbd.rotation.eulerAngles.z < 9)
        {
            myLightMask.enabled = false;
            //Debug.Log("my belly off");

        }

    }

    void OnTriggerStay2D (Collider2D other)
    {
        int result;
        if (!nameToNum.TryGetValue(other.gameObject.name, out result))
            return;
        else
        {
            GameObject.Find("GameStateManager").GetComponent<GameStateManager>().SetControls(result, Controllables.submarine);
            GameObject.Find("GameStateManager").GetComponent<GameStateManager>().SubInteract(result, true);
        }
    }

    //public void ReleaseThePlayers()
    //{
    //    if (!myP1.activeSelf)
    //    {
    //        myP1.SetActive(true);
    //        myP1Mask.SetActive(true);
    //        GameObject[] myP1s = GameObject.FindGameObjectsWithTag("Player1");
    //        foreach (GameObject myP1Controller in myP1s)
    //        {
    //            myP1Controller.GetComponent<ActionsOutputTarget>().SetMyIO(myP1.GetComponent<PlayerControlIO>());
    //        }
    //    }
    //    if (!myP2.activeSelf)
    //    {
    //        myP2.SetActive(true);
    //        myP2Mask.SetActive(true);
    //        GameObject[] myP2s = GameObject.FindGameObjectsWithTag("Player2");
    //        foreach (GameObject myP2Controller in myP2s)
    //        {
    //            myP2Controller.GetComponent<ActionsOutputTarget>().SetMyIO(myP2.GetComponent<PlayerControlIO>());
    //        }
    //    }
    //}

    //void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.gameObject.name == "PlayerCharacter1")
    //    {
    //        myP1.GetComponent<PlayerAir>().Supply(100);
    //        myP1.SetActive(false);
    //        myP1Mask.SetActive(false);
    //        GameObject[] myP1s = GameObject.FindGameObjectsWithTag("Player1");
    //        foreach (GameObject myP1Controller in myP1s)
    //        {
    //            myP1Controller.GetComponent<ActionsOutputTarget>().SetMyIO(transform.root.GetComponent<SubControlIO>());
    //        }
    //    }
    //    else if (other.gameObject.name == "PlayerCharacter2")
    //    {
    //        myP2.GetComponent<PlayerAir>().Supply(100);
    //        myP2.SetActive(false);
    //        myP2Mask.SetActive(false);
    //        GameObject[] myP2s = GameObject.FindGameObjectsWithTag("Player2");
    //        foreach (GameObject myP2Controller in myP2s)
    //        {
    //            myP2Controller.GetComponent<ActionsOutputTarget>().SetMyIO(transform.root.GetComponent<SubControlIO>());
    //        }
    //    }
    //}
}
