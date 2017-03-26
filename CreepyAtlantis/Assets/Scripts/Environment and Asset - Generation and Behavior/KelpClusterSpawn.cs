using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelpClusterSpawn : MonoBehaviour {

    private List<GameObject> mySpawns = new List<GameObject>();

    //private AssetDepot myDepot;

    public float howManyClusters;

    public float clusterDensityMedian;
    public float clusterDensityRange;
    public float xMin;
    public float xMax;
    public float zMin;
    public float zMax;

    private Vector3 node;

    private float angle;
    private float range;
    private float appliedRange;


    void Awake()
    {
        //myDepot = GameObject.Find("AssetDepot").GetComponent<AssetDepot>();
    }

    void Start()
    {
        for (int i = 0; i < howManyClusters; i++)
        {
            node = new Vector3(Random.Range(xMin, xMax), 0, Random.Range(zMin, zMax));
            angle = Random.Range(0, 360);
            range = appliedRange = 1f;

            for (int j = 0; j < Random.Range(clusterDensityMedian - clusterDensityRange/2, clusterDensityMedian + clusterDensityRange/2); j++)
            {
                GameObject mySpawn;
                int rand = Random.Range(0, 2);
                if (rand == 0)
                    mySpawn = (GameObject)Instantiate(Resources.Load("stalkType1"), node + new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad)) * appliedRange, Quaternion.identity, transform);
                else
                    mySpawn = (GameObject)Instantiate(Resources.Load("stalkType2"), node + new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad)) * appliedRange, Quaternion.identity, transform);

                //GameObject mySpawn = GetFromDepot(RandomDepotItem());

                //mySpawn.transform.parent = transform;
                //mySpawn.transform.localPosition = node + new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad)) * appliedRange;

                angle += 60 * 1.6180339887f;
                angle = (angle + 360) % 360;

                range += .65f;
                appliedRange = Random.Range(0, range);

                //mySpawn.transform.rotation = Quaternion.Euler(0, 0, 0);
                mySpawn.transform.localScale = Vector3.one * (Random.Range(.75f, 1.5f));
                mySpawns.Add(mySpawn);
            }
        }
    }

    //DepotObjects RandomDepotItem()
    //{
    //    int myInt =  Random.Range(0, 2);
    //    if (myInt == 0)
    //        return DepotObjects.stalkType1;
    //    else
    //        return DepotObjects.stalkType2;
    //}

    //public GameObject GetFromDepot(DepotObjects theDepotObject)
    //{
    //    return myDepot.DepotRequest(theDepotObject);
    //}

    //public void ReturnToDepot(DepotObjects theDepotObject, GameObject whatIWantSentToDepot)
    //{
    //    myDepot.DepotDeposit(theDepotObject, whatIWantSentToDepot);
    //}
}
