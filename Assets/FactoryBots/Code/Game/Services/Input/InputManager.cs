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

        private bool _isInputModified;

        public event Action<GameObject> SelectPerformedAction;
        public event Action<GameObject, Vector3, bool> ExecutePerformedAction;

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
            _inputActions.GameInput.SetModifier.performed += OnModifierSet;
            _inputActions.GameInput.RemoveModifier.performed += OnModifierRemove;
            _inputActions.GameInput.Enable();

            _isInputModified = false;
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
                ExecutePerformedAction?.Invoke(raycastTarget, hitPosition, _isInputModified);
            }
        }

        private void OnModifierSet(InputAction.CallbackContext context) => 
            _isInputModified = true;

        private void OnModifierRemove(InputAction.CallbackContext context) => 
            _isInputModified = false;

        public void Cleanup()
        {
            _inputActions.GameInput.Select.performed -= OnSelectActionPerformed;
            _inputActions.GameInput.Execute.performed -= OnExecuteActionPerformed;
            _inputActions.GameInput.SetModifier.performed -= OnModifierSet;
            _inputActions.GameInput.RemoveModifier.performed -= OnModifierRemove;
            _inputActions.GameInput.Disable();
        }
    }
}
