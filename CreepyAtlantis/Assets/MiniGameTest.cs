using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTest : MonoBehaviour {

    [HideInInspector] public Vector3 ctrlInput;
    private Vector3 gameInput;

    private float timer;
    [SerializeField] private float speed;
    [SerializeField] private float magnitude;
    private float timeInterval;

	// Use this for initialization
	void Start () {
        timeInterval = 1;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= timeInterval)
        {
            timer -= timeInterval;
            timeInterval = Random.Range(.5f, 1f);
            gameInput = new Vector3(Random.Range(-1.0f, 1.0f) * speed, Random.Range(-1.0f, 1.0f) * speed, 0);
        }

        Vector3 hold = transform.position + (ctrlInput + gameInput) * Time.deltaTime;
        if (Vector3.Distance(hold, Vector3.zero) > magnitude)
        {
            hold = magnitude * Vector3.Normalize(hold);
        }
        transform.position = hold;
	}
}
