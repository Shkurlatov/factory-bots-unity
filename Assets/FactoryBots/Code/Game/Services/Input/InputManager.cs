using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FactoryBots.Game.Services.Input
{
    public class InputManager : IGameInput
    {
        private readonly Raycaster _raycaster;
        private readonly Mouse _mouse;
        private readonly InputActions _inputActions;

        public event Action<GameObject> SelectPerformedAction;
        public event Action<GameObject, Vector3> ExecutePerformedAction;

        public InputManager(Raycaster raycaster)
        {
            _raycaster = raycaster;
            _mouse = Mouse.current;
            _inputActions = new InputActions();
        }

        public void Initialize()
        {
            _inputActions.GameInput.Select.performed += OnSelectActionPerformed;
            _inputActions.GameInput.Execute.performed += OnExecuteActionPerformed;
            _inputActions.GameInput.Enable();
        }

        private void OnSelectActionPerformed(InputAction.CallbackContext context)
        {
            GameObject raycastTarget = _raycaster.GetRaycastTarget(_mouse.position.ReadValue());
            SelectPerformedAction?.Invoke(raycastTarget);
        }

        private void OnExecuteActionPerformed(InputAction.CallbackContext context)
        {
            if (_raycaster.TryGetRaycastTargetWithPosition(_mouse.position.ReadValue(), out GameObject raycastTarget, out Vector3 hitPosition))
            {
                ExecutePerformedAction?.Invoke(raycastTarget, hitPosition);
            }
        }

        public void Cleanup()
        {
            _inputActions.GameInput.Select.performed -= OnSelectActionPerformed;
            _inputActions.GameInput.Execute.performed -= OnExecuteActionPerformed;
            _inputActions.GameInput.Disable();
        }
    }
}
