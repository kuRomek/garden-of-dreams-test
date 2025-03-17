using System;

public class BuildingDragger : IActivatable
{
    private PlayerInputController _inputController;
    private Builder _builder;
    private Building _draggingBuilding = null;
    private Ground _ground;

    public BuildingDragger(Builder builder, PlayerInputController inputController, Ground ground)
    {
        _builder = builder;
        _inputController = inputController;
        _ground = ground;
    }

    public bool IsDragging { get; private set; }

    public event Action<Building> BuildingPlaced;

    public event Action<Building, UnityEngine.Vector3> BuildingDragging;

    public void Enable()
    {
        _builder.BuildingOrdered += OnBuildingOrdered;
        _inputController.MouseMoved += MoveBuilding;
        _inputController.MouseClicked += PlaceBuilding;
    }

    public void Disable()
    {
        _builder.BuildingOrdered -= OnBuildingOrdered;
        _inputController.MouseMoved -= MoveBuilding;
        _inputController.MouseClicked -= PlaceBuilding;
    }

    private void OnBuildingOrdered(Building building)
    {
        _draggingBuilding = building;
        _ground.SetBuildingMode();
        _draggingBuilding.EnableBuildingIndicator();
        IsDragging = true;
    }

    private void MoveBuilding(UnityEngine.Vector3 position)
    {
        if (_draggingBuilding != null)
            BuildingDragging?.Invoke(_draggingBuilding, position);
    }

    private void PlaceBuilding()
    {
        if (_draggingBuilding != null && _draggingBuilding.IsAvailableToPlace)
        {
            _draggingBuilding.DisableBuildingIndicator();
            BuildingPlaced?.Invoke(_draggingBuilding);
            _ground.RemoveBuildingMode();
            IsDragging = false;
            _draggingBuilding = null;
        }
    }
}
