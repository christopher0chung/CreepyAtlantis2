﻿using System.Collections;
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
                Invoke("Death", normalizedTime);
            }
        }
    }

    private float timer;
    private float normalizedTime = 3;
    private float delayTime = 2;

    private GameObject sub;

    [SerializeField] private float storedVel;

	void Start () {
        myBDeath = GameObject.Find("VignMasks").GetComponent<VignetteController>();
        myRDeath = GameObject.Find("DeathMasks").GetComponent<VignetteController>();
        //Debug.Log("myBDeath found is " + myBDeath);
        sub = GameObject.Find("Sub");
	}
	
	void Update () {

        storedVel = Vector3.Magnitude(this.gameObject.GetComponent<Rigidbody>().velocity);

        CheckOffScreen();

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

    public void Drown()
    {
        transform.root.Find("Effects").gameObject.SetActive(false);
        transform.root.Find("AirUnit").gameObject.SetActive(false); thisDeathStyle = DeathStyle.Black;
        GameObject.Find("GameStateManager").GetComponent<GameStateManager>().SetControls(0, Controllables.none);
        GameObject.Find("GameStateManager").GetComponent<GameStateManager>().SetControls(1, Controllables.none);
        StartDeathSeq();
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

            GetComponent<Collider>().enabled = false;

            ShutDownControls();

            transform.root.Find("Model").gameObject.SetActive(false);
            transform.root.Find("Effects").gameObject.SetActive(false);
            transform.root.Find("AirUnit").gameObject.SetActive(false);

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

    void Death()
    {
        //Debug.Log("Invoked");
        GameObject.Find("GameStateManager").GetComponent<LevelLoader>().DeathUnload();
    }

    void ShutDownControls()
    {
        GameObject[] theChars = GameObject.FindGameObjectsWithTag("Character");
        foreach(GameObject theChar in theChars)
        {
            theChar.GetComponent<ControllerAdapter>().enabled = false;
        }

        ControllerAdapter[] theAdapters = GameObject.FindGameObjectWithTag("Sub").GetComponents<ControllerAdapter>();
        foreach (ControllerAdapter theAdapter in theAdapters)
        {
            theAdapter.enabled = false;
        }
    }

    void CheckOffScreen()
    {
        if (Mathf.Abs(transform.position.x - sub.transform.position.x) >= 32 || sub.transform.position.y - transform.position.y > 44)
        {
            DisappearDeath();
        }
    }

    void DisappearDeath()
    {
        Invoke("Drown", 2);

        GetComponent<Collider>().enabled = false;

        GameObject.Find("GameStateManager").GetComponent<GameStateManager>().SetControls(0, Controllables.none);
        GameObject.Find("GameStateManager").GetComponent<GameStateManager>().SetControls(1, Controllables.none);

        transform.root.Find("Model").gameObject.SetActive(false);
        transform.root.Find("Effects").gameObject.SetActive(false);
        transform.root.Find("AirUnit").gameObject.SetActive(false);
    }
}

public enum DeathStyle { Red, Black }
public enum DeathControlType { Character, Sub }
