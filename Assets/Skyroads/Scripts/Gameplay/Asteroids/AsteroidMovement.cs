using Trell.Skyroads.Infrastructure;
using Trell.Skyroads.Infrastructure.Input;
using UnityEngine;

namespace Trell.Skyroads.Gameplay.Asteroid
{
    public class AsteroidMovement : MonoBehaviour
    {
        private float _baseSpeed;
        private float _boostSpeed;
        private float _baseAngularSpeed;
        private float _boostAngularSpeed;

        [SerializeField] private float _speed;
        [SerializeField] private float _angularSpeed;

        private IInputService _inputService;
        
        private void Awake()
        {
            _inputService = ServiceLocator.Instance.Get<IInputService>();
        }

        private void OnEnable()
        {
            _inputService.BoostPerformed += OnBoostPerformed;
            _inputService.BoostReleased += OnBoostReleased;
        }

        private void OnDisable()
        {
            _inputService.BoostPerformed -= OnBoostPerformed;
            _inputService.BoostReleased -= OnBoostReleased;
        }

        private void Update()
        {
            MoveDown();
            Rotate();
        }

        public void SetSpeed(float baseSpeed, float baseAngularSpeed, float boostModificator)
        {
            _speed = _baseSpeed = baseSpeed;
            _boostSpeed = boostModificator * baseSpeed;
            _angularSpeed = _baseAngularSpeed = baseAngularSpeed;
            _boostAngularSpeed = boostModificator * baseAngularSpeed;
            if (_inputService.IsBoosted)
            {
                OnBoostPerformed();
            }
        }

        private void Rotate()
        {
            transform.Rotate(Vector3.forward, _angularSpeed * Time.deltaTime);
        }

        private void MoveDown()
        {
            transform.position += _speed * Time.deltaTime * Vector3.back;
        }

        private void OnBoostReleased()
        {
            _speed = _baseSpeed;
            _angularSpeed = _baseAngularSpeed;
        }

        private void OnBoostPerformed()
        {
            _speed = _boostSpeed;
            _angularSpeed = _boostAngularSpeed;
        }
    }
}