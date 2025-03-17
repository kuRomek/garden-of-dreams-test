using System;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private BuildPlanner _buildPlanner;
    [SerializeField] private BuildingFactory _buildingFactory;
    [SerializeField] private PlayerInputController _inputController;
    [SerializeField] private Ground _ground;
    [SerializeField] private BuildingContainer _buildingContainer;

    private Builder _builder;
    private BuildingDragger _buildingDragger;
    private Grid _grid;
    private Action _mapSaving;

    private void Awake()
    {
        _builder = new Builder(_buildPlanner, _buildingFactory);
        _buildingDragger = new BuildingDragger(_builder, _inputController, _ground);
        _buildPlanner.Init(_buildingDragger);

        InitMap();
    }

    private void OnEnable()
    {
        _builder.Enable();
        _buildingDragger.Enable();
        _grid.Enable();

        Application.quitting += _mapSaving;
    }

    private void OnDisable()
    {
        _builder.Disable();
        _buildingDragger.Disable();
        _grid.Disable();

        Application.quitting -= _mapSaving;
    }

    private void InitMap()
    {
        _grid = new Grid(_buildingDragger, _buildPlanner, MapSerializer.LoadMap());

        for (int i = 0; i < _grid.Size; i++)
        {
            for (int j = 0; j < _grid.Size; j++)
            {
                if (_grid.Cells[i, j] != 0)
                {
                    _buildingFactory.CreateBuilding(
                        _buildingContainer.Buildings[_grid.Cells[i, j]],
                        _grid.CalculateWorldPosition(i, j));
                }
            }
        }
    }
}
