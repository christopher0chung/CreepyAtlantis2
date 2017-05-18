using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GE_Music : GameEvent
{
    public MusicEmoReg reg;
    public float str;
    public GE_Music(MusicEmoReg r, float f)
    {
        reg = r;
        str = f;
    }
}

public enum MusicEmoReg { Creepy, Danger, Depth, Narc, Base, Stalking }
public class MusicController : MonoBehaviour
{
    public enum Tempo { None, A }

    private class AudioHistogramElement
    {
        public readonly MusicEmoReg myEmoReg;
        public readonly AudioClip myClip;
        public readonly float myLayerLowerThreshold;
        public readonly float myLayerUpperThreshold;
        public readonly bool loopedClip;
        public readonly Tempo myTempo;

        public AudioHistogramElement(MusicEmoReg r, AudioClip c, float lT, float uT)
        {
            myEmoReg = r;
            myClip = c;
            myLayerLowerThreshold = lT;
            myLayerUpperThreshold = uT;

            if (c.name.Substring(7, 1) == "L")
                loopedClip = true;
            else
                loopedClip = false;

            if (c.name.Substring(5, 1) == "A")
                myTempo = Tempo.A;
            else
                myTempo = Tempo.None;           
        }
    }

    private readonly List<AudioHistogramElement> _elementList = new List<AudioHistogramElement>();
    private readonly Dictionary<AudioHistogramElement, AudioSource> _tempoLoopedLayerSources = new Dictionary<AudioHistogramElement, AudioSource>();

    [SerializeField] private float[] _layerHistVal; 
    private float[] _layerDecNormalizedTime;

    private FSM<MusicController> _fsm;

    private float depthVal;

    void Start()
    {
        _fsm = new FSM<MusicController>(this);
        _fsm.TransitionTo<Standby>();

        _layerHistVal = new float[System.Enum.GetValues(typeof(MusicEmoReg)).Length];
        _layerDecNormalizedTime = new float[System.Enum.GetValues(typeof(MusicEmoReg)).Length];

        _elementList.Add(new AudioHistogramElement(MusicEmoReg.Base, (AudioClip)Resources.Load("Music/" + "TEMPO0-L-Ocean bubbles low loop"), 0, .1f));

        _elementList.Add(new AudioHistogramElement(MusicEmoReg.Depth, (AudioClip)Resources.Load("Music/" + "TEMPO0-L-New depths"), 1, 2));
        _elementList.Add(new AudioHistogramElement(MusicEmoReg.Depth, (AudioClip)Resources.Load("Music/" + "TEMPO0-L-Ocean floor"), 2, 10));

        _elementList.Add(new AudioHistogramElement(MusicEmoReg.Danger, (AudioClip)Resources.Load("Music/" + "TEMPOA-L-Attacking perc 1 loop"), 0, 2));
        _elementList.Add(new AudioHistogramElement(MusicEmoReg.Danger, (AudioClip)Resources.Load("Music/" + "TEMPOA-L-Attacking snare 1 loop"), 0, 2));

        _elementList.Add(new AudioHistogramElement(MusicEmoReg.Stalking, (AudioClip)Resources.Load("Music/" + "TEMPOA-L-Stalking kick 0 loop"), 0, 4));
        _elementList.Add(new AudioHistogramElement(MusicEmoReg.Stalking, (AudioClip)Resources.Load("Music/" + "TEMPOA-L-Stalking kick 1 loop"), 2, 5));
        _elementList.Add(new AudioHistogramElement(MusicEmoReg.Stalking, (AudioClip)Resources.Load("Music/" + "TEMPOA-L-Stalking kick 2 loop"), 3, 6));
        _elementList.Add(new AudioHistogramElement(MusicEmoReg.Stalking, (AudioClip)Resources.Load("Music/" + "TEMPOA-L-Stalking perc 1 loop"), 4, 7));
        _elementList.Add(new AudioHistogramElement(MusicEmoReg.Stalking, (AudioClip)Resources.Load("Music/" + "TEMPOA-L-Stalking snare 1 loop"), 5, 8));
        _elementList.Add(new AudioHistogramElement(MusicEmoReg.Stalking, (AudioClip)Resources.Load("Music/" + "TEMPOA-L-Stalking bass 1 loop"), 9, 10));

        _elementList.Add(new AudioHistogramElement(MusicEmoReg.Narc, (AudioClip)Resources.Load("Music/" + "TEMPOA-N-Narced A"), 1, 2));

        _elementList.Add(new AudioHistogramElement(MusicEmoReg.Creepy, (AudioClip)Resources.Load("Music/" + "TEMPOA-L-Ops melody A"), .5f, 1.5f));
        _elementList.Add(new AudioHistogramElement(MusicEmoReg.Creepy, (AudioClip)Resources.Load("Music/" + "TEMPOA-L-Ops melody B"), 1.5f, 2.5f));
        _elementList.Add(new AudioHistogramElement(MusicEmoReg.Creepy, (AudioClip)Resources.Load("Music/" + "TEMPOA-L-Ops melody C"), 2.5f, 3.5f));
        _elementList.Add(new AudioHistogramElement(MusicEmoReg.Creepy, (AudioClip)Resources.Load("Music/" + "TEMPOA-L-Ops melody D"), 3.5f, 4.5f));

        _InitLoops();
        _InitHistogramDecVals();

        foreach (AudioHistogramElement theAHE in _elementList)
        {
            AudioSource thisAS;
            _tempoLoopedLayerSources.TryGetValue(theAHE, out thisAS);
            if (theAHE.myEmoReg == MusicEmoReg.Base)
            {
                thisAS.volume = .5f;
            }
        }

        EventManager.instance.Register<GE_Music>(LocalHandler);
    }

    void LocalHandler(GameEvent e)
    {
        GE_Music m = (GE_Music)e;
        if (m.str >= _layerHistVal[(int)m.reg])
        {
            _layerHistVal[(int)m.reg] = m.str;
        }
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1) && ((BasicState)_fsm.CurrentState).name != "UpdateNarc")
        //{
        //    _layerHistVal[(int)MusicEmoReg.Narc] += 1f;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    _layerHistVal[(int)MusicEmoReg.Danger] += 1f;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    _layerHistVal[(int)MusicEmoReg.Stalking] += 1f;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha4) && ((BasicState)_fsm.CurrentState).name != "UpdateCreepy")
        //{
        //    _layerHistVal[(int)MusicEmoReg.Creepy] += 1f;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha5) && ((BasicState)_fsm.CurrentState).name != "UpdateCreepy")
        //{
        //    _layerHistVal[(int)MusicEmoReg.Creepy] += 2f;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha6) && ((BasicState)_fsm.CurrentState).name != "UpdateCreepy")
        //{
        //    _layerHistVal[(int)MusicEmoReg.Creepy] += 3f;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha7) && ((BasicState)_fsm.CurrentState).name != "UpdateCreepy")
        //{
        //    _layerHistVal[(int)MusicEmoReg.Creepy] += 4f;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha8))
        //{
        //    EventNewDepth();
        //}

        _UpdateHistogram();
        _fsm.Update();
        _UpdateLayersFromHistogram();
    }

    //---------------------------------------------------
    // GameEvent system methods
    //---------------------------------------------------

    private void EventNewDepth ()
    {
        depthVal = 5;
    }

    //---------------------------------------------------
    // FSM States
    //---------------------------------------------------

    private class BasicState : FSM<MusicController>.State
    {
        public string name;
    }

    private class Standby : BasicState
    {
        public override void Init()
        {
            name = "Standby";
        }

        public override void OnEnter()
        {

        }

        public override void Update()
        {
            return;
        }

        public override void OnExit()
        {

        }
    }

    private class UpdateNarc : BasicState
    {
        private float timer;
        public override void Init()
        {
            name = "UpdateNarc";
        }

        public override void OnEnter()
        {
            foreach (AudioHistogramElement theAHE in Context._elementList)
            {
                AudioSource thisAS;
                Context._tempoLoopedLayerSources.TryGetValue(theAHE, out thisAS);
                if (thisAS != null && theAHE.myEmoReg != MusicEmoReg.Narc && theAHE.myEmoReg != MusicEmoReg.Depth && theAHE.myEmoReg != MusicEmoReg.Base)
                {
                    thisAS.volume = 0;
                }
                if (theAHE.myEmoReg == MusicEmoReg.Narc)
                {
                    Context.GetComponent<AudioSource>().PlayOneShot(theAHE.myClip);
                }
            }
            timer = 0;
        }

        public override void Update()
        {
            timer += Time.deltaTime;
            if (timer >= 8)
                TransitionTo<Standby>();
        }

        public override void OnExit()
        {

        }
    }

    private class UpdateDanger : BasicState
    {
        public override void Init()
        {
            name = "UpdateDanger";
        }

        public override void OnEnter()
        {
            foreach (AudioHistogramElement theAHE in Context._elementList)
            {
                AudioSource thisAS;
                Context._tempoLoopedLayerSources.TryGetValue(theAHE, out thisAS);
                if (thisAS != null && theAHE.myEmoReg != MusicEmoReg.Danger && theAHE.myEmoReg != MusicEmoReg.Depth && theAHE.myEmoReg != MusicEmoReg.Base)
                {
                    thisAS.volume = 0;
                }
                if (thisAS != null && theAHE.myEmoReg == MusicEmoReg.Danger)
                {
                    thisAS.Play();
                }
            }
        }

        public override void Update()
        {
            foreach (AudioHistogramElement theAHE in Context._elementList)
            {
                AudioSource thisAS;
                Context._tempoLoopedLayerSources.TryGetValue(theAHE, out thisAS);
                if (thisAS != null && theAHE.myEmoReg == MusicEmoReg.Danger)
                {
                    thisAS.volume = Mathf.Lerp(
                        thisAS.volume,
                        (Context._layerHistVal[(int)theAHE.myEmoReg] - theAHE.myLayerLowerThreshold) / (theAHE.myLayerUpperThreshold - theAHE.myLayerLowerThreshold),
                        Time.deltaTime);
                }
            }
            if (Context._layerHistVal[(int)MusicEmoReg.Danger] <= .001f)
            {
                Context._layerHistVal[(int)MusicEmoReg.Danger] = 0;
                TransitionTo<Standby>();
            }
        }

        public override void OnExit()
        {
            foreach (AudioHistogramElement theAHE in Context._elementList)
            {
                AudioSource thisAS;
                Context._tempoLoopedLayerSources.TryGetValue(theAHE, out thisAS);
                if (thisAS != null && theAHE.myEmoReg == MusicEmoReg.Danger)
                {
                    thisAS.Stop();
                }
            }
        }
    }

    private class UpdateStalking : BasicState
    {
        public override void Init()
        {
            name = "UpdateStalking";
        }

        public override void OnEnter()
        {
            foreach (AudioHistogramElement theAHE in Context._elementList)
            {
                AudioSource thisAS;
                Context._tempoLoopedLayerSources.TryGetValue(theAHE, out thisAS);
                if (thisAS != null && theAHE.myEmoReg != MusicEmoReg.Stalking && theAHE.myEmoReg != MusicEmoReg.Depth && theAHE.myEmoReg != MusicEmoReg.Base)
                {
                    thisAS.volume = 0;
                }
                if (thisAS != null && theAHE.myEmoReg == MusicEmoReg.Stalking)
                {
                    thisAS.Play();
                }
            }
        }

        public override void Update()
        {
            foreach (AudioHistogramElement theAHE in Context._elementList)
            {
                AudioSource thisAS;
                Context._tempoLoopedLayerSources.TryGetValue(theAHE, out thisAS);
                if (thisAS != null && theAHE.myEmoReg == MusicEmoReg.Stalking)
                {
                    thisAS.volume = Mathf.Lerp(
                        thisAS.volume,
                        (Context._layerHistVal[(int)theAHE.myEmoReg] - theAHE.myLayerLowerThreshold) / (theAHE.myLayerUpperThreshold - theAHE.myLayerLowerThreshold),
                        Time.deltaTime);
                }
            }
            if (Context._layerHistVal[(int)MusicEmoReg.Stalking] <= .001f)
            {
                Context._layerHistVal[(int)MusicEmoReg.Stalking] = 0;
                TransitionTo<Standby>();
            }
        }

        public override void OnExit()
        {
            foreach (AudioHistogramElement theAHE in Context._elementList)
            {
                AudioSource thisAS;
                Context._tempoLoopedLayerSources.TryGetValue(theAHE, out thisAS);
                if (thisAS != null && theAHE.myEmoReg == MusicEmoReg.Stalking)
                {
                    thisAS.Stop();
                }
            }
        }
    }

    private class UpdateCreepy : BasicState
    {
        private float clipLength;
        private float timer;

        public override void Init()
        {
            name = "UpdateCreepy";
        }

        public override void OnEnter()
        {
            timer = 0;
            foreach (AudioHistogramElement theAHE in Context._elementList)
            {
                AudioSource thisAS;
                Context._tempoLoopedLayerSources.TryGetValue(theAHE, out thisAS);
                if (thisAS != null && theAHE.myEmoReg != MusicEmoReg.Creepy && theAHE.myEmoReg != MusicEmoReg.Depth && theAHE.myEmoReg != MusicEmoReg.Base)
                {
                    thisAS.volume = 0;
                }
                if (theAHE.myEmoReg == MusicEmoReg.Creepy)
                {
                    if (Context._layerHistVal[(int)MusicEmoReg.Creepy] >= theAHE.myLayerLowerThreshold
                        && Context._layerHistVal[(int)MusicEmoReg.Creepy] < theAHE.myLayerUpperThreshold)
                    {
                        Context.GetComponent<AudioSource>().PlayOneShot(theAHE.myClip);
                        clipLength = theAHE.myClip.length;
                        return;
                    }
                }
            }
        }

        public override void Update()
        {
            timer += Time.deltaTime;
            if (timer >= clipLength)
            {
                TransitionTo<Standby>();
            }
        }

        public override void OnExit()
        {
            Context._layerHistVal[(int)MusicEmoReg.Creepy] = 0;
        }
    }

    //---------------------------------------------------
    // Internal Functions
    //---------------------------------------------------

    private void _InitLoops()
    {
        // based on all of the layers, grab everything that is supposed to loop
        // then add it to its own audiosource
        // and create a future reference for use

        foreach (AudioHistogramElement a in _elementList)
        {
            if (a.loopedClip == true && a.myEmoReg != MusicEmoReg.Creepy)
            {
                GameObject layer = (GameObject)Instantiate(Resources.Load("LoopedLayer"), transform);
                AudioSource myAS = layer.GetComponent<AudioSource>();

                layer.name = a.myClip.name;

                myAS.clip = a.myClip;
                myAS.loop = true;

                if (a.myTempo == Tempo.None)
                    myAS.Play();
                else
                    myAS.Stop();

                myAS.volume = 0;
                _tempoLoopedLayerSources.Add(a, myAS);
            }
        }
    }

    private void _InitHistogramDecVals()
    {
        _layerDecNormalizedTime[(int)MusicEmoReg.Depth] = 1f;

        _layerDecNormalizedTime[(int)MusicEmoReg.Narc] = .25f;
        _layerDecNormalizedTime[(int)MusicEmoReg.Creepy] = 3;
        _layerDecNormalizedTime[(int)MusicEmoReg.Danger] = .5f;
        _layerDecNormalizedTime[(int)MusicEmoReg.Stalking] = 1;
    }

    private void _UpdateHistogram()
    {
        // Creepy and Danger wear off
        _layerHistVal[(int)MusicEmoReg.Narc] = Mathf.MoveTowards(_layerHistVal[(int)MusicEmoReg.Narc], 0, Time.deltaTime / _layerDecNormalizedTime[(int)MusicEmoReg.Narc]);
        _layerHistVal[(int)MusicEmoReg.Danger] = Mathf.MoveTowards(_layerHistVal[(int)MusicEmoReg.Danger], 0, Time.deltaTime / _layerDecNormalizedTime[(int)MusicEmoReg.Danger]);
        _layerHistVal[(int)MusicEmoReg.Stalking] = Mathf.MoveTowards(_layerHistVal[(int)MusicEmoReg.Stalking], 0, Time.deltaTime / _layerDecNormalizedTime[(int)MusicEmoReg.Stalking]);

        // Modify volume based on stored depth value
        _layerHistVal[(int)MusicEmoReg.Depth] = Mathf.MoveTowards(_layerHistVal[(int)MusicEmoReg.Depth], depthVal, Time.deltaTime / _layerDecNormalizedTime[(int)MusicEmoReg.Depth]);

        // Clamps all values from 0 to 10
        for (int i = 0; i < System.Enum.GetValues(typeof(MusicEmoReg)).Length; i++)
        {
            _layerHistVal[i] = Mathf.Clamp(_layerHistVal[i], 0, 10);
        }
    }

    private void _UpdateLayersFromHistogram()
    {
        foreach (AudioHistogramElement theAHE in _elementList)
        {
            AudioSource thisAS;
            _tempoLoopedLayerSources.TryGetValue(theAHE, out thisAS);
            if (thisAS != null && theAHE.myEmoReg == MusicEmoReg.Depth)
            {
                thisAS.volume = Mathf.Lerp(
                        thisAS.volume,
                        (_layerHistVal[(int)theAHE.myEmoReg] - theAHE.myLayerLowerThreshold) / (theAHE.myLayerUpperThreshold - theAHE.myLayerLowerThreshold),
                        Time.deltaTime);
            }
        }

        if (_layerHistVal[(int)MusicEmoReg.Narc] > 0)
        {
            if (((BasicState)_fsm.CurrentState).name != "UpdateNarc")
            {
                _fsm.TransitionTo<UpdateNarc>();
                return;
            }
        }
        else if (_layerHistVal[(int)MusicEmoReg.Danger] > 0)
        {
            if (((BasicState)_fsm.CurrentState).name != "UpdateDanger")
            {
                _fsm.TransitionTo<UpdateDanger>();
                return;
            }
        }
        else if (_layerHistVal[(int)MusicEmoReg.Stalking] > 0)
        {
            if (((BasicState)_fsm.CurrentState).name != "UpdateStalking")
            {
                _fsm.TransitionTo<UpdateStalking>();
                return;
            }
        }
        else if (_layerHistVal[(int)MusicEmoReg.Creepy] > 0)
        {
            if (((BasicState)_fsm.CurrentState).name != "UpdateCreepy")
            {
                _fsm.TransitionTo<UpdateCreepy>();
                return;
            }
        }
    }
}
