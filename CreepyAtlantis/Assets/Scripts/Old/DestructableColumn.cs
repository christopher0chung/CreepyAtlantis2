using UnityEngine;
using System.Collections;

public class DestructableColumn : MonoBehaviour, IDestructable {

    public float hitPointsMax;
    public float hitPoints;

    public GameObject explosion;

	// Use this for initialization
	void Start () {
        hitPoints = hitPointsMax;
	}
	
	// Update is called once per frame
	void Update () {
	    if (hitPoints <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
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
