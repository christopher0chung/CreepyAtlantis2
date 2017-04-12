using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToIllumable : MonoBehaviour {

    private float lightAng;
    public float baseRadius;
    public float maxDistance;
    public LayerMask myLM;

    List<RaycastHit> hitGOs = new List<RaycastHit>();
    RaycastHit[] tempHitGOs = new RaycastHit[0];

	// Use this for initialization
	void Start () {
        lightAng = GetComponent<Light>().spotAngle;
	}
	
	// Update is called once per frame
	void Update () {

        hitGOs = new List<RaycastHit>();

        for (int i = 0; i < 3; i++)
        {
            float loopStartDist = (i * maxDistance) / 3;
            float alphaRad = lightAng * Mathf.Deg2Rad / 2;

            //Debug.Log(baseRadius + loopStartDist * Mathf.Tan(alphaRad));

            Ray thisRay = new Ray(transform.position + (loopStartDist * transform.forward), transform.forward);
            Debug.DrawLine(transform.position + Vector3.up * i + transform.forward * loopStartDist, transform.position + transform.forward * maxDistance + Vector3.up * i);

            tempHitGOs = Physics.SphereCastAll(thisRay, 
                baseRadius + loopStartDist * Mathf.Tan(alphaRad),
                maxDistance - loopStartDist, 
                myLM, 
                QueryTriggerInteraction.Collide);

            for (int j = 0; j < tempHitGOs.Length; j++)
            {
                hitGOs.Add(tempHitGOs[j]);
            }
        }

        foreach(RaycastHit RCH in hitGOs)
        {
            if (RCH.transform.root.gameObject.GetComponent<IIlluminable>() != null)
            {
                RCH.transform.root.gameObject.GetComponent<IIlluminable>().Illuminate(this.gameObject);
                Debug.Log(RCH.transform.gameObject.name);
            }
        }
		
	}
}
