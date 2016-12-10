using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class AssetDepot : MonoBehaviour {

    private Dictionary<DepotObjects, Queue<GameObject>> depot = new Dictionary<DepotObjects, Queue<GameObject>>();
    private Dictionary<DepotObjects, string> prefabDict = new Dictionary<DepotObjects, string>(); 

    private Queue<GameObject> basicLightDepot = new Queue<GameObject>();
    private Queue<GameObject> drillDepot = new Queue<GameObject>();
    private Queue<GameObject> bullKelpDepot = new Queue<GameObject>();
    private Queue<GameObject> jellyFishDepot = new Queue<GameObject>();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += ClearOnNewLevel;
    }

    void Start()
    {
        depot.Add(DepotObjects.light, basicLightDepot);
        depot.Add(DepotObjects.drill, drillDepot);
        depot.Add(DepotObjects.bullKelp, bullKelpDepot);
        depot.Add(DepotObjects.jellyFish, jellyFishDepot);

        prefabDict.Add(DepotObjects.light, "basicLightPrefab");
        prefabDict.Add(DepotObjects.drill, "drillPrefab");
        prefabDict.Add(DepotObjects.bullKelp, "bullKelpPrefab2");
        prefabDict.Add(DepotObjects.jellyFish, "jellyFishPrefab");
    }

    public GameObject DepotRequest (DepotObjects desiredObject)
    {
        if (depot[desiredObject].Count > 0)
        {
            //Debug.Log(depot[desiredObject].Count);
            GameObject theRequestedObject = (depot[desiredObject].Dequeue());
            theRequestedObject.SetActive(true);
            return theRequestedObject;
        }
        else
        {
            //Debug.Log(depot[desiredObject].Count);
            //Debug.Log(prefabDict[desiredObject]);
            return (GameObject)Instantiate(Resources.Load(prefabDict[desiredObject]), Vector3.zero, Quaternion.identity);
        }
    }

    public void DepotDeposit(DepotObjects desiredObject, GameObject myGO)
    {
        myGO.SetActive(false);
        depot[desiredObject].Enqueue(myGO);
        //Debug.Log(depot[desiredObject].Count);
    }

    private void ClearOnNewLevel (Scene scene, LoadSceneMode mode)
    {
        //if (mode != LoadSceneMode.Additive)
        //{
        //    basicLightDepot.Clear();
        //    drillDepot.Clear();
        //    bullKelpDepot.Clear();
        //    jellyFishDepot.Clear();
        //}
    }
}

public enum DepotObjects { light, drill, bullKelp, jellyFish }
