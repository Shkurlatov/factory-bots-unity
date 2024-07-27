using FactoryBots.Game.Services.Bots;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FactoryBots.Game.Services.Input
{
    public class InputManager : IGameInput
    {
        private readonly Raycaster _raycaster;
        private readonly Mouse _mouse;
        private readonly InputActions _inputActions;

        private Bot _selectedBot;

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
            if (_raycaster.TryGetRaycastTarget(_mouse.position.ReadValue(), out GameObject raycastTarget))
            {
                if (raycastTarget.CompareTag("Bot"))
                {
                    _selectedBot = raycastTarget.GetComponent<Bot>();
                    Debug.Log(raycastTarget.name);
                }
                else
                {
                    _selectedBot = null;
                }
            }
        }

        private void OnExecuteActionPerformed(InputAction.CallbackContext context)
        {
            if (_raycaster.TryGetRaycastTargetWithPosition(_mouse.position.ReadValue(), out GameObject raycastTarget, out Vector3 hitPosition))
            {
                if (_selectedBot != null && raycastTarget.CompareTag("Walkable"))
                {
                    _selectedBot.SetTargetPosition(hitPosition);
                    Debug.Log(hitPosition);
                }
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
