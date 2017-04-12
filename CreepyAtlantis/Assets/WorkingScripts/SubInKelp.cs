using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubInKelp : MonoBehaviour {

    public static bool inKelp;

    [SerializeField] bool _inKelp;

    [SerializeField] private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1)
        {
            inKelp = false;
        }

        _inKelp = inKelp;
    }

    public void InKelp()
    {
        inKelp = true;
        timer = 0;
    }
}
