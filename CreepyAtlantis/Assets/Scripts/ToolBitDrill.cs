using UnityEngine;
using System.Collections;

public class ToolBitDrill : MonoBehaviour, ToolBitInterface {

    private GameObject bitModel;

    public float turnRate;
    public float onAirConsumptionRate;
    public float offAirConsumptionRate;

    private PlayerAir myAir;

    public int onBubbleRate;
    public int offBubbleRate;
    public ParticleSystem airBubbles;

    public int dustPartRate;
    public ParticleSystem dustParticles;

    public bool drilling;

    private int partCounter;
    public int counterUpperLimit;

    public LayerMask myLM;

    private bool drillSpinning;

    private AudioSource drillEngaged;
    private float volumeOperating = 1;
    private float volumeApplied;

    void Start ()
    {
        bitModel = transform.GetChild(0).gameObject;
        myAir = transform.parent.parent.GetComponent<PlayerAir>();
        drillEngaged = GetComponent<AudioSource>();
        drillEngaged.volume = 0;
    }

    void Update()
    {
        drillEngaged.volume = volumeApplied;
    }

    public void OnState ()
    {
        drillSpinning = true;

        bitModel.transform.Rotate(0, 0, turnRate);
        myAir.Consume(onAirConsumptionRate);
        partCounter++;


        if (partCounter >= counterUpperLimit)
        {
            partCounter = 0;
            airBubbles.Emit(onBubbleRate);
            if (drilling)
            {
                volumeApplied = volumeOperating;
                dustParticles.Emit(dustPartRate);
            }
            else
            {
                volumeApplied = 0;
            }

        }

    }

    public void OffState ()
    {
        drillSpinning = false;
        drilling = false;
        volumeApplied = 0;
        myAir.Consume(offAirConsumptionRate);
    }

    public void OnCollisionStay2D(Collision2D other)
    {
        drilling = true;
        IDestructable myDestructable;
        if (other.gameObject.layer == myLM && drillSpinning)
        {
            myDestructable = other.gameObject.GetComponentInChildren<IDestructable>();
            myDestructable.DamageIt(.5f);
        }
    }

    public void OnCollisionExit2D()
    {
        drilling = false;
        Debug.Log("Is this happening?");
    }
}
