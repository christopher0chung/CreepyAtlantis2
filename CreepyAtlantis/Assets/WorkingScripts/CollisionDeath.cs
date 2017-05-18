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
                Invoke("Death", normalizedTime);
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
        EventManager.instance.Register<GE_Air>(LocalHandler);
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

    private void LocalHandler(GameEvent e)
    {
        if (e.GetType() == typeof(GE_Air))
        {
            GE_Air a = (GE_Air)e;
            if (a.howMuch <= 0)
            {
                if ((a.speaker == Speaker.Ops && gameObject.name == "Character0") || (a.speaker == Speaker.Doc && gameObject.name == "Character1"))
                {
                    Drown();
                }
            }
        }
    }

    public void Drown()
    {
        transform.root.Find("Effects").gameObject.SetActive(false);
        transform.root.Find("AirUnit").gameObject.SetActive(false);
        transform.root.GetComponent<PlayerController>().enabled = false;
        thisDeathStyle = DeathStyle.Black;
        //GameObject.Find("GameStateManager").GetComponent<GameStateManager>().SetControls(0, Controllables.none);
        //GameObject.Find("GameStateManager").GetComponent<GameStateManager>().SetControls(1, Controllables.none);
        StartDeathSeq();
    }

    public void Disappear()
    {
        transform.root.Find("Model").gameObject.SetActive(false);
        transform.root.Find("Effects").gameObject.SetActive(false);
        transform.root.Find("AirUnit").gameObject.SetActive(false);
        transform.root.GetComponent<PlayerController>().enabled = false;
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

            //ShutDownControls();

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
            if (!(other.transform.root.gameObject.name == "Character0" || other.transform.root.gameObject.name == "Character1"))
            {
            Debug.Log(other.transform.root.gameObject.name + " killed the sub");
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
    }

    void DelayVign()
    {
        death = true;
    }

    void Death()
    {
        //Debug.Log("Invoked");
        EventManager.instance.Fire(new GE_LoadLevelRequest(0));
    }

    //void ShutDownControls()
    //{
    //    GameObject[] theChars = GameObject.FindGameObjectsWithTag("Character");
    //    foreach(GameObject theChar in theChars)
    //    {
    //        theChar.GetComponent<ControllerAdapter>().enabled = false;
    //    }

    //    ControllerAdapter[] theAdapters = GameObject.FindGameObjectWithTag("Sub").GetComponents<ControllerAdapter>();
    //    foreach (ControllerAdapter theAdapter in theAdapters)
    //    {
    //        theAdapter.enabled = false;
    //    }
    //}
}

public enum DeathStyle { Red, Black }
public enum DeathControlType { Character, Sub }
