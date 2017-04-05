using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelpHeadNode : MonoBehaviour {

    //private AssetDepot myAD;

	void Start () {
        //myAD = GameObject.Find("AssetDepot").GetComponent<AssetDepot>();
        int headType = Random.Range(0, 2);
        GameObject head;
        if (headType == 0)
        {
            //head = myAD.DepotRequest(DepotObjects.headType1);
            head = (GameObject)Instantiate(Resources.Load("headType1"), transform.position, Quaternion.Euler(-90, -90, 0), transform);
        }
        else
        {
            head = (GameObject)Instantiate(Resources.Load("headType1"), transform.position, Quaternion.Euler(-90, -90, 0), transform);
            //head = myAD.DepotRequest(DepotObjects.headType2);
        }

        //head.transform.position = transform.position;
        //head.transform.eulerAngles = new Vector3(-90, -90, 0);
        head.transform.localScale = Vector3.one * Random.Range(30f, 60f);
        //head.transform.parent = transform;
    }
	

}
