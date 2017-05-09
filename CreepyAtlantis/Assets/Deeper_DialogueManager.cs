using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DialogueLineType { Standard, Choice }
public enum DialogueLinePriority { Normal, Interrupt }
public enum DialogueLineTag { First, Middle, Last, FirstAndLast }
public class GE_Dia_Line : GameEvent
{
    public DialogueLineType type;
    public DialogueLinePriority priority;
    public DialogueLineTag tag;

    public Speaker speaker;
    public string description;

    public string line;
    public string audioFileName;
    public Deeper_DialogueEvent followOnEvent;

    public string choice1;
    public string choice2;
    public Deeper_DialogueEvent choice1Event;
    public Deeper_DialogueEvent choice2Event;


    public GE_Dia_Line (DialogueLinePriority p, DialogueLineTag t, Speaker s, string d, string l, string n, Deeper_DialogueEvent f)
    {
        type = DialogueLineType.Standard;
        priority = p;
        tag = t;
        speaker = s;
        description = d;
        line = l;
        audioFileName = n;
        followOnEvent = f;
    }

    public GE_Dia_Line (DialogueLinePriority p, DialogueLineTag t, Speaker s, string d, string c1, string c2, Deeper_DialogueEvent c1E, Deeper_DialogueEvent c2E)
    {
        type = DialogueLineType.Choice;
        priority = p;
        tag = t;
        speaker = s;
        description = d;
        choice1 = c1;
        choice2 = c2;
        choice1Event = c1E;
        choice2Event = c2E;
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

public enum DialogueInQueueStatus { Waiting, Expired }
public class GE_UI_LineInQueue
{
    public DialogueInQueueStatus status;
    public GE_UI_LineInQueue (DialogueInQueueStatus s)
    {
        status = s;
    }
}


//---------------------------------------------------------------------------------------------------------------------------------
// When an DialogueEvent fires, it gets added to the list of dialogue lines in chronological order as determined by the event.
// Normal priority DEs will get added to the end of the queue, and will require a prompt to progress.
// Interrupt priority DEs will get pushed to the front, stop the current dialogue line, and start playing.
//   To resume, players will have to hit the Dialogue Button (listening to the event).
// DialogueEvents that aren't started before an expiration occurs will be discarded.
//---------------------------------------------------------------------------------------------------------------------------------
[RequireComponent (typeof(AudioSource))]
public class Deeper_DialogueManager : MonoBehaviour {

    #region UI Targets
    [Header ("Length must be 3")]
    [SerializeField] private Text[] TextBoxes = new Text[3];
    [SerializeField] private Text[] ChoiceBoxes = new Text[4];
    [SerializeField] private Image[] TextBorders = new Image[3];
    [SerializeField] private Text DescriptionBox;
    #endregion

    #region Internal Convenience Variables
    private Dictionary<Speaker, Text> _standardSpeakerRef = new Dictionary<Speaker, Text>();
    private Dictionary<Speaker, Text[]> _choiceSpeakerRefs = new Dictionary<Speaker, Text[]>();
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
    void Awake()
    {
        EventManager.instance.Register<GE_Dia_Line>(EventFunc);
        EventManager.instance.Register<Button_GE>(EventFunc);
        DontDestroyOnLoad(this.gameObject);
    }

    void Start () {
        myAS = GetComponent<AudioSource>();

        _fsm = new FSM<Deeper_DialogueManager>(this);
        _fsm.TransitionTo<Standby>();

        if (GameObject.Find("GameStateManager").GetComponent<SelectionManager>().C1 == SelectChoice.Ops)
        {
            _standardSpeakerRef.Add(Speaker.Ops, TextBoxes[0]);
            _standardSpeakerRef.Add(Speaker.Doc, TextBoxes[2]);

            Text[] leftFields = new Text[2];
            leftFields[0] = ChoiceBoxes[0];
            leftFields[1] = ChoiceBoxes[1];
            Text[] rightFields = new Text[2];
            rightFields[0] = ChoiceBoxes[2];
            rightFields[1] = ChoiceBoxes[3];

            _choiceSpeakerRefs.Add(Speaker.Ops, leftFields);
            _choiceSpeakerRefs.Add(Speaker.Doc, rightFields);
        }
        else
        {
            _standardSpeakerRef.Add(Speaker.Ops, TextBoxes[2]);
            _standardSpeakerRef.Add(Speaker.Doc, TextBoxes[0]);

            Text[] leftFields = new Text[2];
            leftFields[0] = ChoiceBoxes[0];
            leftFields[1] = ChoiceBoxes[1];
            Text[] rightFields = new Text[2];
            rightFields[0] = ChoiceBoxes[2];
            rightFields[1] = ChoiceBoxes[3];

            _choiceSpeakerRefs.Add(Speaker.Ops, rightFields);
            _choiceSpeakerRefs.Add(Speaker.Doc, leftFields);
        }
        _standardSpeakerRef.Add(Speaker.DANI, TextBoxes[1]);
	}

    void Update()
    {
        _LinesParser();
        _fsm.Update();
    }
    #endregion

    #region Internal Context Functions
    void _LinesParser()
    {
        // when queued lines has something in it, start playing the line 
        if (queuedLines.Count > 0 && _fsm.CurrentState.GetType() == typeof(Standby))
        {
            if (queuedLines[0].type == DialogueLineType.Standard && queuedLines[0].tag != DialogueLineTag.First && queuedLines[0].tag != DialogueLineTag.FirstAndLast)
            {
                _fsm.TransitionTo<PrintStart>();
            }
            else if (queuedLines[0].type == DialogueLineType.Choice && queuedLines[0].tag != DialogueLineTag.First && queuedLines[0].tag != DialogueLineTag.FirstAndLast)
            {
                _fsm.TransitionTo<ChoiceState>();
            }
            DescriptionBox.text = queuedLines[0].description;
        }
        else if (queuedLines.Count > 0 && (_fsm.CurrentState.GetType() == typeof(PrintStart) || _fsm.CurrentState.GetType() == typeof(PrintComplete)))
        {
            DescriptionBox.text = "Press Y to advance";
        }
        else
        {
            DescriptionBox.text = "";
        }
    }

    void _HardInitialize()
    {
        //clear things out
        myAS.Stop();

        foreach (Text t in TextBoxes)
        {
            t.text = "";
        }
        foreach (Text t in ChoiceBoxes)
        {
            t.text = "";
        }
        //initialize what referenced as active
        currentActiveLine = queuedLines[0].line;
        currentActiveSpeaker = queuedLines[0].speaker;
        currentActiveAudioFile = queuedLines[0].audioFileName;

        //indicates to UI that someone is talking
        EventManager.instance.Fire(new GE_UI_Dia(currentActiveSpeaker, DialogueStatus.Start));
    }

    void _SoftInitialize()
    {
        //clear things out
        myAS.Stop();

        foreach (Text t in TextBoxes)
        {
            t.text = "";
        }
        foreach (Text t in ChoiceBoxes)
        {
            t.text = "";
        }
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
                if (b.button == Button.Dialogue && b.pressedReleased)
                {
                    //------------------------------------
                    // if lines are playing, advance
                    //------------------------------------
                    if (_fsm.CurrentState.GetType() == typeof(PrintStart))
                    {
                        _fsm.TransitionTo<PrintComplete>();
                    }
                    else if (_fsm.CurrentState.GetType() == typeof(PrintComplete))
                    {
                        _fsm.TransitionTo<Standby>();
                    }
                    //------------------------------------
                    // if a new event's worth of dialogue is in the queue, start it in the appropriate way
                    //------------------------------------
                    if (queuedLines.Count > 0 && _fsm.CurrentState.GetType() == typeof(Standby))
                    {
                        if (queuedLines[0].tag == DialogueLineTag.First || queuedLines[0].tag == DialogueLineTag.FirstAndLast)
                        {
                            if (queuedLines[0].type == DialogueLineType.Standard)
                            {
                                _fsm.TransitionTo<PrintStart>();
                            }
                            else if (queuedLines[0].type == DialogueLineType.Choice)
                            {
                                _fsm.TransitionTo<ChoiceState>();
                            }
                        }
                    }
                }
                else if (b.button == Button.Choice1 && b.pressedReleased)
                {
                    if(_fsm.CurrentState.GetType() == typeof(ChoiceState))
                    {
                        queuedLines[0].choice1Event.Fire();
                        _fsm.TransitionTo<Standby>();
                    }
                }
                else if (b.button == Button.Choice2 && b.pressedReleased)
                {
                    if (_fsm.CurrentState.GetType() == typeof(ChoiceState))
                    {
                        queuedLines[0].choice2Event.Fire();
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
            if (Context.queuedLines.Count > 0)
                Context.queuedLines.RemoveAt(0);
            EventManager.instance.Fire(new GE_UI_Dia(Context.currentActiveSpeaker, DialogueStatus.Complete));
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
            Context._HardInitialize();
            //get or reset internal variables
            Context._standardSpeakerRef.TryGetValue(Context.currentActiveSpeaker, out theText);

            theLine = Context.currentActiveLine;
            if (Context.myAS.clip != null)
                audioLength = Context.myAS.clip.length;
            else
                audioLength = 5;
            timer = 0;

            //start audio
            Context.myAS.clip = (AudioClip)Resources.Load("Dialogue/" + Context.currentActiveAudioFile);
            Context.myAS.Play();
        }

        public override void Update()
        {
            Debug.Log("In PrintStart");

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
            //finish out the line
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
            //get or reset internal variables
            Context._standardSpeakerRef.TryGetValue(Context.currentActiveSpeaker, out theText);
            if (Context.myAS.clip != null)
                timeRemaining = Context.myAS.clip.length - Context.myAS.time;
            else
                timeRemaining = 5;
            timer = 0;
        }

        public override void Update()
        {
            Debug.Log("In PrintComplete");

            timer += Time.deltaTime;
            if (timer >= timeRemaining)
                TransitionTo<Standby>();
        }

        public override void OnExit()
        {
            Context._SoftInitialize();
        }
    }

    private class ChoiceState : BasicState
    {
        private Text choice1Field;
        private Text choice2Field;

        public override void Init()
        {
        }

        public override void OnEnter()
        {
            Context._HardInitialize();

            Text[] fields;
            Context._choiceSpeakerRefs.TryGetValue(Context.currentActiveSpeaker, out fields);
            choice1Field = fields[0];
            choice2Field = fields[1];

            EventManager.instance.Fire(new GE_UI_Dia(Context.currentActiveSpeaker, DialogueStatus.Start));

            foreach (Text t in Context.TextBoxes)
            {
                t.text = "";
            }
            foreach (Text t in Context.ChoiceBoxes)
            {
                t.text = "";
            }
        }

        public override void Update()
        {
            Debug.Log("In Choice");

        }

        public override void OnExit()
        {
            Context._SoftInitialize();
        }
    }
    #endregion
}