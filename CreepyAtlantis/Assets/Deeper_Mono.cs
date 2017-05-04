using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreLoadLevel_GE : GameEvent { }
public class Pause_GE : GameEvent
{
    public bool pause;
    public Pause_GE (bool p)
    {
        pause = p;
    }
}

public class Deeper_Mono : MonoBehaviour {

    private bool paused;

    protected virtual void D_Awake()
    {
        D_Register();
    }

    protected virtual void D_Start()
    {
		
	}

    protected virtual void D_Update()
    {
		
	}

    protected virtual void D_FixedUpdate()
    {

    }

    protected virtual void D_LateUpdate()
    {

    }

    #region EventManager Interactions
    protected virtual void D_Register()
    {
        EventManager.instance.Register<PreLoadLevel_GE>(D_LocalHandler);
        EventManager.instance.Register<Pause_GE>(D_LocalHandler);
    }

    protected virtual void D_Unregister()
    {
        EventManager.instance.Unregister<PreLoadLevel_GE>(D_LocalHandler);
        EventManager.instance.Unregister<Pause_GE>(D_LocalHandler);
    }

    protected virtual void D_Destroy()
    {
        D_Unregister();
    }

    public virtual void D_PreUnload()
    {
        D_Unregister();
    }

    public virtual void D_LocalHandler (GameEvent e)
    {
        if (e.GetType() == typeof(PreLoadLevel_GE))
        {
            D_PreUnload();
        }
        if (e.GetType() == typeof(Pause_GE))
        {
            paused = ((Pause_GE)e).pause;
        }
    }
    #endregion

    #region MonoBehaviour Funcs
    void Awake()
    {
        D_Awake();
    }

    void Start()
    {
        D_Start();
    }

    void Update()
    {
        if (!paused)
            D_Update();
    }

    void FixedUpdate()
    {
        if (!paused)
            D_FixedUpdate();
    }

    void LateUpdate()
    {
        if (!paused)
            D_LateUpdate();
    }
    #endregion
}
