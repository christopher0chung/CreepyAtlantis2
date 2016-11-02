using UnityEngine;
using System.Collections;

public class DestructableColumn : MonoBehaviour, IDestructable {

    public float hitPointsMax;
    public float hitPoints;

	// Use this for initialization
	void Start () {
        hitPoints = hitPointsMax;
	}
	
	// Update is called once per frame
	void Update () {
	    if (hitPoints <= 0)
        {
            DestroyIt();
        }
	}

    public void DamageIt(float dmg)
    {
        hitPoints -= dmg;
    }

    public void DestroyIt()
    {
        Destroy(this.gameObject);
    }
}
