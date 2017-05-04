using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseRequest_GE : GameEvent { }

public class GameStateAndServices : MonoBehaviour {

    private bool paused;

    void Awake()
    {
        EventManager.instance.Register<PauseRequest_GE>(LocalHandler);
    }

    void LocalHandler(GameEvent e)
    {
        if (e.GetType() == typeof(PauseRequest_GE))
        {
            paused = !paused;
            if (paused)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
            EventManager.instance.Fire(new Pause_GE(paused));
            //keojineun heart b-b-beat ppallajineunde
            //neodapjanke heart b-b-b-beat 
            //georyeo nareul bol ttae
            //majimak nameun sungankkaji 
            //jeomjeom dagaoji crazy
            //ajjihage gyeonun russian roulette
        }
    }
}
