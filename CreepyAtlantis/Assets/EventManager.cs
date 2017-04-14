using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerID { p1, p2 }
public enum Stick { Left, Right }
public enum Button { Action, Dialogue, Choice1, Choice2 }

public abstract class GameEvent
{
    public delegate void Handler(GameEvent e);
}


//---------------------------------------
// Rumble Events
//---------------------------------------

public class P1_DialogueChoiceRumble_GE : GameEvent { }

public class P2_DialogueChoiceRumble_GE : GameEvent { }


//---------------------------------------
// Control Events
//---------------------------------------

public class Stick_GE : GameEvent
{
    public PlayerID thisPID;
    public Stick stick;
    public float upDown;
    public float leftRight;

    public Stick_GE(PlayerID pID, Stick s, float uD, float lR)
    {
        thisPID = pID;
        stick = s;
        upDown = uD;
        leftRight = lR;
    }
}

public class Button_GE : GameEvent
{
    public PlayerID thisPID;
    public Button button;
    public bool pressedReleased;

    public Button_GE(PlayerID pID, Button b, bool pR)
    {
        thisPID = pID;
        button = b;
        pressedReleased = pR;
    }
}


//---------------------------------------
// Event Manager
//---------------------------------------

public class EventManager : ScriptableObject {
    //---------------------
    // Creates singleton for ease of access
    //---------------------

    static private EventManager _instance;
    static public EventManager instance
    {
        get
        {
            if (_instance == null)
                return _instance = EventManager.CreateInstance<EventManager>();
            else
                return _instance;
        }
    }

    //---------------------
    // Storage of Events
    //---------------------

    private Dictionary<Type, GameEvent.Handler> registeredHandlers = new Dictionary<Type, GameEvent.Handler>();


    //---------------------
    // Register and Unregister
    //---------------------

    public void Register<T>(GameEvent.Handler handler) where T : GameEvent
    {
        Type type = typeof(T);
        if(registeredHandlers.ContainsKey(type))
        {
            registeredHandlers[type] += handler;
            //Debug.Log("Added Handler");
        }
        else
        {
            registeredHandlers[type] = handler;

            //Debug.Log("Registered Handler " + handler);
        }
        //if (handler != null)
            //Debug.Log(handler);
    }

    public void Unregister<T>(GameEvent.Handler handler) where T : GameEvent
    {
        Type type = typeof(T);
        GameEvent.Handler handlers;
        if (registeredHandlers.TryGetValue(type, out handlers))
        {
            handlers -= handler;
            if (handlers == null)
            {
                registeredHandlers.Remove(type);
            }
            else
            {
                registeredHandlers[type] = handlers;
            }
        }
    }

    //---------------------
    // Call event
    //---------------------

    public void Fire(GameEvent e)
    {
        Type type = e.GetType();
        //Debug.Log(e.GetType());
        GameEvent.Handler handlers;
        if (registeredHandlers.TryGetValue(type, out handlers))
        {
            handlers(e);
            //Debug.Log("Event Fired");
        }
        else
        {
            Debug.Log(handlers);
        }
    }
}
