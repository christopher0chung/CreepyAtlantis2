using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallastAnim : MonoBehaviour {

    private float timer;

    private bool _play;
    private bool play
    {
        get
        {
            return _play;
        }
        set
        {
            if (value != _play)
            {
                _play = value;
                if (_play)
                {
                    myPS.Play();
                }
                else
                {
                    myPS.Stop();
                }
            }
        }
    }
    private ParticleSystem myPS;

    [SerializeField] private float threshold;

	// Use this for initialization
	void Start () {
        myPS = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.root.GetComponent<SubControlScript>().resultantMoveVector.y < -threshold)
        {
            play = true;
        }      
        else
        {
            play = false;
        }
	}
}
