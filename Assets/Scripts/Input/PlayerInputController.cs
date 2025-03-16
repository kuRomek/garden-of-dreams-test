using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private PlayerInput _input;

        public event Action<Vector2> CameraMoved;

        private void Awake()
        {
            _input = new PlayerInput();
        }

        private void OnEnable()
        {
            _input.Enable();

            _input.Player.MouseMoved.performed += OnMouseMoved;
        }

        private void OnDisable()
        {
            _input.Disable();

            _input.Player.MouseMoved.performed -= OnMouseMoved;
        }

        private void OnMouseMoved(InputAction.CallbackContext context)
        {
            Vector2 mousePosition = context.action.ReadValue<Vector2>();

            Vector2 cameraMovingVector = Vector2.zero;

            if (mousePosition.x <= 0)
                cameraMovingVector += Vector2.left;

            if (mousePosition.x >= _camera.pixelWidth)
                cameraMovingVector += Vector2.right;

            if (mousePosition.y <= 0)
                cameraMovingVector += Vector2.down;

            if (mousePosition.y >= _camera.pixelHeight)
                cameraMovingVector += Vector2.up;

            CameraMoved?.Invoke(cameraMovingVector);
        }
    }
}
