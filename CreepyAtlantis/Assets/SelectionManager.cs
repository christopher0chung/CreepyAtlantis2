using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour {

    [SerializeField] private SelectChoice C1Select;
    [SerializeField] private SelectChoice C2Select;

    [SerializeField]
    private Image c1;
    [SerializeField]
    private Image c2;
    [SerializeField]
    private Text s1O;
    [SerializeField]
    private Text ops;
    [SerializeField]
    private Text s2O;
    [SerializeField]
    private Text doc;

    private ColorManager myCM;

    void Start()
    {
        myCM = GameObject.FindGameObjectWithTag("Managers").GetComponent<ColorManager>();

        c1 = GameObject.Find("Controller1").GetComponent<Image>();
        c2 = GameObject.Find("Controller2").GetComponent<Image>();

        ops = GameObject.Find("OpsName").GetComponent<Text>();
        doc = GameObject.Find("DocName").GetComponent<Text>();
    }

    public void UpdateSelection(SelectChoice mySC, int pNum)
    {
        if (pNum == 0)
        {
            C1Select = mySC;
        }
        else
        {
            C2Select = mySC;
        }
        ControllerColor();
        NameColor();
    }

    private void ControllerColor()
    {
        if (C1Select == SelectChoice.Ops)
        {
            c1.color = myCM.Ops;
        }
        else if (C1Select == SelectChoice.None)
        {
            c1.color = Color.white;
        }
        else if (C1Select == SelectChoice.Doc)
        {
            c1.color = myCM.Doc;
        }

        if (C2Select == SelectChoice.Ops)
        {
            c2.color = myCM.Ops;
        }
        else if (C2Select == SelectChoice.None)
        {
            c2.color = Color.white;
        }
        else if (C2Select == SelectChoice.Doc)
        {
            c2.color = myCM.Doc;
        }
    }

    private void NameColor ()
    {
        if (C1Select == C2Select)
        {
            ops.color = doc.color = Color.white;
        }
        else 
        {
            if (C1Select == SelectChoice.Ops || C2Select == SelectChoice.Ops)
            {
                ops.color = myCM.Ops;
            }
            else if (C1Select == SelectChoice.Doc || C2Select == SelectChoice.Doc)
            {
                doc.color = myCM.Doc;
            }
        }

        if (C1Select != SelectChoice.Ops && C2Select != SelectChoice.Ops)
        {
            ops.color = Color.white;
        }
        if (C1Select != SelectChoice.Doc && C2Select != SelectChoice.Doc)
        {
            doc.color = Color.white;
        }
    }
}
