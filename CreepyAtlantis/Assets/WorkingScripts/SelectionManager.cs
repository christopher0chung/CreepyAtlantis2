using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour {

    [SerializeField] private SelectChoice C1Select;
    public SelectChoice C1
    {
        get { return C1Select; }
    }
    [SerializeField] private SelectChoice C2Select;
    public SelectChoice C2
    {
        get { return C2Select; }
    }

    [SerializeField] private Image c1;
    [SerializeField] private Image c2;
    [SerializeField] private Text c1Label;
    [SerializeField] private Text c2Label;
    [SerializeField] private Text ops;
    [SerializeField] private Text doc;
    [SerializeField] private Image ind1;
    [SerializeField] private Image ind2;


    private ColorManager myCM;

    private bool _c1Confirm;
    private bool c1Confirm
    {
        get
        {
            return _c1Confirm;
        }
        set
        {
            if (value != _c1Confirm)
            {
                _c1Confirm = value;
                if (_c1Confirm)
                {
                    if (C1Select == SelectChoice.Ops)
                        c1Label.color = new Color(myCM.Ops.r, myCM.Ops.g, myCM.Ops.b, c1Label.color.a);
                    if (C1Select == SelectChoice.Doc)
                        c1Label.color = new Color(myCM.Doc.r, myCM.Doc.g, myCM.Doc.b, c1Label.color.a);
                    if (c2Confirm && C1Select != C2Select && C1Select != SelectChoice.None && C2Select != SelectChoice.None)
                    {
                        GameObject.FindGameObjectWithTag("Managers").GetComponent<LevelLoader>().LoadLevel(2);

                        if (C1Select == SelectChoice.Doc && C2Select == SelectChoice.Ops)
                        {
                            GameObject p0 = GameObject.FindGameObjectWithTag("Player0");
                            GameObject p1 = GameObject.FindGameObjectWithTag("Player1");
                            p0.tag = "Player1";
                            p0.GetComponent<MultiplayerWithBindingsExample.Player>().playerNum = 1;
                            p1.tag = "Player0";
                            p1.GetComponent<MultiplayerWithBindingsExample.Player>().playerNum = 0;
                        }
                    }
                }
                else
                {
                    c1Label.color = Color.white;
                }
            }
        }
    }

    private bool _c2Confirm;
    private bool c2Confirm
    {
        get
        {
            return _c2Confirm;
        }
        set
        {
            if (value != _c2Confirm)
            {
                _c2Confirm = value;
                if (_c2Confirm)
                {
                    if (C2Select == SelectChoice.Ops)
                        c2Label.color = new Color(myCM.Ops.r, myCM.Ops.g, myCM.Ops.b, c2Label.color.a);
                    if (C2Select == SelectChoice.Doc)
                        c2Label.color = new Color(myCM.Doc.r, myCM.Doc.g, myCM.Doc.b, c2Label.color.a);
                    if (c1Confirm && C1Select != C2Select && C1Select != SelectChoice.None && C2Select != SelectChoice.None)
                    {
                        GameObject.FindGameObjectWithTag("Managers").GetComponent<LevelLoader>().LoadLevel(2);

                        if (C1Select == SelectChoice.Doc && C2Select == SelectChoice.Ops)
                        {
                            GameObject p0 = GameObject.FindGameObjectWithTag("Player0");
                            GameObject p1 = GameObject.FindGameObjectWithTag("Player1");
                            p0.tag = "Player1";
                            p0.GetComponent<MultiplayerWithBindingsExample.Player>().playerNum = 1;
                            p1.tag = "Player0";
                            p1.GetComponent<MultiplayerWithBindingsExample.Player>().playerNum = 0;
                        }
                    }
                }
                else
                {
                    c2Label.color = Color.white;
                }
            }
        }
    }

    void Awake ()
    {
        SceneManager.sceneLoaded += Init;
    }

    public void Init(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "ControllerCharacterHookup")
        {
            //Debug.Log("Ran Init in SelMan");
            myCM = GameObject.FindGameObjectWithTag("Managers").GetComponent<ColorManager>();

            c1 = GameObject.Find("Controller1").GetComponent<Image>();
            c2 = GameObject.Find("Controller2").GetComponent<Image>();

            ops = GameObject.Find("OpsName").GetComponent<Text>();
            doc = GameObject.Find("DocName").GetComponent<Text>();

            c1Label = GameObject.Find("Controller1").GetComponentInChildren<Text>();
            c2Label = GameObject.Find("Controller2").GetComponentInChildren<Text>();

            //ind1 = GameObject.Find("Channel1Tab").transform.GetChild(2).GetComponent<Image>();
            //ind2 = GameObject.Find("Channel2Tab").transform.GetChild(2).GetComponent<Image>();

            ind1 = GameObject.Find("Ch1Ind").GetComponent<Image>();
            ind2 = GameObject.Find("Ch2Ind").GetComponent<Image>();
        }
    }

    public void UpdateSelection(SelectChoice mySC, int pNum)
    {
        if (pNum == 0)
        {
            if (mySC != C1Select)
                c1Confirm = false;
            C1Select = mySC;
        }

        if (pNum == 1)
        {
            if (mySC != C2Select)
                c2Confirm = false;
            C2Select = mySC;
        }

        ControllerColor();
        NameColor();
    }

    public void ConfirmSelect(int pNum)
    {
       if (pNum == 0 && C1Select != SelectChoice.None)
        {
            c1Confirm = true;
        }
       if (pNum == 1 && C2Select != SelectChoice.None)
        {
            c2Confirm = true;
        }
    }

    private void ControllerColor()
    {
        if (C1Select == SelectChoice.Ops)
        {
            c1.color = new Color(myCM.Ops.r, myCM.Ops.g, myCM.Ops.b, c1.color.a);
        }
        else if (C1Select == SelectChoice.None)
        {
            c1.color = new Color(myCM.DANI.r, myCM.DANI.g, myCM.DANI.b, c1.color.a);
        }
        else if (C1Select == SelectChoice.Doc)
        {
            c1.color = new Color(myCM.Doc.r, myCM.Doc.g, myCM.Doc.b, c1.color.a);
        }

        if (C2Select == SelectChoice.Ops)
        {
            c2.color = new Color(myCM.Ops.r, myCM.Ops.g, myCM.Ops.b, c2.color.a);
        }
        else if (C2Select == SelectChoice.None)
        {
            c2.color = new Color(myCM.DANI.r, myCM.DANI.g, myCM.DANI.b, c2.color.a);
        }
        else if (C2Select == SelectChoice.Doc)
        {
            c2.color = new Color(myCM.Doc.r, myCM.Doc.g, myCM.Doc.b, c2.color.a);
        }

        ind1.color = c1.color;
        ind2.color = c2.color;
    }

    private void NameColor ()
    {
        if (C1Select == C2Select)
        {
            ops.color = new Color(myCM.DANI.r, myCM.DANI.g, myCM.DANI.b, ops.color.a);
            doc.color = new Color(myCM.DANI.r, myCM.DANI.g, myCM.DANI.b, doc.color.a);
            //Debug.Log("C1 = C2");
        }
        if (C1Select != SelectChoice.Ops && C2Select != SelectChoice.Ops)
        {
            ops.color = new Color(myCM.DANI.r, myCM.DANI.g, myCM.DANI.b, ops.color.a);
            //Debug.Log("C1 and C2 are both not ops");
        }
        if (C1Select != SelectChoice.Doc && C2Select != SelectChoice.Doc)
        {
            doc.color = new Color(myCM.DANI.r, myCM.DANI.g, myCM.DANI.b, doc.color.a);
            //Debug.Log("C1 and C2 are both not doc");
        }
        if (C1Select != C2Select) 
        {
            //Debug.Log("C1 or C2");

            if (C1Select == SelectChoice.Ops || C2Select == SelectChoice.Ops)
            {
                ops.color = new Color(myCM.Ops.r, myCM.Ops.g, myCM.Ops.b, ops.color.a);
                //Debug.Log("just C1 or C2 is ops");
            }
            if (C1Select == SelectChoice.Doc || C2Select == SelectChoice.Doc)
            {
                doc.color = new Color(myCM.Doc.r, myCM.Doc.g, myCM.Doc.b, doc.color.a);
                //Debug.Log("just C1 or C2 is doc");
            }
        }
    }
}
