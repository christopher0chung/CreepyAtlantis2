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

    public LayerMask myLM;

    void Start ()
    {
        myAir = transform.parent.parent.GetComponent<PlayerAir>();
        //drilling = true;
    }

    public void OnState ()
    {
        transform.Rotate(turnRate, 0, 0);
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

    public void OnCollisionStay2D(Collision2D other)
    {
        drilling = true;
        IDestructable myDestructable;
        if (other.gameObject.layer == myLM)
        {
            myDestructable = other.gameObject.GetComponentInChildren<IDestructable>();
            myDestructable.DamageIt(.5f);
        }

    }

    public void OnCollisionExit2D()
    {
        drilling = false;
    }
}
