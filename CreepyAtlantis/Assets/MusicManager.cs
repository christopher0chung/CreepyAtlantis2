using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    private List<AudioSource> myAudioLayers;

    public class FadeInfo
    {
        public int layer;
        public float speed;
        public float volume;
        public FadeInfo (int l, float s, float v)
        {
            layer = l;
            speed = s;
            volume = v;
        }
    }

    private FadeInfo myFI;

    void Awake ()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start ()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            myAudioLayers.Add(transform.GetChild(0).GetChild(i).GetComponent<AudioSource>());
        }
    }

    public void FadeOutAll()
    {
        StopAllCoroutines();
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            myFI = new FadeInfo(i, 5, 0);
            StartCoroutine("Fade", myFI);
        }
    }

    public void FadeOut(int layer)
    {
        myFI = new FadeInfo(layer, 5, 0);
        StartCoroutine("Fade", myFI);
    }

    public void FadeIn(int layer)
    {
        myFI = new FadeInfo(layer, 8, 1);
        StartCoroutine("Fade", myFI);
    }

    public void MuteAll()
    {
        StopAllCoroutines();
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            myAudioLayers[i].volume = 0;
        }
    }

    public void Mute(int layer)
    {
        myAudioLayers[layer].volume = 0;
    }

    private IEnumerator Fade (FadeInfo fI)
    {
        bool done = false;

        while (!done)
        {
            myAudioLayers[fI.layer].volume = Mathf.Lerp(myAudioLayers[fI.layer].volume, fI.volume, (fI.speed * Time.deltaTime));
            yield return null;
        }

        print("layer " + fI.layer + " complete");
        yield break;
    }
}
