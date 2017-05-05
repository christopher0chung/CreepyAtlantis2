using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DialogueLinePriority { Normal, Interrupt}
public class GE_Dia_Line : GameEvent
{
    public Speaker speaker;
    public string line;
    public DialogueLinePriority priority;
    public string audioFileName;

    public GE_Dia_Line (Speaker s, string l, DialogueLinePriority p, string n)
    {
        speaker = s;
        line = l;
        priority = p;
        audioFileName = n;
    }
}

public enum DialogueStatus { Start, Complete}
public class GE_UI_Dia : GameEvent
{
    public Speaker speaker;
    public DialogueStatus status;

    public GE_UI_Dia (Speaker s, DialogueStatus dS)
    {
        speaker = s;
        status = dS;
    }
}


[RequireComponent (typeof(AudioSource))]
public class Deeper_DialogueManager : MonoBehaviour {

    #region UI Targets
    [Header ("Length must be 3")]
    [SerializeField] private Text[] TextBoxes = new Text[3];
    [SerializeField] private Image[] TextBorders = new Image[3];
    #endregion

    #region Internal Convenience Variables
    private Dictionary<Speaker, int> _speakerIndex = new Dictionary<Speaker, int>();
    #endregion

    #region Finite State Machine Related References
    private FSM<Deeper_DialogueManager> _fsm;
    #endregion

    #region Internal Functional Variables
    private string currentActiveLine;
    private Speaker currentActiveSpeaker;
    private string currentActiveAudioFile;

    private AudioSource myAS;

    private List<GE_Dia_Line> queuedLines = new List<GE_Dia_Line>();
    #endregion
 

    #region Mono Functions
    void Start () {
        myAS = GetComponent<AudioSource>();

        _fsm = new FSM<Deeper_DialogueManager>(this);
        _fsm.TransitionTo<Standby>();

        if (GameObject.Find("GameStateManagers").GetComponent<SelectionManager>().C1 == SelectChoice.Ops)
        {
            _speakerIndex.Add(Speaker.Ops, 0);
            _speakerIndex.Add(Speaker.Doc, 2);
        }
        else
        {
            _speakerIndex.Add(Speaker.Doc, 0);
            _speakerIndex.Add(Speaker.Ops, 2);
        }
        _speakerIndex.Add(Speaker.DANI, 1);
        EventManager.instance.Register<GE_Dia_Line>(EventFunc);
        EventManager.instance.Register<Button_GE>(EventFunc);
	}

    void Update()
    {
        // when queued lines has something in it, start playing the line 
        if (queuedLines.Count > 0 && !(_fsm.CurrentState.GetType() == typeof(PrintStart) || _fsm.CurrentState.GetType() == typeof(PrintComplete)))
        {
            _fsm.TransitionTo<PrintStart>();
        }
        _fsm.Update();
    }
    #endregion

    #region Event Related Functions
    void EventFunc (GameEvent e)
    {
        if (e.GetType() == typeof(GE_Dia_Line))
        {
            GE_Dia_Line d = (GE_Dia_Line)e;

            if (d.priority == DialogueLinePriority.Normal)
            {
                queuedLines.Add(d);
            }
            else if (d.priority == DialogueLinePriority.Interrupt)
            {
                queuedLines.Insert(0, d);
                _fsm.TransitionTo<PrintStart>();
            }
        }
        else if (e.GetType() == typeof(Button_GE))
        {
            Button_GE b = (Button_GE)e;
            {
                if (b.button == Button.Dialogue)
                {
                    if (_fsm.CurrentState.GetType() == typeof(PrintStart))
                    {
                        _fsm.TransitionTo<PrintComplete>();
                    }
                    else if (_fsm.CurrentState.GetType() == typeof(PrintComplete))
                    {
                        _fsm.TransitionTo<Standby>();
                    }
                }
            }
        }
    }
    #endregion

    #region Finite State Machine States
    private class BasicState : FSM<Deeper_DialogueManager>.State { }

    private class Standby: BasicState
    {
        public override void Init()
        {
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

    private class PrintStart: BasicState
    {
        private Text theText;
        private string theLine;

        private float timer;
        private float audioLength;

        public override void Init()
        {
        }

        public override void OnEnter()
        {
            Context.myAS.Stop();

            Context.currentActiveLine = Context.queuedLines[0].line;
            Context.currentActiveSpeaker = Context.queuedLines[0].speaker;
            Context.currentActiveAudioFile = Context.queuedLines[0].audioFileName;

            theLine = Context.currentActiveLine;

            EventManager.instance.Fire(new GE_UI_Dia(Context.currentActiveSpeaker, DialogueStatus.Start));

            for (int i = 0; i < 3; i++)
            {
                Context.TextBoxes[i].text = "";
            }

            int outputIndex;
            Context._speakerIndex.TryGetValue(Context.currentActiveSpeaker, out outputIndex);
            theText = Context.TextBoxes[outputIndex];

            Context.myAS.clip = (AudioClip)Resources.Load("Dialogue/" + Context.currentActiveAudioFile);
            audioLength = Context.myAS.clip.length;
            timer = 0;
            Context.myAS.Play();
        }

        public override void Update()
        {
            timer += Time.deltaTime;
            if (timer / audioLength >= 1)
            {
                TransitionTo<PrintComplete>();
                return;
            }
            theText.text = theLine.Substring(0, (int)((theLine.Length - 1) * timer / audioLength));
        }

        public override void OnExit()
        {
            theText.text = theLine;
        }
    }

    private class PrintComplete : BasicState
    {
        private float timeRemaining;
        private float timer;
        private Text theText;

        public override void Init()
        {
        }

        public override void OnEnter()
        {
            timeRemaining = Context.myAS.clip.length - Context.myAS.time + .5f;
            timer = 0;

            int outputIndex;
            Context._speakerIndex.TryGetValue(Context.currentActiveSpeaker, out outputIndex);
            theText = Context.TextBoxes[outputIndex];
        }

        public override void Update()
        {
            timer += Time.deltaTime;
            if (timer >= timeRemaining)
                TransitionTo<Standby>();
        }

        public override void OnExit()
        {
            theText.text = "";
            Context.myAS.Stop();
            Context.queuedLines.RemoveAt(0);
            EventManager.instance.Fire(new GE_UI_Dia(Context.currentActiveSpeaker, DialogueStatus.Complete));
        }
    }
    #endregion
}
