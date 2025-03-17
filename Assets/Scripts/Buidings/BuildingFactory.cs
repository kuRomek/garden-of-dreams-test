using UnityEngine;

public class BuildingFactory : MonoBehaviour
{
    public Building CreateBuilding(Building building)
    {
        return Instantiate(building);
    }

    public void DestroyBuilding(Building building)
    {
        Destroy(building.gameObject);
    }
}
