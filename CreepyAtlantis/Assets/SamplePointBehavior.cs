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
        timer = 60;
        myLL = GameObject.Find("GameStateManager").GetComponent<LevelLoader>();
    }

    void Update()
    {
        if (timer <= 0 && !done)
        {
            done = true;
        }
        if (!done)
            SampleTimer.text = "0:" + ((int)timer).ToString();
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.transform.root.gameObject.tag == "Character1")
        {

        }
    }

    private IEnumerator delayedLoadLevel()
    {
        yield return new WaitForSeconds (3);
        myLL.LoadLevel(myLL.GetLevel() + 1);
        yield break;
    }
}
