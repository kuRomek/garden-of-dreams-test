using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Sprite _tileSprite;

    public void SetBuildingMode()
    {
        _renderer.material.mainTexture = _tileSprite.texture;
    }

    public void RemoveBuildingMode()
    {
        _renderer.material.mainTexture = null;
    }
}
