using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_UI_Parent : MonoBehaviour {

    [SerializeField] private Camera myCam;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        EventManager.instance.Register<GE_PreLoadLevel>(LocalHandler);
    }

    void Update()
    {
        if (myCam == null)
        {
            HookCam();
            transform.parent = myCam.transform;
            transform.position = myCam.transform.position;
            transform.rotation = myCam.transform.rotation;
        }

    }

    void LocalHandler(GameEvent e)
    {
        transform.parent = null;
    }

    public void HookCam()
    {
        myCam = Camera.main;
    }
}
