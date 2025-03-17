using UnityEngine;

public class BuildingIndicator : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Color _availableForBuildingColor;
    [SerializeField] private Color _notAvailableForBuildingColor;

    public void SetUnableToBuildMode()
    {
        _renderer.material.color = _notAvailableForBuildingColor;
    }

    public void SetAbleToBuildMode()
    {
        _renderer.material.color = _availableForBuildingColor;
    }
}
