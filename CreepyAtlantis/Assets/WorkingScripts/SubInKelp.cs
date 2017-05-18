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

    private float inKelpTimer;

    public void InKelp()
    {
        inKelp = true;
        timer = 0;
        inKelpTimer += Time.deltaTime;
        if (inKelpTimer >= 30)
        {
            inKelpTimer -= 30;
            Instantiate(Resources.Load("Shark"), new Vector3(transform.position.x + (Random.Range(0,1) * 2 - 1) * 30, Random.Range(5f, 30f), 20), Quaternion.identity);

        }
    }
}
