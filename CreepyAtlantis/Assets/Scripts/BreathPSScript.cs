using UnityEngine;
using System.Collections;

public class BreathPSScript : MonoBehaviour {

    public bool onOff;
    public int breathCycle;
    public int exhaleCycle;
    public int exhaleVolumeInv;

    private int counter;
    private int breathCounter;
    private ParticleSystem myPS;

    private AudioSource myAS;

	// Use this for initialization
	void Start () {
        myAS = GetComponent<AudioSource>();
        myPS = GetComponent<ParticleSystem>();
        counter = Random.Range(0, breathCycle);
        //Debug.Log(counter);

        myAS.pitch = .5f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (onOff)
        {
            counter++;
            if (counter >= breathCycle)
                counter = 0;

            if (counter <exhaleCycle)
            {
                myAS.volume = Mathf.Lerp(myAS.volume, 1, .1f);

                breathCounter++;
                if (breathCounter >= exhaleVolumeInv)
                {
                    myPS.Emit(1);
                    breathCounter = 0;

                }
            }
            else
            {
                myAS.volume = Mathf.Lerp(myAS.volume, 0, .1f);            
            }
        }
	}
}
