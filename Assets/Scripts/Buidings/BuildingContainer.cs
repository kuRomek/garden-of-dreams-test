using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buildings", menuName = "Custom/Buildings", order = 51)]
public class BuildingContainer : ScriptableObject
{
    [SerializeField] private Building[] _buildingPrefabs;

    private Dictionary<int, Building> _buildings;

    public IReadOnlyDictionary<int, Building> Buildings => _buildings;

    private void OnValidate()
    {
        _buildings = new Dictionary<int, Building>();

        foreach (Building building in _buildingPrefabs)
            _buildings.Add(building.GridIndex, building);
    }
}
