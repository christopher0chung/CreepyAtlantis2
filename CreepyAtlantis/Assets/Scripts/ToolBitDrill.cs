using UnityEngine;
using System.Collections;

public class ToolBitDrill : MonoBehaviour, ToolBitInterface {

    public float turnRate;
    public float onAirConsumptionRate;
    public float offAirConsumptionRate;

    private PlayerAir myAir;

    public int onBubbleRate;
    public int offBubbleRate;
    public ParticleSystem airBubbles;

    public int dustPartRate;
    public ParticleSystem dustParticles;

    private bool drilling;

    private int partCounter;
    public int counterUpperLimit;

    void Start ()
    {
        myAir = transform.parent.parent.GetComponent<PlayerAir>();
        drilling = true;
    }

    public void OnState ()
    {
        transform.Rotate(0, 0, turnRate);
        myAir.Consume(onAirConsumptionRate);
        partCounter++;


        if (partCounter >= counterUpperLimit)
        {
            partCounter = 0;
            airBubbles.Emit(onBubbleRate);
            if (drilling)
                dustParticles.Emit(dustPartRate);
        }

    }

    public void OffState ()
    {
        myAir.Consume(offAirConsumptionRate);
        airBubbles.Emit(onBubbleRate);
    }

    //public void OnTriggerStay()
    //{
    //    drilling = true;
    //}

    //public void OnTriggerExit()
    //{
    //    drilling = false;
    //}
}
