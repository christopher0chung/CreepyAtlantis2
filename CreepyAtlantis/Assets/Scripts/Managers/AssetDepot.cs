using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class AssetDepot : MonoBehaviour { }

//    private Dictionary<DepotObjects, Queue<GameObject>> depot = new Dictionary<DepotObjects, Queue<GameObject>>();
//    private Dictionary<DepotObjects, string> prefabDict = new Dictionary<DepotObjects, string>(); 

//    private Queue<GameObject> basicLightDepot = new Queue<GameObject>();
//    private Queue<GameObject> drillDepot = new Queue<GameObject>();
//    private Queue<GameObject> bullKelpDepot = new Queue<GameObject>();
//    private Queue<GameObject> jellyFishDepot = new Queue<GameObject>();

//    private Queue<GameObject> stalkType1 = new Queue<GameObject>();
//    private Queue<GameObject> stalkType2 = new Queue<GameObject>();
//    private Queue<GameObject> headType1 = new Queue<GameObject>();
//    private Queue<GameObject> headType2 = new Queue<GameObject>();

//    private Queue<GameObject> interactIcon = new Queue<GameObject>();

//    void Awake()
//    {
//        DontDestroyOnLoad(this.gameObject);
//        SceneManager.sceneLoaded += ClearOnNewLevel;
//    }

//    void Start()
//    {
//        depot.Add(DepotObjects.light, basicLightDepot);
//        depot.Add(DepotObjects.drill, drillDepot);
//        depot.Add(DepotObjects.bullKelp, bullKelpDepot);
//        depot.Add(DepotObjects.jellyFish, jellyFishDepot);
//        depot.Add(DepotObjects.stalkType1, stalkType1);
//        depot.Add(DepotObjects.stalkType2, stalkType2);
//        depot.Add(DepotObjects.headType1, headType1);
//        depot.Add(DepotObjects.headType2, headType2);
//        depot.Add(DepotObjects.interactIcon, interactIcon);

//        prefabDict.Add(DepotObjects.light, "basicLightPrefab");
//        prefabDict.Add(DepotObjects.drill, "drillPrefab");
//        prefabDict.Add(DepotObjects.bullKelp, "bullKelpPrefab2");
//        prefabDict.Add(DepotObjects.jellyFish, "jellyFishPrefab");
//        prefabDict.Add(DepotObjects.stalkType1, "stalkType1");
//        prefabDict.Add(DepotObjects.stalkType2, "stalkType2");
//        prefabDict.Add(DepotObjects.headType1, "headType1");
//        prefabDict.Add(DepotObjects.headType2, "headType2");
//        prefabDict.Add(DepotObjects.interactIcon, "interactIcon");
//    }

//    public GameObject DepotRequest (DepotObjects desiredObject)
//    {
//        if (depot[desiredObject].Count > 0)
//        {
//            GameObject theRequestedObject = (depot[desiredObject].Dequeue());
//            theRequestedObject.SetActive(true);
//            return theRequestedObject;
//        }
//        else
//        {
//            return (GameObject)Instantiate(Resources.Load(prefabDict[desiredObject]), Vector3.one * 1000, Quaternion.identity);
//        }
//    }

//    public void DepotDeposit(DepotObjects desiredObject, GameObject myGO)
//    {
//        myGO.SetActive(false);
//        depot[desiredObject].Enqueue(myGO);
//    }

//    private void ClearOnNewLevel (Scene scene, LoadSceneMode mode)
//    {
//        //if (mode != LoadSceneMode.Additive)
//        //{
//        //    basicLightDepot.Clear();
//        //    drillDepot.Clear();
//        //    bullKelpDepot.Clear();
//        //    jellyFishDepot.Clear();
//        //}
//    }
//}

//public enum DepotObjects { light, drill, bullKelp, jellyFish, stalkType1, stalkType2, headType1, headType2, interactIcon }
