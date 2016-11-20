using UnityEngine;
using System.Collections;

public interface ISpawnableUsingDepot {

    GameObject GetFromDepot(DepotObjects whatIWantFromDepot);
    void ReturnToDepot(DepotObjects whatIWantFromDepot, GameObject whatIWantSentToDepot);

}
