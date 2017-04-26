using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource myAS;

    public List<AudioClip> myACs = new List<AudioClip>();

	void Start () {
        myAS = GetComponent<AudioSource>();
        LoadSounds();
        EventkManager.instance.Register<P1_DialogueChoiceRumble_GE>(PlaySFX);
	}

    void LoadSounds()
    {
        myACs.Add((AudioClip)Resources.Load("SFX/TEMPO0-N-Click 1"));
    }
	
    void PlaySFX(GameEvent e)
    {
        myAS.PlayOneShot(myACs[0]);
    }
}
