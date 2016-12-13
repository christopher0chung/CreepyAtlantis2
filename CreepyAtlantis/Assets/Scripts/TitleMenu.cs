using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour {

    public Text line;
    public Text c1;
    public Text c2;
    public Text start;

    private int states;

	void Update () {
        if (states == 0)
        {
            line.color = new Color(0, 0, 0, 0);
            c1.color = new Color(0, 0, 0, 0);
            c2.color = new Color(0, 0, 0, 0);
            start.color = new Color(0, 0, 0, 0);
        }
        else if (states == 1)
        {
            line.color = new Color(.706f, .706f, .706f, .404f);
            c1.color = new Color(.706f, .706f, .706f, .404f);
            c2.color = new Color(0, 0, 0, 0);
            start.color = new Color(0, 0, 0, 0);
        }
        else if (states > 1)
        {
            line.color = new Color(.706f, .706f, .706f, .404f);
            c1.color = new Color(.706f, .706f, .706f, .404f);
            c2.color = new Color(.706f, .706f, .706f, .404f);
            start.color = new Color(1, 0, 0, .404f);
        }
	}

    public void setState (int i)
    {
        states = i;
    }
}
