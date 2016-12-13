using System.Collections;
using UnityEngine;


public class ObjectivesTracker : MonoBehaviour {

    public void ObjectivesUpdate(string detail)
    {
        if (GetComponent<LevelLoader>().LEVEL == 1)
        {
            if (detail == "LAST")
            {
                GetComponent<LevelLoader>().LoadLevel(2);
            }
        }
    }
}
