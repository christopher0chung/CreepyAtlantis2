using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DialogueLineAction { Play, Stop}
public class GE_Dia_Line : GameEvent
{
    public Speaker speaker;
    public string line;
    public DialogueLineAction action;
    public string audioFileName;

    public GE_Dia_Line (Speaker s, string l, DialogueLineAction a, string n)
    {
        speaker = s;
        line = l;
        action = a;
        audioFileName = n;
    }
}

public enum DialogueEventStatus {Standby, Active, Complete }
public class GE_Dia_Event : GameEvent
{
    public DialogueEventStatus status;
    public string eventName;

    public GE_Dia_Event (DialogueEventStatus s, string n)
    {
        status = s;
        eventName = n;
    }
}

public enum DialogueLineStatus { Active, Complete}
public class GE_DialogueManager_Status : GameEvent
{
    public DialogueLineStatus status;
    public string audioFileName;

    public GE_DialogueManager_Status (DialogueLineStatus s, string n)
    {
        status = s;
        audioFileName = n;
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
    #endregion

    #region Mono Functions
    void Start () {
        myAS = GetComponent<AudioSource>();

        _fsm = new FSM<Deeper_DialogueManager>(this);
        _fsm.TransitionTo<Standby>();

        //if(GameObject.Find("GameStateManagers").GetComponent<SelectionManager>().C1 == SelectChoice.Ops)
        //{
        //    _speakerIndex.Add(Speaker.Ops, 0);
        //    _speakerIndex.Add(Speaker.Doc, 2);
        //}
        //else
        //{
            _speakerIndex.Add(Speaker.Doc, 0);
            _speakerIndex.Add(Speaker.Ops, 2);
        //}
        _speakerIndex.Add(Speaker.DANI, 1);
        EventManager.instance.Register<GE_Dia_Line>(EventFunc);
        EventManager.instance.Register<Button_GE>(EventFunc);
	}

    void Update()
    {
        _fsm.Update();

        if(Input.GetKeyDown(KeyCode.Return))
        {
            EventManager.instance.Fire(new GE_Dia_Line((Speaker)Random.Range(0, 3), "Look at me, I'm DANI!", DialogueLineAction.Play, "AALERT1-XXXX-DAN"));
        }
    }
    #endregion

    #region Event Related Functions
    void EventFunc (GameEvent e)
    {
        if (e.GetType() == typeof(GE_Dia_Line))
        {
            GE_Dia_Line d = (GE_Dia_Line)e;

            if (d.action == DialogueLineAction.Play)
            {
                currentActiveLine = d.line;
                currentActiveSpeaker = d.speaker;
                currentActiveAudioFile = d.audioFileName;
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

            theLine = Context.currentActiveLine;

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
            EventManager.instance.Fire(new GE_DialogueManager_Status(DialogueLineStatus.Complete, Context.myAS.clip.name));
        }
    }
    #endregion
}
