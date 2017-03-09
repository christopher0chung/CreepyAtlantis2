using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KelpDepotSpawn : MonoBehaviour, ISpawnableUsingDepot {

    private List<GameObject> mySpawns = new List<GameObject>();

    public AssetDepot myDepot;

    public DepotObjects myDepotObject;

    public float howMany;
    public float xMinRelative;
    public float xMaxRelative;
    public float zMinRelative;
    public float zMaxRelative;

    void Awake()
    {
        myDepot = GameObject.Find("AssetDepot").GetComponent<AssetDepot>();
    }

	void Start ()
    {
	    for (int i = 0; i < howMany; i++)
        {
            GameObject mySpawn = GetFromDepot(myDepotObject);

            mySpawn.transform.parent = transform;
            mySpawn.transform.localPosition = new Vector3(Random.Range(xMinRelative, xMaxRelative), Random.Range(-3.0f, 0.0f), Random.Range(zMinRelative, zMaxRelative));
            mySpawn.transform.rotation = Quaternion.Euler(0, 0, 0);
            mySpawn.transform.localScale = Vector3.one * (Random.Range(.75f, 1.5f));
            mySpawns.Add(mySpawn);
        }
	}

    //void Update ()
    //{
    //    if (Input.GetKeyDown(KeyCode.Y))
    //    {
    //        foreach (GameObject mySpawn in mySpawns)
    //        {
    //            ReturnToDepot(myDepotObject, mySpawn);
    //        }
    //        mySpawns.Clear();
    //    }
    //    if (Input.GetKeyDown(KeyCode.U))
    //    {
    //        for (int i = 0; i < 100; i++)
    //        {
    //            GameObject mySpawn = GetFromDepot(myDepotObject);

    //            mySpawn.transform.localPosition = new Vector3(Random.Range(xMinRelative, xMaxRelative), 0, Random.Range(zMinRelative, zMaxRelative));
    //            //mySpawn.transform.rotation = Quaternion.Euler(0, -180, 90);
    //            mySpawn.transform.rotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);
    //            int scalar = Random.Range(0, 5);
    //            transform.localScale = Vector3.one * (1 + scalar / 10);
    //            mySpawn.transform.parent = transform;

    //            mySpawns.Add(mySpawn);
    //        }
    //    }
    //}
	
    public GameObject GetFromDepot(DepotObjects theDepotObject)
    {
        return myDepot.DepotRequest(theDepotObject);
    }

    public void ReturnToDepot(DepotObjects theDepotObject, GameObject whatIWantSentToDepot)
    {
        myDepot.DepotDeposit(theDepotObject, whatIWantSentToDepot);
    }
}
