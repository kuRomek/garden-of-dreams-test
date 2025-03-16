using UnityEngine;
using Input;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private PlayerInputController _inputController;
    [SerializeField] private float _speed;

    private Vector3 _movingVector;

    private void OnEnable()
    {
        _inputController.CameraMoved += MoveCamera;
    }

    private void OnDisable()
    {
        _inputController.CameraMoved -= MoveCamera;
    }

    private void Update()
    {
        transform.Translate(_movingVector * Time.deltaTime);
    }

    private void MoveCamera(Vector2 movingVector)
    {
        _movingVector = new Vector3(movingVector.x, 0f, movingVector.y) * _speed;
    }
}
