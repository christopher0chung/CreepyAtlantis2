using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour {

    public Text line;
    public Text c1;
    public Text c2;
    public Text start;
    public Text start1;
    public Text start2;
    public Text start3;
    public Image c1Box;
    public Image c2Box;

    private int states;

	void Update () {
        if (states == 0)
        {
            line.color = new Color(0, 0, 0, 0);
            c1.color = new Color(.706f, .706f, .706f, .6f);
            c2.color = new Color(.706f, .706f, .706f, .6f);
            start.color = new Color(0, 0, 0, 0);
            start1.color = new Color(0, 0, 0, 0);
            start2.color = new Color(.9f, .9f, .9f, .9f);
            start3.color = new Color(.074f, 1f, .773f, .9f);
            c1Box.color = new Color(.074f, 1f, .773f, .09f);
            c2Box.color = new Color(.074f, 1f, .773f, .09f);
        }
        else if (states == 1)
        {
            line.color = new Color(.706f, .706f, .706f, .2f);
            c1.color = new Color(.074f, 1f, .773f, .9f);
            c2.color = new Color(.706f, .706f, .706f, .6f);
            start.color = new Color(0, 0, 0, 0);
            start1.color = new Color(0, 0, 0, 0);
            start2.color = new Color(.9f, .9f, .9f, .9f);
            start3.color = new Color(.074f, 1f, .773f, .9f);
            c1Box.color = new Color(0, 0, 0, 0);
            c2Box.color = new Color(.074f, 1f, .773f, .09f);
        }
        else if (states > 1)
        {
            line.color = new Color(.706f, .706f, .706f, .2f);
            c1.color = new Color(.074f, 1f, .773f, .9f);
            c2.color = new Color(.074f, 1f, .773f, .9f);
            start.color = new Color (.9f, .9f, .9f, .9f);
            start1.color = new Color(.074f, 1f, .773f, .9f);
            start2.color = new Color(0, 0, 0, 0);
            start3.color = new Color(0, 0, 0, 0);
            c1Box.color = new Color(0, 0, 0, 0);
            c2Box.color = new Color(0, 0, 0, 0);
        }
	}

    public void setState (int i)
    {
        states = i;
    }

    public int GetState()
    {
        return states;
    }
}
