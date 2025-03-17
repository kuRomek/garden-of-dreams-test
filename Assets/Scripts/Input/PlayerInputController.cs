using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _groundLayer;

    private PlayerInput _input;
    private Vector2 _mousePosition;

    public event Action<Vector2> CameraMoved;

    public event Action<Vector3, Ray> MouseMoved;

    public event Action MouseClicked;

    public event Action<Collider> ObjectSelectedOnGround;

    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.Player.MouseClicked.performed += OnMouseClicked;
        _input.Player.MouseMoved.performed += OnMouseMoved;
    }

    private void OnDisable()
    {
        _input.Disable();

        _input.Player.MouseMoved.performed -= OnMouseMoved;
    }

    private void OnMouseClicked(InputAction.CallbackContext context)
    {
        if (EventSystem.current.IsPointerOverGameObject() == true)
            return;

        MouseClicked?.Invoke();

        Ray ray = _camera.ScreenPointToRay(_mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity))
        {
            if (hit.collider.TryGetComponent(out BoxCollider collider))
                ObjectSelectedOnGround?.Invoke(collider);
        }
    }

    private void OnMouseMoved(InputAction.CallbackContext context)
    {
        _mousePosition = context.action.ReadValue<Vector2>();

        Ray ray = _camera.ScreenPointToRay(_mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, _groundLayer))
        {
            if (hit.collider.TryGetComponent(out Ground _))
                MouseMoved?.Invoke(hit.point, ray);
        }

        Vector2 cameraMovingVector = Vector2.zero;

        if (_mousePosition.x <= 0)
            cameraMovingVector += Vector2.left;

        if (_mousePosition.x >= _camera.pixelWidth)
            cameraMovingVector += Vector2.right;

        if (_mousePosition.y <= 0)
            cameraMovingVector += Vector2.down;

        if (_mousePosition.y >= _camera.pixelHeight)
            cameraMovingVector += Vector2.up;

        CameraMoved?.Invoke(cameraMovingVector);
    }
}
