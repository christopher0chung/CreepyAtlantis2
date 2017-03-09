using UnityEngine;
using System.Collections;

public class ShaleChipScript : MonoBehaviour {

    public bool embedded;
    private float cleanUpTimer;
    private bool marked;

    void Start()
    {
        GetComponent<AudioSource>().pitch = Random.Range(1.0f, 1.3f);
    }

    void Update ()
    {
        if (marked)
            cleanUpTimer += Time.deltaTime;
        if (cleanUpTimer > 1)
            Destroy(this.gameObject);
    }

    void OnCollisionStay2D (Collision2D other)
    {
        if (!embedded && (other.transform.root.name == "PlayerCharacter1" || other.transform.root.name == "PlayerCharacter2"))
        {
            marked = true;
            GetComponent<AudioSource>().Play();
            GetComponent<Collider2D>().enabled = false;
            GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }
}
