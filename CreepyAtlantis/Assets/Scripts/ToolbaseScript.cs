using UnityEngine;
using System.Collections;

public class ToolbaseScript : MonoBehaviour {

    public KeyCode drillOn;
    public MonoBehaviour placeHolder;
    private ToolBitInterface myCurrentToolScript;

    private AudioSource toolOn;

    public float pitchOperating;
    public float volumeOperating;

    private float pitchApplied;
    private float volumeApplied;

	// Use this for initialization
	void Start () {
        myCurrentToolScript = GetComponentInChildren<ToolBitInterface>();
        toolOn = GetComponent<AudioSource>();
        toolOn.volume = 0;
        toolOn.pitch = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey(drillOn))
        {
            myCurrentToolScript.OnState();
            volumeApplied = volumeOperating;
            pitchApplied = pitchOperating;
        }

        else
        {
            myCurrentToolScript.OffState();
            volumeApplied = 0;
            pitchApplied = 0;
        }

    }

    void Update ()
    {
        toolOn.volume = Mathf.Lerp(toolOn.volume, volumeApplied, .08f);
        toolOn.pitch = Mathf.Lerp(toolOn.pitch, pitchApplied, .08f);
    }
}
