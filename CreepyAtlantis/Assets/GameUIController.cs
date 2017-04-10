using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

    public SelectionManager mySM;

    public Image[] Base = new Image[3];
    public Image[] Ch0 = new Image[3];
    public Image[] Ch1 = new Image[3];
    public Image[] Ch2 = new Image[3];
    public Image[] Field = new Image[4];

    private Color[] Base_Assigned = new Color[3];
    private Color[] Ch0_Assigned = new Color[3];
    private Color[] Ch1_Assigned = new Color[3];
    private Color[] Ch2_Assigned = new Color[3];
    private Color[] Field_Assigned = new Color[4];

    public Color frame_Off;
    public Color frame_On;
    public Color field_Off;
    public Color field_On;

    public Color ind_Off;

    public float rate;
    
    private void Awake()
    {
        mySM = GameObject.Find("GameStateManager").GetComponent<SelectionManager>();
    }

    private void Start()
    {
        for (int i = 0; i < Base.Length; i++)
        {
            Base_Assigned[i] = new Color(1, 1, 1, 0);
            Ch0_Assigned[i] = new Color(1, 1, 1, 0);
            Ch1_Assigned[i] = new Color(1, 1, 1, 0);
            Ch2_Assigned[i] = new Color(1, 1, 1, 0);
            Field_Assigned[i] = new Color(1, 1, 1, 0);
        }
        Field_Assigned[3] = new Color(1, 1, 1, 0);

        if (mySM.C1 == SelectChoice.Ops)
        {
            transform.Find("Overlays").Find("P1_Label").GetComponent<Text>().text = "CHANNEL 1: Ops";
            transform.Find("Overlays").Find("P2_Label").GetComponent<Text>().text = "CHANNEL 2: Doc";
        }
        else if (mySM.C1 == SelectChoice.Doc)
        {
            transform.Find("Overlays").Find("P1_Label").GetComponent<Text>().text = "CHANNEL 1: Doc";
            transform.Find("Overlays").Find("P2_Label").GetComponent<Text>().text = "CHANNEL 2: Ops";
        }
        else
        {
            transform.Find("Overlays").Find("P1_Label").GetComponent<Text>().text = "CHANNEL 1: N/A";
            transform.Find("Overlays").Find("P2_Label").GetComponent<Text>().text = "CHANNEL 2: N/A";
        }
    }

    public void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            Base[i].color = Color.Lerp(Base[i].color, Base_Assigned[i], Time.deltaTime);
            Ch0[i].color = Color.Lerp(Ch0[i].color, Ch0_Assigned[i], Time.deltaTime);
            Ch1[i].color = Color.Lerp(Ch1[i].color, Ch1_Assigned[i], Time.deltaTime);
            Ch2[i].color = Color.Lerp(Ch2[i].color, Ch2_Assigned[i], Time.deltaTime);
            Field[i].color = Color.Lerp(Field[i].color, Field_Assigned[i], Time.deltaTime);
        }
        Field[3].color = Color.Lerp(Field[3].color, Field_Assigned[3], Time.deltaTime);
    }

    public void DialogueFunction (Speaker channel, bool on)
    {
        if (on)
        {
            if (channel == Speaker.DANI)
                DANISpeak();
            else if (channel == Speaker.Ops)
                OpsSpeak();
            else
                DocSpeak();
        }
        else
        {
            if (channel == Speaker.DANI)
                DANIDone();
            else if (channel == Speaker.Ops)
                OpsDone();
            else
                DocDone();
        }

    }

    private void DANISpeak()
    {
        Ch0[1].color = GameObject.Find("GameStateManager").GetComponent<ColorManager>().DANI;
        Ch1[1].color = ind_Off;
        Ch2[1].color = ind_Off;

        Ch0_Assigned[1] = GameObject.Find("GameStateManager").GetComponent<ColorManager>().DANI;
        Ch1_Assigned[1] = ind_Off;
        Ch2_Assigned[1] = ind_Off;

        Base_Assigned[0] = frame_On;
        Base_Assigned[2] = field_On;

        Ch0_Assigned[0] = frame_On;
        Ch0_Assigned[2] = field_On;

        Field_Assigned[0] = frame_On;
        Field_Assigned[2] = field_On;
        Field_Assigned[3] = field_On;
    }
    private void DANIDone()
    {
        Ch0_Assigned[1] = ind_Off;
        Ch1_Assigned[1] = ind_Off;
        Ch2_Assigned[1] = ind_Off;

        Base_Assigned[0] = frame_Off;
        Base_Assigned[2] = field_Off;

        Ch0_Assigned[0] = frame_Off;
        Ch0_Assigned[2] = field_Off;

        Field_Assigned[0] = frame_Off;
        Field_Assigned[2] = field_Off;
        Field_Assigned[3] = field_Off;
    }

    private void DocSpeak()
    {
        if (mySM.C1 == SelectChoice.Ops)
        {
            Ch2[1].color = GameObject.Find("GameStateManager").GetComponent<ColorManager>().Doc;
            Ch0[1].color = ind_Off;
            Ch1[1].color = ind_Off;

            Ch2_Assigned[1] = GameObject.Find("GameStateManager").GetComponent<ColorManager>().Doc;
            Ch0_Assigned[1] = ind_Off;
            Ch1_Assigned[1] = ind_Off;

            Ch2_Assigned[0] = frame_On;
            Ch2_Assigned[2] = field_On;
        }
        else
        {
            Ch1[1].color = GameObject.Find("GameStateManager").GetComponent<ColorManager>().Doc;
            Ch0[1].color = ind_Off;
            Ch2[1].color = ind_Off;

            Ch1_Assigned[1] = GameObject.Find("GameStateManager").GetComponent<ColorManager>().Doc;
            Ch0_Assigned[1] = ind_Off;
            Ch2_Assigned[1] = ind_Off;

            Ch1_Assigned[0] = frame_On;
            Ch1_Assigned[2] = field_On;
        }

        Base_Assigned[0] = frame_On;
        Base_Assigned[2] = field_On;

        Field_Assigned[0] = frame_On;
        Field_Assigned[2] = field_On;
        Field_Assigned[3] = field_On;
    }
    private void DocDone()
    {
        Ch0_Assigned[1] = ind_Off;
        Ch1_Assigned[1] = ind_Off;
        Ch2_Assigned[1] = ind_Off;

        Base_Assigned[0] = frame_Off;
        Base_Assigned[2] = field_Off;

        Ch1_Assigned[0] = frame_Off;
        Ch1_Assigned[2] = field_Off;
        Ch2_Assigned[0] = frame_Off;
        Ch2_Assigned[2] = field_Off;

        Field_Assigned[0] = frame_Off;
        Field_Assigned[2] = field_Off;
        Field_Assigned[3] = field_Off;
    }

    private void OpsSpeak()
    {
        if (mySM.C1 == SelectChoice.Ops)
        {
            Ch1[1].color = GameObject.Find("GameStateManager").GetComponent<ColorManager>().Ops;
            Ch0[1].color = ind_Off;
            Ch2[1].color = ind_Off;

            Ch1_Assigned[1] = GameObject.Find("GameStateManager").GetComponent<ColorManager>().Ops;
            Ch0_Assigned[1] = ind_Off;
            Ch2_Assigned[1] = ind_Off;

            Ch1_Assigned[0] = frame_On;
            Ch1_Assigned[2] = field_On;
        }
        else
        {
            Ch2[1].color = GameObject.Find("GameStateManager").GetComponent<ColorManager>().Ops;
            Ch0[1].color = ind_Off;
            Ch1[1].color = ind_Off;

            Ch2_Assigned[1] = GameObject.Find("GameStateManager").GetComponent<ColorManager>().Ops;
            Ch0_Assigned[1] = ind_Off;
            Ch1_Assigned[1] = ind_Off;

            Ch2_Assigned[0] = frame_On;
            Ch2_Assigned[2] = field_On;
        }

        Base_Assigned[0] = frame_On;
        Base_Assigned[2] = field_On;

        Field_Assigned[0] = frame_On;
        Field_Assigned[2] = field_On;
        Field_Assigned[3] = field_On;
    }
    private void OpsDone()
    {
        Ch0_Assigned[1] = ind_Off;
        Ch1_Assigned[1] = ind_Off;
        Ch2_Assigned[1] = ind_Off;

        Base_Assigned[0] = frame_Off;
        Base_Assigned[2] = field_Off;

        Ch1_Assigned[0] = frame_Off;
        Ch1_Assigned[2] = field_Off;
        Ch2_Assigned[0] = frame_Off;
        Ch2_Assigned[2] = field_Off;

        Field_Assigned[0] = frame_Off;
        Field_Assigned[2] = field_Off;
        Field_Assigned[3] = field_Off;
    }

    //private IEnumerator _BaseOn()
    //{
    //    while(true)
    //    {
    //        Base[0].color = Color.Lerp(Base[0].color, frame_On, rate * Time.deltaTime);
    //        Base[2].color = Color.Lerp(Base[2].color, field_On, rate * Time.deltaTime);

    //        if (Mathf.Abs(Base[2].color.a - field_On.a) < .01f)
    //        {
    //            Base[0].color = frame_On;
    //            Base[2].color = field_On;
    //            yield break;
    //        }

    //        yield return null;
    //    }
    //}
    //private IEnumerator _Ch0On()
    //{
    //    Ch0[1].color = GameObject.Find("GameStateManager").GetComponent<ColorManager>().DANI;
    //    Ch1[1].color = ind_Off;
    //    Ch2[1].color = ind_Off;

    //    Field[1].color = GameObject.Find("GameStateManager").GetComponent<ColorManager>().DANI;
    //    while (true)
    //    {
    //        Ch0[0].color = Color.Lerp(Ch0[0].color, frame_On, rate * Time.deltaTime);
    //        Ch0[2].color = Color.Lerp(Ch0[2].color, field_On, rate * Time.deltaTime);

    //        if (Mathf.Abs(Ch0[2].color.a - field_On.a) < .01f)
    //        {
    //            Ch0[0].color = frame_On;
    //            Ch0[2].color = field_On;
    //            yield break;
    //        }

    //        yield return null;
    //    }
    //}
    //private IEnumerator _Ch1On()
    //{
    //    Ch1[1].color = GameObject.Find("GameStateManager").GetComponent<ColorManager>().Ops;

    //    Ch0[1].color = ind_Off;
    //    Ch2[1].color = ind_Off;

    //    Field[1].color = GameObject.Find("GameStateManager").GetComponent<ColorManager>().Ops;
    //    while (true)
    //    {
    //        Ch1[0].color = Color.Lerp(Ch1[0].color, frame_On, rate * Time.deltaTime);
    //        Ch1[2].color = Color.Lerp(Ch1[2].color, field_On, rate * Time.deltaTime);

    //        if (Mathf.Abs(Ch1[2].color.a - field_On.a) < .01f)
    //        {
    //            Ch1[0].color = frame_On;
    //            Ch1[2].color = field_On;
    //            yield break;
    //        }

    //        yield return null;
    //    }
    //}
    //private IEnumerator _Ch2On()
    //{
    //    Ch2[1].color = GameObject.Find("GameStateManager").GetComponent<ColorManager>().Doc;

    //    Ch0[1].color = ind_Off;
    //    Ch1[1].color = ind_Off;

    //    Field[1].color = GameObject.Find("GameStateManager").GetComponent<ColorManager>().Doc;
    //    while (true)
    //    {
    //        Ch2[0].color = Color.Lerp(Ch2[0].color, frame_On, rate * Time.deltaTime);
    //        Ch2[2].color = Color.Lerp(Ch2[2].color, field_On, rate * Time.deltaTime);

    //        if (Mathf.Abs(Ch2[2].color.a - field_On.a) < .01f)
    //        {
    //            Ch2[0].color = frame_On;
    //            Ch2[2].color = field_On;
    //            yield break;
    //        }

    //        yield return null;
    //    }
    //}
    //private IEnumerator _FieldOn()
    //{
    //    while (true)
    //    {
    //        Field[0].color = Color.Lerp(Field[0].color, frame_On, rate * Time.deltaTime);
    //        Field[2].color = Color.Lerp(Field[2].color, field_On, rate * Time.deltaTime);
    //        Field[3].color = Color.Lerp(Field[3].color, field_On, rate * Time.deltaTime);

    //        if (Mathf.Abs(Field[2].color.a - field_On.a) < .01f)
    //        {
    //            Field[0].color = frame_On;
    //            Field[2].color = field_On;
    //            Field[3].color = field_On;

    //            yield break;
    //        }

    //        yield return null;
    //    }
    //}

    //private IEnumerator _BaseOff()
    //{
    //    while (true)
    //    {
    //        Base[0].color = Color.Lerp(Base[0].color, frame_Off, rate * Time.deltaTime);
    //        Base[2].color = Color.Lerp(Base[2].color, field_Off, rate * Time.deltaTime);

    //        if (Mathf.Abs(Base[2].color.a - field_Off.a) < .01f)
    //        {
    //            Base[0].color = frame_Off;
    //            Base[2].color = field_Off;
    //            yield break;
    //        }

    //        yield return null;
    //    }
    //}
    //private IEnumerator _Ch0Off()
    //{
    //    Ch0[1].color = ind_Off;
    //    Ch1[1].color = ind_Off;
    //    Ch2[1].color = ind_Off;

    //    Field[1].color = ind_Off;
    //    while (true)
    //    {
    //        Ch0[0].color = Color.Lerp(Ch0[0].color, frame_Off, rate * Time.deltaTime);
    //        Ch0[2].color = Color.Lerp(Ch0[2].color, field_Off, rate * Time.deltaTime);

    //        if (Mathf.Abs(Ch0[2].color.a - field_Off.a) < .01f)
    //        {
    //            Ch0[0].color = frame_Off;
    //            Ch0[2].color = field_Off;
    //            yield break;
    //        }

    //        yield return null;
    //    }
    //}
    //private IEnumerator _Ch1Off()
    //{
    //    Ch0[1].color = ind_Off;
    //    Ch1[1].color = ind_Off;
    //    Ch2[1].color = ind_Off;

    //    Field[1].color = ind_Off;
    //    while (true)
    //    {
    //        Ch1[0].color = Color.Lerp(Ch1[0].color, frame_Off, rate * Time.deltaTime);
    //        Ch1[2].color = Color.Lerp(Ch1[2].color, field_Off, rate * Time.deltaTime);

    //        if (Mathf.Abs(Ch1[2].color.a - field_Off.a) < .01f)
    //        {
    //            Ch1[0].color = frame_Off;
    //            Ch1[2].color = field_Off;
    //            yield break;
    //        }

    //        yield return null;
    //    }
    //}
    //private IEnumerator _Ch2Off()
    //{
    //    Ch0[1].color = ind_Off;
    //    Ch1[1].color = ind_Off;
    //    Ch2[1].color = ind_Off;

    //    Field[1].color = ind_Off;
    //    while (true)
    //    {
    //        Ch2[0].color = Color.Lerp(Ch2[0].color, frame_Off, rate * Time.deltaTime);
    //        Ch2[2].color = Color.Lerp(Ch2[2].color, field_Off, rate * Time.deltaTime);

    //        if (Mathf.Abs(Ch2[2].color.a - field_Off.a) < .01f)
    //        {
    //            Ch2[0].color = frame_Off;
    //            Ch2[2].color = field_Off;
    //            yield break;
    //        }

    //        yield return null;
    //    }
    //}
    //private IEnumerator _FieldOff()
    //{
    //    while (true)
    //    {
    //        Field[0].color = Color.Lerp(Field[0].color, frame_Off, rate * Time.deltaTime);
    //        Field[2].color = Color.Lerp(Field[2].color, field_Off, rate * Time.deltaTime);
    //        Field[3].color = Color.Lerp(Field[3].color, field_Off, rate * Time.deltaTime);

    //        if (Mathf.Abs(Field[2].color.a - field_Off.a) < .01f)
    //        {
    //            Field[0].color = frame_Off;
    //            Field[2].color = field_Off;
    //            Field[3].color = field_Off;

    //            yield break;
    //        }

    //        yield return null;
    //    }
    //}
}
