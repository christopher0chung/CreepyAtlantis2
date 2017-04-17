using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePointBehavior : MonoBehaviour {

    public TextMesh SampleTimer;
    private float timer;

    private LevelLoader myLL;

    private bool done;

    void Start()
    {
        timer = 120;
        myLL = GameObject.Find("GameStateManager").GetComponent<LevelLoader>();
    }

    void Update()
    {
        if (timer <= 0 && !done)
        {
            done = true;
        }
        if (!done)
        {
            int m = (int)(timer / 60);
            int sInt = (int)(timer % 60);
            string s;
            if ((int)sInt >= 10)
                s = sInt.ToString();
            else if (sInt < 10)
                s = "0" + sInt.ToString();
            else
                s = "00";

            SampleTimer.text = m + ":" + s;
        }
    }

    void OnTriggerStay (Collider other)
    {
        if (other.transform.root.gameObject.name == "Character1")
        {
            timer -= Time.fixedDeltaTime;
        }
    }

    private IEnumerator delayedLoadLevel()
    {
        yield return new WaitForSeconds (3);
        myLL.LoadLevel(myLL.GetLevel() + 1);
        yield break;
    }
}
