using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawn : MonoBehaviour {

    public int number;

    private float invRange;
    private float range;

    [SerializeField] GameObject fish;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < number; i++)
        {
            invRange = Random.Range(0.001f, 1.000f);
            range = (1 - (invRange * invRange * invRange)) * 20;

            Vector3 position = Vector3.right * - 60 + Vector3.forward * 60 + (Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))) * Vector3.forward) * range;

            Instantiate(fish, position, Quaternion.identity, transform);
        }
	}
	
}
