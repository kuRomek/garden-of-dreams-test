using System;

[Serializable]
public class GridData
{
    public const int Size = 50;

    private byte[,] _cells = new byte[Size, Size];

    public byte[,] Cells => _cells;

    public void TakePlace(int xPosition, int yPosition, byte buildingIndex)
    {
        _cells[xPosition, yPosition] = buildingIndex;
    }

    public void Remove(int xPosition, int yPosition)
    {
        _cells[xPosition, yPosition] = 0;
    }
}