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

	// Use this for initialization
	void Start () {
        myPS = GetComponent<ParticleSystem>();
        counter = Random.Range(0, breathCycle);
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
                breathCounter++;
                if (breathCounter >= exhaleVolumeInv)
                {
                    myPS.Emit(1);
                    breathCounter = 0;

                }
            }
        }
	}
}
