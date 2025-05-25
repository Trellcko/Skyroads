using System;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = Trell.Skyroads.Infrastructure.Input.InputSystem;

namespace Trell.Skyroads.Infrastructure
{
    public class StandaloneInput : IInputService
    {
        public event Action<Vector2> MovementPerformed;
        public event Action MovementCancelled;
        public event Action BoostPerformed;
        public event Action BoostReleased;

        private readonly InputSystem _inputSystem;
        
        public StandaloneInput()
        {
            _inputSystem = new();
            _inputSystem.Enable();
            
            _inputSystem.Player.Movement.performed += OnMovementPerformed;
            _inputSystem.Player.Movement.canceled += OnMovementCanceled;
            _inputSystem.Player.Boost.performed += OnBoostPerformed;
            _inputSystem.Player.Boost.canceled += OnBoostCanceled;
        }

        private void OnMovementCanceled(InputAction.CallbackContext obj) => 
            MovementCancelled?.Invoke();

        private void OnBoostCanceled(InputAction.CallbackContext obj) => 
            BoostReleased?.Invoke();

        private void OnBoostPerformed(InputAction.CallbackContext obj) => 
            BoostPerformed?.Invoke();

        private void OnMovementPerformed(InputAction.CallbackContext obj) => 
            MovementPerformed?.Invoke(obj.ReadValue<Vector2>());
    }
}