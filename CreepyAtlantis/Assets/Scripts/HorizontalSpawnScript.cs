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
            GameObject thisRock = (GameObject) Instantiate(ground1, new Vector3(i * 5, Random.Range (-2, 2), 0), Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)), transform);
            thisRock.transform.localScale = Vector3.one * 150;
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
