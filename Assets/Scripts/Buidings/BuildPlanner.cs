using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildPlanner : MonoBehaviour
{
    [SerializeField] private Button _orderButton;
    [SerializeField] private Button _removeButotn;
    [SerializeField] private BuildingUIItem[] _buildingUIItems;
    [SerializeField] private PlayerInputController _inputController;
    
    private Building _selectedBuildingWithButton = null;
    private Building _selectedBuildingOnGround = null;
    private BuildingDragger _buildingDragger;

    public event Action<Building> BuildingOrdering;

    public event Action<Building> BuildingRemoving;

    public void OnEnable()
    {
        foreach (BuildingUIItem buildingItem in _buildingUIItems)
            buildingItem.BuildingSelected += OnBuildingItemSelected;

        _inputController.ObjectSelectedOnGround += OnObjectSelectedOnGround;
        _inputController.ObjectDeselectedOnGround += OnObjectDeselectedOnGround;

        _orderButton.onClick.AddListener(OrderBuilding);
        _removeButotn.onClick.AddListener(RemoveBuilding);
    }

    public void OnDisable()
    {
        foreach (BuildingUIItem building in _buildingUIItems)
            building.BuildingSelected -= OnBuildingItemSelected;

        _inputController.ObjectSelectedOnGround -= OnObjectSelectedOnGround;
        _inputController.ObjectDeselectedOnGround -= OnObjectDeselectedOnGround;

        _orderButton.onClick.RemoveListener(OrderBuilding);
        _removeButotn.onClick.RemoveListener(RemoveBuilding);
    }

    public void Init(BuildingDragger buildingDragger)
    {
        _buildingDragger = buildingDragger;
    }

    private void OnObjectSelectedOnGround(Collider @object)
    {
        if (_buildingDragger.IsDragging)
            return;

        if (_selectedBuildingOnGround != null)
            _selectedBuildingOnGround.Deselect();

        if (@object.TryGetComponent(out Building building))
        {
            building.Select();
            _selectedBuildingOnGround = building;
        }
    }

    private void OnObjectDeselectedOnGround()
    {
        if (_buildingDragger.IsDragging)
            return;

        if (_selectedBuildingOnGround != null)
        {
            _selectedBuildingOnGround.Deselect();
            _selectedBuildingOnGround = null;
        }
    }

    private void OnBuildingItemSelected(Building building)
    {
        _selectedBuildingWithButton = building;
    }

    private void OrderBuilding()
    {
        if (_selectedBuildingWithButton != null)
            BuildingOrdering?.Invoke(_selectedBuildingWithButton);
    }

    private void RemoveBuilding()
    {
        if (_selectedBuildingOnGround != null)
            BuildingRemoving?.Invoke(_selectedBuildingOnGround);
    }
}
