using System;
using UnityEngine;

public class Grid : IActivatable
{
    private const int Size = 50;
    private const float WorldOffset = 25f;

    private BuildingDragger _buildingDragger;
    private BuildPlanner _buildPlanner;
    private Building[,] _cells = new Building[Size, Size];

    public Grid(BuildingDragger buildingDragger, BuildPlanner buildPlanner)
    {
        _buildingDragger = buildingDragger;
        _buildPlanner = buildPlanner;
    }

    public void Enable()
    {
        _buildingDragger.BuildingDragging += Move;
        _buildingDragger.BuildingPlaced += Place;
        _buildPlanner.BuildingRemoving += Remove;
    }

    public void Disable()
    {
        _buildingDragger.BuildingDragging -= Move;
        _buildingDragger.BuildingPlaced -= Place;
        _buildPlanner.BuildingRemoving -= Remove;
    }

    private void Place(Building building)
    {
        int[] gridPosition = CalculateGridPosition(building.transform.position);

        _cells[gridPosition[0], gridPosition[1]] = building;
    }

    private void Remove(Building building)
    {
        for (int i = 0; i < _cells.GetLength(0); i++)
        {
            for (int j = 0; j < _cells.GetLength(1); j++)
            {
                if (_cells[i, j] == building)
                    _cells[i, j] = null;
            }    
        }
    }

    private void Move(Building building, Vector3 position)
    {
        building.transform.position = new Vector3(Mathf.Round(position.x), 0f, Mathf.Round(position.z));
    }

    public Vector3 CalculateWorldPosition(int[] gridPosition)
    {
        if (gridPosition == null)
            throw new ArgumentNullException(nameof(gridPosition));

        if (gridPosition.Length != 2)
            throw new ArgumentException("The grid is 2-dimensional.");

        if (gridPosition[0] < 0 || gridPosition[0] >= Size)
        {
            throw new ArgumentException($"The size of the grid is {Size}x{Size}, " +
                $"so x position must be in set {{0, ..., {Size - 1}}}, yours is {gridPosition[0]}");
        }

        if (gridPosition[1] < 0 || gridPosition[1] >= Size)
        {
            throw new ArgumentException($"The size of the grid is {Size}x{Size}, " +
                $"so y position must be in set {{0, ..., {Size - 1}}}, yours is {gridPosition[1]}");
        }

        return new Vector3(gridPosition[1] - WorldOffset, -(gridPosition[0] - WorldOffset));
    }

    public int[] CalculateGridPosition(Vector3 worldPosition)
    {
        int[] gridPosition = new int[2]
        {
            (int)(-Mathf.Round(worldPosition.y) + WorldOffset),
            (int)(Mathf.Round(worldPosition.x) + WorldOffset),
        };

        if (gridPosition[0] < 0 || gridPosition[0] >= Size)
        {
            throw new ArgumentException($"The size of the grid is {Size}x{Size}, " +
                $"so x position must be in set {{0, ..., {Size - 1}}}, yours is {gridPosition[0]}");
        }

        if (gridPosition[1] < 0 || gridPosition[1] >= Size)
        {
            throw new ArgumentException($"The size of the grid is {Size}x{Size}, " +
                $"so y position must be in set {{0, ..., {Size - 1}}}, yours is {gridPosition[1]}");
        }

        if (_cells[gridPosition[0], gridPosition[1]] != null)
        {
            throw new InvalidOperationException($"The cell ({gridPosition[0]}, {gridPosition[1]}) " +
                $"is already taken.");
        }

        return gridPosition;
    }
}
