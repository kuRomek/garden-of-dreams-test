public class BuildingDragger : IActivatable
{
    private PlayerInputController _inputController;
    private Builder _builder;
    private Building _draggingBuilding = null;

    public BuildingDragger(Builder builder, PlayerInputController inputController)
    {
        _builder = builder;
        _inputController = inputController;
    }

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
    }

    private void MoveBuilding(UnityEngine.Vector3 position)
    {
        if (_draggingBuilding != null)
            _draggingBuilding.transform.position = position;
    }

    private void PlaceBuilding()
    {
        if (_draggingBuilding != null)
            _draggingBuilding = null;
    }
}
