using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUIItem : MonoBehaviour
{
    [SerializeField] private Button _orderButton;
    [SerializeField] private Building _building;

    public event Action<Building> BuildingSelected;

    private void OnEnable()
    {
        _orderButton.onClick.AddListener(OrderBuilding);
    }

    private void OnDisable()
    {
        _orderButton.onClick.RemoveListener(OrderBuilding);
    }

    private void OrderBuilding()
    {
        BuildingSelected?.Invoke(_building);
    }
}
