using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private BuildPlanner _buildPlanner;
    [SerializeField] private BuildingFactory _buildingFactory;
    [SerializeField] private PlayerInputController _inputController;
    [SerializeField] private Ground _ground;

    private Builder _builder;
    private BuildingDragger _buildingDragger;
    private Grid _grid;

    private void Awake()
    {
        _builder = new Builder(_buildPlanner, _buildingFactory);
        _buildingDragger = new BuildingDragger(_builder, _inputController, _ground);
        _grid = new Grid(_buildingDragger, _buildPlanner);
        _buildPlanner.Init(_buildingDragger);
    }

    private void OnEnable()
    {
        _builder.Enable();
        _buildingDragger.Enable();
        _grid.Enable();
    }

    private void OnDisable()
    {
        _builder.Disable();
        _buildingDragger.Disable();
        _grid.Disable();
    }
}
