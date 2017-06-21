using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceSelectIcon : MonoBehaviour {

    public Text checkEmpty;
	void Update () {
        if (checkEmpty.text != "")
        {
            Debug.Log("Not empty");
            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 1);
        }
        else
        {
            Debug.Log("Empty");
            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 0);
        }

    }
}
