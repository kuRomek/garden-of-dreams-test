using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private Outline _outline;

    public void Select() =>
        _outline.enabled = true;

    public void Deselect() =>
        _outline.enabled = false;
}
