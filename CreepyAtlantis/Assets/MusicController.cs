using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public enum MusicEmoReg { Creepy, Danger, Depth, Narc, Base }
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

    void Start()
    {
        _fsm = new FSM<MusicController>(this);
        _fsm.TransitionTo<Standby>();

        _layerHistVal = new float[System.Enum.GetValues(typeof(MusicEmoReg)).Length];
        _layerDecNormalizedTime = new float[System.Enum.GetValues(typeof(MusicEmoReg)).Length];

        _elementList.Add(new AudioHistogramElement(MusicEmoReg.Base, (AudioClip)Resources.Load("Music/" + "TEMPO0-L-Base"), 0, .1f));

        _elementList.Add(new AudioHistogramElement(MusicEmoReg.Creepy, (AudioClip)Resources.Load("Music/" + "TEMPO0-L-New depths"), 1, 2));
        _elementList.Add(new AudioHistogramElement(MusicEmoReg.Creepy, (AudioClip)Resources.Load("Music/" + "TEMPO0-L-Ocean floor"), 2, 10));

        _elementList.Add(new AudioHistogramElement(MusicEmoReg.Narc, (AudioClip)Resources.Load("Music/" + "TEMPOA-N-Narced A"), 1, 2));

        _InitLoops();
        _InitHistogramDecVals();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("hello " + _layerHistVal[(int)MusicEmoReg.Danger] + " " + (int)MusicEmoReg.Danger);
            _layerHistVal[(int)MusicEmoReg.Danger] += .5f;
        }

        _layerHistVal[(int)MusicEmoReg.Danger] = 5;

        _UpdateHistogram();
        _fsm.Update();
        _UpdateLayersFromHistogram();
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

    private class UpdateDepth : BasicState
    {
        public override void Init()
        {
            name = "UpdateDepth";
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

    private class UpdateDanger : BasicState
    {
        float timer;
        public override void Init()
        {
            name = "UpdateDanger";
        }

        public override void OnEnter()
        {
            foreach (AudioHistogramElement theAHS in Context._elementList)
            {
                AudioSource thisAS;
                Context._tempoLoopedLayerSources.TryGetValue(theAHS, out thisAS);
                if (thisAS != null && theAHS.myEmoReg != MusicEmoReg.Danger)
                {
                    thisAS.volume = 0;
                }
            }
            timer = 0;
        }

        public override void Update()
        {
            foreach (AudioHistogramElement theAHS in Context._elementList)
            {
                AudioSource thisAS;
                Context._tempoLoopedLayerSources.TryGetValue(theAHS, out thisAS);
                if (thisAS != null && theAHS.myEmoReg != MusicEmoReg.Danger)
                {
                    thisAS.volume = Mathf.Lerp(
                        thisAS.volume,
                        Context._layerHistVal[(int)theAHS.myEmoReg] - theAHS.myLayerLowerThreshold / (theAHS.myLayerUpperThreshold - theAHS.myLayerLowerThreshold),
                        .05f);
                }
            }
            timer += Time.deltaTime;
            if (timer >= 5)
                TransitionTo<Standby>();
        }

        public override void OnExit()
        {

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
            if (a.loopedClip == true)
            {
                GameObject layer = (GameObject)Instantiate(Resources.Load("LoopedLayer"), transform);
                AudioSource myAS = layer.GetComponent<AudioSource>();
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
        _layerDecNormalizedTime[(int)MusicEmoReg.Creepy] = 3;
        _layerDecNormalizedTime[(int)MusicEmoReg.Danger] = 1;
    }

    private void _UpdateHistogram()
    {
        // Creepy and Danger wear off
        _layerHistVal[(int)MusicEmoReg.Creepy] = Mathf.MoveTowards(_layerHistVal[(int)MusicEmoReg.Creepy], 0, Time.deltaTime / _layerDecNormalizedTime[(int)MusicEmoReg.Creepy]);
        _layerHistVal[(int)MusicEmoReg.Danger] = Mathf.MoveTowards(_layerHistVal[(int)MusicEmoReg.Creepy], 0, Time.deltaTime / _layerDecNormalizedTime[(int)MusicEmoReg.Creepy]);

        // Clamps all values from 0 to 10
        for (int i = 0; i < System.Enum.GetValues(typeof(MusicEmoReg)).Length; i++)
        {
            _layerHistVal[i] = Mathf.Clamp(_layerHistVal[i], 0, 10);
        }
    }

    private void _UpdateLayersFromHistogram()
    {
        if (_layerHistVal[(int)MusicEmoReg.Danger] > 0)
        {
            if (((BasicState)_fsm.CurrentState).name != "UpdateDanger")
            {
                _fsm.TransitionTo<UpdateDanger>();
            }
        }
    }
}
