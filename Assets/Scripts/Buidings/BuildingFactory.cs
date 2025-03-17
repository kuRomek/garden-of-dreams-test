using UnityEngine;

public class BuildingFactory : MonoBehaviour
{
    public Building CreateBuilding(Building building)
    {
        return Instantiate(building);
    }

    public Building CreateBuilding(Building building, Vector3 position)
    {
        return Instantiate(building, position, Quaternion.identity);
    }

    public void DestroyBuilding(Building building)
    {
        Destroy(building.gameObject);
    }
}
