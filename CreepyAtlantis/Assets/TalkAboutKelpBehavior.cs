using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkAboutKelpBehavior : MonoBehaviour
{
    private void Start()
    {
        SphereCollider myCol = gameObject.AddComponent<SphereCollider>();
        myCol.isTrigger = true;
        myCol.radius = 30;
    }

    private float timer;

    private void Update()
    {
        if (timer > 30)
        {
            GetComponent<TalkAboutKelpObjective>().Trigger();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Character1")
        {
            timer += Time.fixedDeltaTime;
        }

    }
}
