using UnityEngine;
using System.Collections;

public class DestructableVeinScript : MonoBehaviour, IDestructable {

    public float hitPointsMax;
    public float hitPoints;
    public GameObject explosion;
    static int numberCrystals = 5;
    public GameObject crystal;

    private GameObject[] crystals;

    public float explosiveScale;

    // Use this for initialization
    void Start()
    {
        crystals = new GameObject[numberCrystals];
        hitPoints = hitPointsMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (hitPoints <= 0)
        {
            for (int i = 0; i < numberCrystals; i++)
            {
                crystals[i] = (GameObject)Instantiate(crystal, transform.position + new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(1.0f, 3.0f), 0), Quaternion.Euler (0, 0, Random.Range (1.0f, 70.0f)));
                Debug.Log(crystals[i].transform.position);
                crystals[i].GetComponent<Rigidbody2D>().AddForce((crystals[i].transform.position - transform.position) * explosiveScale, ForceMode2D.Impulse);
                crystals[i].GetComponent<Rigidbody2D>().AddTorque(Random.Range(-4f, 4f));

            }
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
