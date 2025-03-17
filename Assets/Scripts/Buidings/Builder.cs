using System;

public class Builder : IActivatable
{
    private BuildPlanner _buildPlanner;
    private BuildingFactory _buildingFactory;

    public Builder(BuildPlanner buildPlanner, BuildingFactory buildingFactory)
    {
        _buildPlanner = buildPlanner;
        _buildingFactory = buildingFactory;
    }

    public event Action<Building> BuildingOrdered;

    public void Enable()
    {
        _buildPlanner.BuildingOrdering += OrderBuilding;
        _buildPlanner.BuildingRemoving += RemoveBuilding;
    }

    public void Disable()
    {
        _buildPlanner.BuildingOrdering -= OrderBuilding;
        _buildPlanner.BuildingRemoving -= RemoveBuilding;
    }

    private void OrderBuilding(Building building)
    {
        BuildingOrdered?.Invoke(_buildingFactory.CreateBuilding(building));
    }

    private void RemoveBuilding(Building building)
    {
        _buildingFactory.DestroyBuilding(building);
    }
}