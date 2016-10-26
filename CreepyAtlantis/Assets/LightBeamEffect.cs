using UnityEngine;
using System.Collections;

public class LightBeamEffect : MonoBehaviour {

    private SpriteRenderer mySR;
    private float timer;
    private float periodScalar;

	// Use this for initialization
	void Start () {
        mySR = GetComponent<SpriteRenderer>();
        timer = Random.Range(-2000, -1000);
        periodScalar = Random.Range(.2f, .6f);
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        mySR.color = new Color(mySR.color.r, mySR.color.g, mySR.color.b, Mathf.Sin(timer * periodScalar) * .025f);
	}
}
