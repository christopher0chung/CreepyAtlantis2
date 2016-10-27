using UnityEngine;
using System.Collections;

public class HorizontalSpawnScript : MonoBehaviour {

    public GameObject ground1;

    public int leftMax;
    public int RightMax;

	// Use this for initialization
	void Start () {

        for (int i = leftMax; i < RightMax; i++)
        {
            Instantiate(ground1, new Vector3(i, 0, 0), Quaternion.identity, transform);
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
