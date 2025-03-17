using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private BuildPlanner _buildPlanner;
    [SerializeField] private BuildingFactory _buildingFactory;
    [SerializeField] private PlayerInputController _inputController;

    private Builder _builder;
    private BuildingDragger _buildingDragger;

    private void Awake()
    {
        _builder = new Builder(_buildPlanner, _buildingFactory);
        _buildingDragger = new BuildingDragger(_builder, _inputController);
    }

    private void OnEnable()
    {
        _builder.Enable();
        _buildingDragger.Enable();
    }

    private void OnDisable()
    {
        _builder.Disable();
        _buildingDragger.Disable();
    }
}
