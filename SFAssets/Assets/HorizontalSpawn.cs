using UnityEngine;
using System.Collections;

public class HorizontalSpawn : MonoBehaviour {

    public GameObject whatToSpawn;
    public int fromWhere;
    public int toWhere;

	// Use this for initialization
	void Start () {

        for (int i = fromWhere; i < toWhere; i++)
        {
            Instantiate(whatToSpawn, new Vector3(i, 0, 0), Quaternion.identity, transform);
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
