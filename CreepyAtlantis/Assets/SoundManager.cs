using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFX { Click, CheckPoint, DeathStatic, Beep }

public class GE_SFX : GameEvent
{
    public SFX toPlay;
    public GE_SFX (SFX s)
    {
        toPlay = s;
    }
}

public class SoundManager : MonoBehaviour {

    public AudioSource myAS;

    public List<AudioClip> myACs = new List<AudioClip>();

	void Start () {
        myAS = GetComponent<AudioSource>();
        LoadSounds();
        //EventManager.instance.Register<P1_DialogueChoiceRumble_GE>(PlaySFX);
        EventManager.instance.Register<GE_SFX>(PlaySFX);
	}

    void LoadSounds()
    {
        myACs.Add((AudioClip)Resources.Load("SFX/TEMPO0-N-Click 1"));
    }
	
    void PlaySFX(GameEvent e)
    {
        if (e.GetType() == typeof(GE_SFX))
        {
            GE_SFX s = (GE_SFX)e;
            if (s.toPlay == SFX.Click)
                myAS.PlayOneShot((AudioClip)Resources.Load("SFX/TEMPO0-N-Click 1"));
            if (s.toPlay == SFX.CheckPoint)
                myAS.PlayOneShot((AudioClip)Resources.Load("SFX/TEMPO0-N-Checkpt chime"));
            if (s.toPlay == SFX.DeathStatic)
                myAS.PlayOneShot((AudioClip)Resources.Load("SFX/TEMPO0-L-Death static"));
            if (s.toPlay == SFX.Beep)
                myAS.PlayOneShot((AudioClip)Resources.Load("SFX/beep"));
        }
        //myAS.PlayOneShot(myACs[0]);
    }
}
