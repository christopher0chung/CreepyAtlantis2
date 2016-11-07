using UnityEngine;
using System.Collections;

public class KelpGenerator : MonoBehaviour {

    public int numOfKelp;
    public float xRange;
    public float zRange;
    public float yDipRange;

    public GameObject kelp;

	// Use this for initialization
	void Start () {

        for (int i = 0; i < numOfKelp; i++)
        {
            Instantiate(kelp, transform.position + new Vector3(Random.Range(-xRange / 2, xRange / 2), Random.Range(-yDipRange, 0), Random.Range(0, zRange)), Quaternion.Euler(0, 180, 90), transform);
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
