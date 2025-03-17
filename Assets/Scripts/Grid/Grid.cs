using System;
using UnityEngine;

public class Grid : IActivatable
{
    private const int WorldOffset = 25;

    private BuildingDragger _buildingDragger;
    private BuildPlanner _buildPlanner;
    private GridData _gridData;
    private Action _mapSaving;

    public Grid(BuildingDragger buildingDragger, BuildPlanner buildPlanner, GridData gridData = null)
    {
        _buildingDragger = buildingDragger;
        _buildPlanner = buildPlanner;

        if (gridData == null)
            _gridData = new GridData();
        else
            _gridData = gridData;

        _mapSaving = () => MapSerializer.SaveMap(_gridData);
    }

    public int Size => GridData.Size;

    public byte[,] Cells => _gridData.Cells;

    public void Enable()
    {
        _buildingDragger.BuildingDragging += Move;
        _buildingDragger.BuildingPlaced += Place;
        _buildPlanner.BuildingRemoving += Remove;

        Application.quitting += _mapSaving;
    }

    public void Disable()
    {
        _buildingDragger.BuildingDragging -= Move;
        _buildingDragger.BuildingPlaced -= Place;
        _buildPlanner.BuildingRemoving -= Remove;

        Application.quitting -= _mapSaving;
    }

    private void Place(Building building)
    {
        Vector2Int gridPosition = CalculateGridPosition(building.transform.position);

        _gridData.TakePlace(gridPosition[0], gridPosition[1], building.GridIndex);
    }

    private void Remove(Building building)
    {
        for (int i = 0; i < Cells.GetLength(0); i++)
        {
            for (int j = 0; j < Cells.GetLength(1); j++)
            {
                if (Cells[i, j] == building.GridIndex)
                    _gridData.Remove(i, j);
            }
        }
    }

    private void Move(Building building, Vector3 position)
    {
        building.transform.position = new Vector3(Mathf.Round(position.x), 0f, Mathf.Round(position.z));
    }

    public Vector2Int CalculateGridPosition(Vector3 worldPosition)
    {
        return new Vector2Int((int)worldPosition.x + WorldOffset, (int)worldPosition.z + WorldOffset);
    }

    public Vector3 CalculateWorldPosition(int x, int y)
    {
        return new Vector3(x - WorldOffset, 0f, y - WorldOffset);
    }
}
