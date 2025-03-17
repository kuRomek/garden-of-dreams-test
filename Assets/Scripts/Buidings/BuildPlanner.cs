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

    public event Action<Building> BuildingOrdering;

    public event Action<Building> BuildingRemoving;

    public void OnEnable()
    {
        foreach (BuildingUIItem building in _buildingUIItems)
            building.BuildingSelected += OnBuildingSelected;

        _inputController.ObjectSelectedOnGround += OnObjectSelected;
        _inputController.ObjectDeselectedOnGround += OnObjectDeselected;

        _orderButton.onClick.AddListener(OrderBuilding);
        _removeButotn.onClick.AddListener(RemoveBuilding);
    }

    public void OnDisable()
    {
        foreach (BuildingUIItem building in _buildingUIItems)
            building.BuildingSelected -= OnBuildingSelected;

        _inputController.ObjectSelectedOnGround -= OnObjectSelected;
        _inputController.ObjectDeselectedOnGround -= OnObjectDeselected;

        _orderButton.onClick.RemoveListener(OrderBuilding);
        _removeButotn.onClick.RemoveListener(RemoveBuilding);
    }

    private void OnObjectSelected(Collider @object)
    {
        if (_selectedBuildingOnGround != null)
            _selectedBuildingOnGround.Deselect();

        if (@object.TryGetComponent(out Building building))
        {
            building.Select();
            _selectedBuildingOnGround = building;
        }
    }

    private void OnObjectDeselected()
    {
        if (_selectedBuildingOnGround != null)
        {
            _selectedBuildingOnGround.Deselect();
            _selectedBuildingOnGround = null;
        }
    }

    private void OnBuildingSelected(Building building)
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
