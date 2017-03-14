using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterIngressEgressManager : MonoBehaviour {

    private GameObject[] characters = new GameObject[2];
    private GameObject sub;

    private void Awake()
    {
        SceneManager.sceneLoaded += grabChars;
        GameStateManager.onSubInteract += PlayerIngressEgress;
    }

    private void PlayerIngressEgress (int player, bool ingress)
    {
        if (!ingress)
        {
            characters[player].transform.position = sub.transform.position + (Vector3.down * 4) + (Vector3.left * 3) + (Vector3.right * 4 * player); 
        }
        else
        {
            characters[player].GetComponent<PlayerAir>().Supply(100);
        }
        characters[player].SetActive(!ingress);

    }

    private void grabChars (Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Play01")
        {
            DelayedFindChar();
        }
    }

    void Update()
    {
        if (characters[0] != null || characters [1] != null)
        {
            foreach (GameObject myChar in characters)
            {
                if (!myChar.activeInHierarchy)
                {
                    myChar.transform.position = sub.transform.position;
                }
            }
        }
    }

    private void DelayedFindChar()
    {
        characters[0] = GameObject.Find("Character0");
        characters[1] = GameObject.Find("Character1");
        sub = GameObject.Find("Sub");
    }
}
