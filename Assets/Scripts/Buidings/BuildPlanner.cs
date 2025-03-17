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
    private Building _removingBuilding = null;
    private BuildingDragger _buildingDragger;
    private bool _isRemoving = false;

    public event Action<Building> BuildingOrdering;

    public event Action<Building> BuildingRemoving;

    public void OnEnable()
    {
        foreach (BuildingUIItem buildingItem in _buildingUIItems)
            buildingItem.BuildingSelected += OnBuildingItemSelected;

        _inputController.ObjectSelectedOnGround += OnObjectSelectedOnGround;
        _inputController.MouseMoved += OnMouseMoved;

        _orderButton.onClick.AddListener(OrderBuilding);
        _removeButotn.onClick.AddListener(() => _isRemoving = true);
    }

    public void OnDisable()
    {
        foreach (BuildingUIItem building in _buildingUIItems)
            building.BuildingSelected -= OnBuildingItemSelected;

        _inputController.ObjectSelectedOnGround -= OnObjectSelectedOnGround;
        _inputController.MouseMoved -= OnMouseMoved;

        _orderButton.onClick.RemoveListener(OrderBuilding);
        _removeButotn.onClick.RemoveAllListeners();
    }

    public void Init(BuildingDragger buildingDragger)
    {
        _buildingDragger = buildingDragger;
    }

    private void OnMouseMoved(Vector3 _, Ray ray)
    {
        if (_isRemoving && Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity))
        {
            if (hit.collider.TryGetComponent(out Building building))
            {
                if (_removingBuilding != null)
                    _removingBuilding.DisableOutline();

                _removingBuilding = building;
                building.EnableOutline();
            }
            else
            {
                if (_removingBuilding != null)
                {
                    _removingBuilding.DisableOutline();
                    _removingBuilding = null;
                }
            }
        }
    }

    private void OnObjectSelectedOnGround(Collider @object)
    {
        if (_buildingDragger.IsDragging)
            return;

        if (@object.TryGetComponent(out Building building) && _isRemoving)
        {
            BuildingRemoving?.Invoke(building);
            _isRemoving = false;
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
}
