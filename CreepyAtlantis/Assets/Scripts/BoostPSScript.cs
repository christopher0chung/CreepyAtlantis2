using UnityEngine;
using System.Collections;

public class BoostPSScript : MonoBehaviour {
    public bool onOff;
    public int exhaustVolumeInv;

    private int exhaustCounter;
    private ParticleSystem myPS;
    private AudioSource myAS;

    // Use this for initialization
    void Start()
    {
        myPS = GetComponent<ParticleSystem>();
        myAS = GetComponent<AudioSource>();
        myAS.volume = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (onOff)
        {
            myAS.volume = Mathf.Lerp(myAS.volume, 1, .1f);
            exhaustCounter++;
            if (exhaustCounter >= exhaustVolumeInv)
            {
                myPS.Emit(2);
                exhaustCounter = 0;
            }
        }
        else
        {
            myAS.volume = Mathf.Lerp(myAS.volume, 0, .1f);
        }
    }
}