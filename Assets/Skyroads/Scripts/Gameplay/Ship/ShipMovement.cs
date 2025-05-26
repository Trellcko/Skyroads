using Trell.Skyroads.Infrastructure;
using Trell.Skyroads.Infrastructure.Input;
using UnityEngine;

namespace Trell.Skyroads.Gameplay.Ship
{
    public class ShipMovement : MonoBehaviour
    {   
        private IInputService _inputService;

        [SerializeField] private float _speed;

        private float _baseSpeed;
        private float _xLimit;
        private float _boostSpeed;
        
        private Vector3 _movementDirection;
        
        private void Awake()
        {
            _inputService = ServiceLocator.Instance.Get<IInputService>();
        }

        public void Init(float baseSpeed, float boostModificator, float xLimit)
        {
            _speed = baseSpeed;
            _baseSpeed = baseSpeed;
            _boostSpeed = boostModificator * baseSpeed;
            _xLimit = xLimit;
        }
        
        private void OnEnable()
        {
            _inputService.BoostPerformed += OnBoostPerformed;
            _inputService.BoostReleased += OnBoostReleased;
            _inputService.MovementPerformed += OnMovementPerformed;
            _inputService.MovementCancelled += OnMovementCancelled;
        }

        private void OnDisable()
        {
            _inputService.BoostPerformed -= OnBoostPerformed;
            _inputService.BoostReleased -= OnBoostReleased;
            _inputService.MovementPerformed -= OnMovementPerformed;
            _inputService.MovementCancelled -= OnMovementCancelled;
        }

        private void Update()
        {
            transform.position = CalculateNextPosition();
        }

        private Vector3 CalculateNextPosition()
        {
            Vector3 position = transform.position + _speed * Time.deltaTime * _movementDirection;
            position.x = Mathf.Clamp(position.x, -_xLimit, _xLimit);
            return position;
        }

        private void OnMovementCancelled()
        {
            _movementDirection = Vector2.zero;
        }

        private void OnMovementPerformed(Vector2 obj)
        {
            _movementDirection = obj;
        }

        private void OnBoostReleased()
        {
            _speed = _baseSpeed;
        }

        private void OnBoostPerformed()
        {
            _speed = _boostSpeed;
        }
    }
}