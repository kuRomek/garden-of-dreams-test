using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private Outline _outline;
    [SerializeField] private BuildingIndicator _buildingIndicator;
    [SerializeField] private BoxCollider _collider;

    private List<Building> _overlappedBuidings = new List<Building>();
    public bool IsAvailableToPlace => _overlappedBuidings.Count == 0;

    private void Awake()
    {
        float width = Mathf.Ceil(_collider.size.x);
        float height = Mathf.Ceil(_collider.size.z);

        if (width % 2 == 1)
            width += 1f;

        if (height % 2 == 1)
            height += 1f;

        _buildingIndicator.transform.localScale =
            new Vector3(width * 0.1f, 1f, height * 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Building building))
        {
            _overlappedBuidings.Add(building);
            _buildingIndicator.SetUnableToBuildMode();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Building building))
        {
            _overlappedBuidings.Remove(building);

            if (IsAvailableToPlace)
                _buildingIndicator.SetAbleToBuildMode();
        }
    }

    public void Select()
    {
        _outline.enabled = true;
    }

    public void Deselect()
    {
        _outline.enabled = false;
    }

    public void EnableBuildingIndicator()
    {
        _buildingIndicator.gameObject.SetActive(true);
    }

    public void DisableBuildingIndicator()
    {
        _buildingIndicator.gameObject.SetActive(false);
    }
}
