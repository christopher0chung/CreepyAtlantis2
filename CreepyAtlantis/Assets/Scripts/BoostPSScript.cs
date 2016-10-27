using UnityEngine;
using System.Collections;

public class BoostPSScript : MonoBehaviour {
    public bool onOff;
    public int exhaustVolumeInv;

    private int exhaustCounter;
    private ParticleSystem myPS;

    // Use this for initialization
    void Start()
    {
        myPS = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (onOff)
        {
            exhaustCounter++;
            if (exhaustCounter >= exhaustVolumeInv)
            {
                myPS.Emit(2);
                exhaustCounter = 0;
            }
        }
    }
}