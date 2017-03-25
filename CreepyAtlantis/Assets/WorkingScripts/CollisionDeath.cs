using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDeath : MonoBehaviour {

    [SerializeField] private DeathControlType whoThisIsFor;
    [SerializeField] private DeathStyle thisDeathStyle;

    private VignetteController myBDeath;
    private VignetteController myRDeath;

    private bool _death;
    private bool death
    {
        get
        {
            return _death;
        }
        set
        {
            if (value != _death)
            {
                _death = value;
                Invoke("Reload", normalizedTime);
            }
        }
    }

    private float timer;
    private float normalizedTime = 3;
    private float delayTime = 2;

    [SerializeField] private float storedVel;

	void Start () {
        myBDeath = GameObject.Find("VignMasks").GetComponent<VignetteController>();
        myRDeath = GameObject.Find("DeathMasks").GetComponent<VignetteController>();
        //Debug.Log("myBDeath found is " + myBDeath);
	}
	
	void Update () {

        storedVel = Vector3.Magnitude(this.gameObject.GetComponent<Rigidbody>().velocity);


        if (death)
        {
            timer += Time.deltaTime / normalizedTime;
            timer = Mathf.Clamp01(timer);
            if (thisDeathStyle == DeathStyle.Black)
            {
                myBDeath.slider = (1 - timer); 
            }
            else if (thisDeathStyle == DeathStyle.Red)
            {
                myBDeath.slider = (1 - timer);
            }
        }
        else
        {
            return;
        }
	}

    public void StartDeathSeq ()
    {
        if (thisDeathStyle == DeathStyle.Black)
        {
            Invoke("DelayVign", delayTime);
        }
        else if (thisDeathStyle == DeathStyle.Red)
        {
            Invoke("DelayVign", delayTime);
            Instantiate(Resources.Load("BloodExplosion"), transform.position, Quaternion.identity);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Collision");

        if (whoThisIsFor == DeathControlType.Character)
        {
            //Debug.Log("on Character");

            if (GetComponent<PlayerMovement>().grounded && other.gameObject.name == "Sub" && thisDeathStyle == DeathStyle.Red)
            {
                //Debug.Log("with Sub");
                StartDeathSeq();
            } 
        }
        else if (whoThisIsFor == DeathControlType.Sub)
        {
            //Debug.Log(Vector3.Magnitude(this.gameObject.GetComponent<Rigidbody>().velocity) + " " + storedVel);
            if (storedVel >= 2.0f)
            {
                //Debug.Log("Exceeding threshold");
                StartDeathSeq();
                transform.Find("Effects").GetChild(0).GetComponent<ParticleSystem>().Play();
                transform.Find("Effects").GetChild(1).GetComponent<ParticleSystem>().Play();

            }
        }
    }

    void DelayVign()
    {
        death = true;
    }

    void Reload()
    {
        //Debug.Log("Invoked");
        GameObject.Find("GameStateManager").GetComponent<LevelLoader>().ReloadLevel();
    }
}

public enum DeathStyle { Red, Black }
public enum DeathControlType { Character, Sub }
