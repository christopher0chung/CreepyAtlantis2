using UnityEngine;
using System.Collections;

public class WaterLayerBehavior : MonoBehaviour {

    private float timer;
    public float yTimeGain;
    public float yScale;
    public float xTimeGain;
    public float xScale;
    public float angTimeGain;
    public float angScale;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        if (timer >= Mathf.PI * 2)
        {
            timer -= Mathf.PI * 2;
        }
        transform.localPosition = new Vector3(xScale * Mathf.Sin(xTimeGain * timer), yScale * Mathf.Cos(yTimeGain * timer), transform.localPosition.z);
	
	}
}
