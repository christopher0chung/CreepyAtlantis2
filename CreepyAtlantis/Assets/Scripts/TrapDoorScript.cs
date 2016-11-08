using UnityEngine;
using System.Collections;
using MultiplayerWithBindingsExample;

public class TrapDoorScript : MonoBehaviour {

    public Transform trapDoorPort;
    public Transform trapDoorStbd;

    public delegate void currentDoorOperation();
    currentDoorOperation CDO;

    public float doorOpeningRate;

    public MeshRenderer myLightMask;

    public GameObject myP1Controller;
    public GameObject myP2Controller;
    public GameObject myP1;
    public GameObject myP2;
    public GameObject myP1Mask;
    public GameObject myP2Mask;

	// Use this for initialization
	void Start () {
        CDO = Closing;
        myLightMask.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (CDO == Opening)
        //    {
        //        CDO = Closing;
        //        Debug.Log("Closing");
        //    }
        //    else
        //    {
        //        CDO = Opening;
        //        Debug.Log("Opening");
        //    }
        //}

        //Debug.Log(Vector3.Distance(transform.position, myP2.transform.position));

        if ((myP1.activeSelf && Vector3.Distance(transform.position, myP1.transform.position) < 6) || (myP2.activeSelf && Vector3.Distance(transform.position, myP2.transform.position) < 6))
            CDO = Opening;
        else
            CDO = Closing;

        if (!myP1.activeSelf)
            myP1.transform.position = transform.position + Vector3.down * 4 + Vector3.left * .5f;
        if (!myP2.activeSelf)
            myP2.transform.position = transform.position + Vector3.down * 4 + Vector3.left * 4;

        CDO();
	}

    public void OpenTheDoors (bool openClose)
    {
        if (openClose)
            CDO = Opening;
        else
            CDO = Closing;
    }

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

    public void ReleaseThePlayers()
    {
        if (!myP1.activeSelf)
        {
            myP1.SetActive(true);
            myP1Mask.SetActive(true);
            GameObject[] myP1s = GameObject.FindGameObjectsWithTag("Player1");
            foreach (GameObject myP1Controller in myP1s)
            {
                myP1Controller.GetComponent<ActionsOutputTarget>().SetMyIO(myP1.GetComponent<PlayerControlIO>());
            }
        }
        if (!myP2.activeSelf)
        {
            myP2.SetActive(true);
            myP2Mask.SetActive(true);
            GameObject[] myP2s = GameObject.FindGameObjectsWithTag("Player2");
            foreach (GameObject myP2Controller in myP2s)
            {
                myP2Controller.GetComponent<ActionsOutputTarget>().SetMyIO(myP2.GetComponent<PlayerControlIO>());
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "PlayerCharacter1")
        {
            myP1.GetComponent<PlayerAir>().Supply(100);
            myP1.SetActive(false);
            myP1Mask.SetActive(false);
            GameObject[] myP1s = GameObject.FindGameObjectsWithTag("Player1");
            foreach (GameObject myP1Controller in myP1s)
            {
                myP1Controller.GetComponent<ActionsOutputTarget>().SetMyIO(transform.root.GetComponent<SubControlIO>());
            }
        }
        else if (other.gameObject.name == "PlayerCharacter2")
        {
            myP2.GetComponent<PlayerAir>().Supply(100);
            myP2.SetActive(false);
            myP2Mask.SetActive(false);
            GameObject[] myP2s = GameObject.FindGameObjectsWithTag("Player2");
            foreach (GameObject myP2Controller in myP2s)
            {
                myP2Controller.GetComponent<ActionsOutputTarget>().SetMyIO(transform.root.GetComponent<SubControlIO>());
            }
        }
    }
}
