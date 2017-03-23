using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDeath : MonoBehaviour {

    [SerializeField] private DeathControlType whoThisIsFor;
    [SerializeField] private DeathStyle thisDeathStyle;

    private VignetteController myBDeath;
    private VignetteController myRDeath;

    private bool bDeath;
    private bool rDeath;

    private float timer;
    [SerializeField]private float normalizedTime;

	void Start () {
        myBDeath = GameObject.Find("VignMasks").GetComponent<VignetteController>();
        myRDeath = GameObject.Find("DeathMasks").GetComponent<VignetteController>();
        Debug.Log("myBDeath found is " + myBDeath);
	}
	
	void Update () {
		if (bDeath || rDeath)
        {
            timer += Time.deltaTime / normalizedTime;
            timer = Mathf.Clamp01(timer);
            if (thisDeathStyle == DeathStyle.Black)
            {
                myBDeath.slider = (1 - timer); 
            }
            else if (thisDeathStyle == DeathStyle.Red)
            {
                myRDeath.slider = (1 - timer);
            }
        }
        else
        {
            return;
        }
	}

    private void StartDeathSeq ()
    {
        if (thisDeathStyle == DeathStyle.Black)
        {
            bDeath = true;
        }
        else if (thisDeathStyle == DeathStyle.Red)
        {
            rDeath = true;
        }
        Invoke("Reload", normalizedTime + .5f);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision");

        if (whoThisIsFor == DeathControlType.Character)
        {
            Debug.Log("on Character");

            if (GetComponent<PlayerMovement>().grounded && other.gameObject.name == "Sub" && thisDeathStyle == DeathStyle.Red)
            {
                Debug.Log("with Sub");
                StartDeathSeq();
            } 
        }
        else if (whoThisIsFor == DeathControlType.Sub)
        {
            Debug.Log("as Sub");
            if (other.gameObject.isStatic && Vector3.Magnitude(this.gameObject.GetComponent<Rigidbody>().velocity) > .25f)
            {
                Debug.Log("Exceeding threshold");
                StartDeathSeq();
                transform.Find("Effects").GetChild(0).GetComponent<ParticleSystem>().Play();
                transform.Find("Effects").GetChild(1).GetComponent<ParticleSystem>().Play();

            }
        }
    }

    void Reload()
    {
        Debug.Log("Invoked");
        GameObject.Find("GameStateManager").GetComponent<LevelLoader>().ReloadLevel();
    }
}

public enum DeathStyle { Red, Black }
public enum DeathControlType { Character, Sub }
