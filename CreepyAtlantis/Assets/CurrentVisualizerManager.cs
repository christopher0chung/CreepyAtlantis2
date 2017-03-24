﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CVInfo : MonoBehaviour
{
    private bool _marked;
    public bool marked
    {
        get
        {
            return _marked;
        }
        set
        {
            if (value != _marked)
            {
                _marked = value;
                if (marked)
                {
                    timer = 0;
                    GetComponent<ParticleSystem>().Stop();
                }
            }
        }
    }

    public float timer;

    public void CVUpdate()
    {
        timer += Time.deltaTime;
    }
}

public class CurrentVisualizerManager : MonoBehaviour {

    [SerializeField] private List<GameObject> myCVs = new List<GameObject>();
	// Use this for initialization
	void Start () {
        for (int i = 0; i < 20; i++)
        {
            GameObject myCV = (GameObject) Instantiate(Resources.Load("CurrentVisualizer"), new Vector3(transform.position.x + Random.Range(-20, 20), transform.position.y + Random.Range(-12, 12), 0), Quaternion.identity);
            myCV.AddComponent<CVInfo>();
            myCV.GetComponent<CVInfo>().timer = -i * 5;
            myCVs.Add(myCV);
        }
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < myCVs.Count; i++)
        {
            myCVs[i].GetComponent<CVInfo>().CVUpdate();
            if (myCVs[i].GetComponent<CVInfo>().timer >= 15)
            {
                if (!myCVs[i].GetComponent<CVInfo>().marked)
                {
                    myCVs[i].GetComponent<CVInfo>().marked = true;
                }
            }
            if (Vector3.Distance(transform.position, myCVs[i].transform.position) >=22)
            {
                if (!myCVs[i].GetComponent<CVInfo>().marked)
                {
                    myCVs[i].GetComponent<CVInfo>().marked = true;
                }
            }

            if (myCVs[i].GetComponent<CVInfo>().marked && myCVs[i].GetComponent<CVInfo>().timer >= 5)
            {
                GameObject thisCV = myCVs[i];
                myCVs.Remove(thisCV);
                Destroy(thisCV);
                Invoke("MakeAnother", .1f);
            }
        }
	}

    void MakeAnother()
    {
        GameObject myCV = (GameObject)Instantiate(Resources.Load("CurrentVisualizer"), new Vector3(transform.position.x + Random.Range(-20, 20), transform.position.y + Random.Range(-12, 12), 0), Quaternion.identity);
        myCV.AddComponent<CVInfo>();
        myCVs.Add(myCV);
    }
}
