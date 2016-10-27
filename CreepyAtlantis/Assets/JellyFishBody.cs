using UnityEngine;
using System.Collections;

public class JellyFishBody : MonoBehaviour {

    private SkinnedMeshRenderer mySMR;
    public float percent;

    private float timer;

	// Use this for initialization
	void Start () {
        mySMR = GetComponent<SkinnedMeshRenderer>();
        timer = Random.Range(-2000, -1800);
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        percent = ((Mathf.Sin(timer * 2) / 2) + .5f) * 100;

        mySMR.SetBlendShapeWeight(0, percent);
	}
}
