using Trell.Skyroads.Infrastructure;
using UnityEngine;

namespace Trell.Skyroads.Gameplay.Ship
{
    public class ShipMovement : MonoBehaviour
    {
        [SerializeField] private ShipData _shipData;
        
        private IInputService _inputService;

        private float _speed;
        private Vector3 _movementDirection;
        
        private void Awake()
        {
            _speed = _shipData.BaseSpeed;
            _inputService = ServiceLocator.Instance.Get<IInputService>();
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
            _inputService.BoostPerformed += OnBoostPerformed;
            _inputService.BoostReleased += OnBoostReleased;
            _inputService.MovementPerformed += OnMovementPerformed;
            _inputService.MovementCancelled += OnMovementCancelled;
        }

        private void Update()
        {
            transform.position = CalculateNextPosition();
        }

        private Vector3 CalculateNextPosition()
        {
            Vector3 position = transform.position + _speed * Time.deltaTime * _movementDirection;
            position.x = Mathf.Clamp(position.x, -_shipData.XLimit, _shipData.XLimit);
            return position;
        }

        private void OnMovementCancelled()
        {
            _movementDirection = Vector2.zero;
        }

        private void OnMovementPerformed(Vector2 obj)
        {
            Debug.Log(obj);
            _movementDirection = obj;
        }

        private void OnBoostReleased()
        {
            _speed = _shipData.BaseSpeed;
        }

        private void OnBoostPerformed()
        {
            _speed = _shipData.BoostSpeed;
        }
    }
}