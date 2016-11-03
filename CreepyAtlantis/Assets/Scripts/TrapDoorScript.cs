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

    public GameObject myP1;
    public GameObject myP2;

	// Use this for initialization
	void Start () {
        CDO = Closing;
        myLightMask.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (CDO == Opening)
            {
                CDO = Closing;
                Debug.Log("Closing");
            }
            else
            {
                CDO = Opening;
                Debug.Log("Opening");
            }
        }

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

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "PlayerCharacter1")
        {
            other.gameObject.SetActive(false);
            GameObject[] myP1s = GameObject.FindGameObjectsWithTag("Player1");
            foreach (GameObject myP1 in myP1s)
            {
                myP2.GetComponent<ActionsOutputTarget>().SetMyIO(transform.root.GetComponent<SubControlIO>());

            }
        }
        else if (other.gameObject.name == "PlayerCharacter2")
        {
            other.gameObject.SetActive(false);
            GameObject[] myP2s = GameObject.FindGameObjectsWithTag("Player2");
            foreach (GameObject myP2 in myP2s)
            {
                myP2.GetComponent<ActionsOutputTarget>().SetMyIO(transform.root.GetComponent<SubControlIO>());
                Debug.Log(myP2.GetComponent<ActionsOutputTarget>().myIO);
            }
        }
    }
}
