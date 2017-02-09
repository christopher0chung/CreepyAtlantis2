using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GazeTrigger : MonoBehaviour {

    [SerializeField] private float gazeTimer;
    [SerializeField] private float idleTimer;
    [SerializeField] private Image progressImage;

    public UnityEvent OnGazeComplete = new UnityEvent();
    public UnityEvent OnHeadStop = new UnityEvent();

    private bool isFixed = true;
    private TextMeshReadout myTMR;

	// Use this for initialization
	void Start () {
        myTMR = Camera.main.transform.GetComponentInChildren<TextMeshReadout>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 camLookDir = Camera.main.transform.forward;
        Vector3 vectorFromCameraToTarget = transform.position - Camera.main.transform.position;

        float angle = Vector3.Angle(camLookDir, vectorFromCameraToTarget);

        if (isFixed)
        {
            //Can do something based off of angle.
            if (angle <= 5f)
            {
                gazeTimer += Time.deltaTime / 2;
            }
            else
            {
                gazeTimer -= Time.deltaTime;
            }

            gazeTimer = Mathf.Clamp01(gazeTimer);
            progressImage.fillAmount = gazeTimer;

            if (gazeTimer >= 1)
            {
                gazeTimer = 0;
                idleTimer = -5;
                OnGazeComplete.Invoke();
                isFixed = false;
            }
        }
        else
        {
            transform.position = Camera.main.transform.position + Vector3.Normalize(camLookDir) * 100;

            if (Vector3.Magnitude(myTMR.smoothedDeltaAngs) <= .6f)
            {
                idleTimer += Time.deltaTime / 2;
            }
            else
            {
                idleTimer = 0;
            }
            
            idleTimer = Mathf.Clamp01 (idleTimer);
            progressImage.fillAmount = idleTimer;


            if (idleTimer >= 1)
            {
                idleTimer = 0;
                gazeTimer = 0;
                OnHeadStop.Invoke();
                isFixed = true;
            }
        }
   }
}
