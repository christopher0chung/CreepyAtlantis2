using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkAttacking : MonoBehaviour, IIlluminable {

    private FSM<SharkAttacking> _fsm;

    [SerializeField] float patrolSpeed;
    [SerializeField] float upSpeed;

    [HideInInspector] public float appliedSpeed;

    private enum SharkDir { Left, Right}
    private SharkDir currentDir;

    private Transform cam;

    private uint passCounter;
    private bool p1Illumin;
    private bool p2Illumin;
    private float p1Timer;
    private float p2Timer;

    private void Start()
    {
        _fsm = new FSM<SharkAttacking>(this);

        _fsm.TransitionTo<Patrol>();

        cam = Camera.main.transform;

        appliedSpeed = patrolSpeed;
    }

    private void Update()
    {
        _fsm.Update();
        IlluminTrigger();
        CheckToClearIllumination();
        //Debug.Log(_fsm.CurrentState.GetType());
    }

    //------------------------------------------------
    // Shared functionality
    //------------------------------------------------

    private void Move(SharkDir thisDir)
    {
        appliedSpeed = patrolSpeed;
        if( thisDir == SharkDir.Right)
        {
            transform.position += Vector3.right * appliedSpeed * Time.deltaTime;
        }
        else
        {
            transform.position -= Vector3.right * appliedSpeed * Time.deltaTime;
        }
    }

    private void Run(SharkDir thisDir)
    {
        appliedSpeed = upSpeed;
        if (thisDir == SharkDir.Right)
        {
            transform.position += Vector3.right * appliedSpeed * 1.5f * Time.deltaTime;
        }
        else
        {
            transform.position -= Vector3.right * appliedSpeed * 1.5f * Time.deltaTime;
        }
    }

    private void Advance(SharkDir thisDir)
    {
        appliedSpeed = patrolSpeed;
        if (this.transform.position.x >=6)
        {
            Debug.Log("advancing");
            if (thisDir == SharkDir.Right)
            {
                transform.position += (Vector3.right - Vector3.forward) * appliedSpeed * Time.deltaTime;
            }
            else
            {
                transform.position += (-Vector3.right - Vector3.forward) * appliedSpeed * Time.deltaTime;
            }
        }
        else
        {
            Move(thisDir);
        }
    }

    private bool CheckOffScreen()
    {
        if (currentDir == SharkDir.Right)
        {
            if (transform.position.x - cam.position.x > 40)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (transform.position.x - cam.position.x < -40)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private bool CheckCenterOfScreen()
    {
        if (Mathf.Abs(transform.position.x - cam.position.x) < 5)
            return true;
        else
            return false;
    }

    private void Chase (Vector3 where)
    {
        appliedSpeed = upSpeed;
        transform.position = Vector3.MoveTowards(transform.position, where, appliedSpeed * Time.deltaTime);
    }

    //------------------------------------------------
    // Interface functionality
    //------------------------------------------------

    public void Illuminate(GameObject whoIsIlluminating)
    {
        if (whoIsIlluminating.transform.root.gameObject.name == "Character0")
        {
            p1Timer = 0;
            p1Illumin = true;
        }
        if (whoIsIlluminating.transform.root.gameObject.name == "Character1")
        {
            p2Timer = 0;
            p2Illumin = true;
        }
    }

    private void IlluminTrigger()
    {
        if (p1Illumin && p2Illumin)
            _fsm.TransitionTo<Flee>();
    }

    private void CheckToClearIllumination ()
    {
        p1Timer += Time.deltaTime;
        p2Timer += Time.deltaTime;

        if (p1Timer >= .25f)
            p1Illumin = false;

        if (p2Timer >= .25f)
            p2Illumin = false;
    }

    //------------------------------------------------
    // Physics interaction
    //------------------------------------------------

    void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStay Shark");
        if ((other.transform.root.gameObject.tag == "Character") && ((BasicState)_fsm.CurrentState).name != "Flee")
        {
            other.transform.root.GetComponent<CollisionDeath>().StartDeathSeq();
            _fsm.TransitionTo<Flee>();
        }
    }

    //------------------------------------------------
    // States
    //------------------------------------------------

    private class BasicState : FSM<SharkAttacking>.State
    {
        public string name;
    }

    private class Wait : BasicState
    {
        float timer;

        public override void Init()
        {
            name = "Wait";
        }

        public override void OnEnter()
        {
            
        }

        public override void Update()
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                if (Context.passCounter >= 4)
                    Destroy(Context.gameObject);
                else if (Context.transform.position.z <= 5)
                    TransitionTo<Interact>();
                else if (SubInKelp.inKelp)
                    TransitionTo<Stalk>();
                else if (!SubInKelp.inKelp)
                    TransitionTo<Patrol>();
            }
        }

        public override void CleanUp()
        {

        }
    }

    private class Patrol : BasicState
    {
        public override void Init()
        {
            name = "Patrol";
        }

        public override void OnEnter()
        {
            Context.passCounter++;
            if (Context.currentDir == SharkDir.Right)
            {
                Context.currentDir = SharkDir.Left;
                Context.transform.position = new Vector3(Context.cam.position.x + 40, Context.transform.position.y, Context.transform.position.z);
            }
            else
            {
                Context.currentDir = SharkDir.Right;
                Context.transform.position = new Vector3(Context.cam.position.x - 40, Context.transform.position.y, Context.transform.position.z);
            }
        }

        public override void Update()
        {
            Context.Move(Context.currentDir);
            if (Context.CheckOffScreen())
            {
                TransitionTo<Wait>();
            }
        }

        public override void CleanUp()
        {

        }
    }
    
    private class Stalk : BasicState
    {
        public override void Init()
        {
            name = "Stalk";
        }

        public override void OnEnter()
        {
            if (Context.currentDir == SharkDir.Right)
            {
                Context.currentDir = SharkDir.Left;
                Context.transform.position = new Vector3(Context.cam.position.x + 40, Context.transform.position.y, Context.transform.position.z);
            }
            else
            {
                Context.currentDir = SharkDir.Right;
                Context.transform.position = new Vector3(Context.cam.position.x - 40, Context.transform.position.y, Context.transform.position.z);
            }
        }

        public override void Update()
        {
            if (Context.CheckCenterOfScreen())
                Context.Advance(Context.currentDir);
            else
                Context.Move(Context.currentDir);

            if (Context.CheckOffScreen())
            {
                TransitionTo<Wait>();
            }
        }

        public override void CleanUp()
        {

        }
    }

    private class Interact : BasicState
    {
        GameObject[] players;

        public override void Init()
        {
            name = "Interact";
            players = GameObject.FindWithTag("Sub").GetComponent<PlayerArrayReference>().players;
            Context.transform.GetChild(0).GetChild(0).GetComponentInChildren<SkinnedMeshRenderer>().material = (Material)Resources.Load("SharkMatAlt");
        }

        public override void OnEnter()
        {
            if (Context.currentDir == SharkDir.Right)
            {
                Context.currentDir = SharkDir.Left;
                Context.transform.position = new Vector3(Context.cam.position.x + 40, Context.cam.position.y + 5, 0);
            }
            else
            {
                Context.currentDir = SharkDir.Right;
                Context.transform.position = new Vector3(Context.cam.position.x - 40, Context.cam.position.y + 5, 0);
            }
        }

        public override void Update()
        {
            //if no players just cruise by
            if (players[0].activeInHierarchy == false && players[1].activeInHierarchy == false)
            {
                Context.Move(Context.currentDir);
            }
            // if both players chase the closest one
            else if (players[0].activeInHierarchy == true && players[1].activeInHierarchy == true)
            {
                if (Vector3.Distance(players[0].transform.position, Context.transform.position) < Vector3.Distance(players[1].transform.position, Context.transform.position))
                {
                    Context.Chase(players[0].transform.position);
                }
                else
                {
                    Context.Chase(players[1].transform.position);
                }
            }
            // if one player chase that player
            else
            {
                if (players[0].activeInHierarchy)
                    Context.Chase(players[0].transform.position);
                else
                    Context.Chase(players[1].transform.position);
            }

            // if shark leaves
            if (Context.CheckOffScreen())
            {
                Destroy(Context.gameObject);
            }
        }

        public override void CleanUp()
        {

        }
    }

    private class Flee : BasicState
    {

        public override void Init()
        {
            name = "Flee";
        }

        public override void OnEnter()
        {
            if (Context.currentDir == SharkDir.Right)
            {
                Context.currentDir = SharkDir.Left;
            }
            else
            {
                Context.currentDir = SharkDir.Right;
            }
        }

        public override void Update()
        {
            Context.Run(Context.currentDir);
            if (Context.CheckOffScreen())
            {
                Destroy(Context.gameObject);
            }
        }

        public override void CleanUp()
        {

        }
    }
}
