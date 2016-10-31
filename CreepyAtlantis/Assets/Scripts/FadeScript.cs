using UnityEngine;
using System.Collections;

public class FadeScript : MonoBehaviour {

    public float timer;
    public MeshRenderer myMR;

    void Start ()
    {
        myMR = GetComponent<MeshRenderer>();
        timer = 2;
    }
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (timer > 1)
        {
            myMR.enabled = true;
        }
        else
        {
            myMR.enabled = false;
        }

	}

    public void Fade ()
    {
        timer = 0;
    }
}
