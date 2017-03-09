using UnityEngine;
using System.Collections;

public class JellyFishGuts : MonoBehaviour {

    private SkinnedMeshRenderer mySMR;
    public float percent;

    private float timer;

    private float ang;

    // Use this for initialization
    void Start()
    {
        mySMR = GetComponent<SkinnedMeshRenderer>();
        timer = Random.Range(-2000, -1800);
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        percent = ((Mathf.Sin(timer * 2) / 2) + .5f) * 100;
        ang = timer * 50;
        transform.localRotation = Quaternion.Euler(-90, ang, 0);

        mySMR.SetBlendShapeWeight(0, percent);
    }
}
