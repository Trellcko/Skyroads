using Trell.Skyroads.Extra;
using Trell.Skyroads.Infrastructure;
using Trell.Skyroads.Infrastructure.Factories;
using Trell.Skyroads.Infrastructure.Input;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Trell.Skyroads.Gameplay.Asteroid
{
    public class AsteroidSpawner : MonoBehaviour
    {
        [SerializeField] private bool _drawSafeZone;
        [SerializeField] private bool _drawSpawnLine;

        [SerializeField] private Vector2 _asteroidSafeZoneX;
        [SerializeField] private int _spawnAsteroidsCount;
        [SerializeField] private float _safeZoneWeight;
        [SerializeField] private float _offsetBetweenSafeZones;
        [SerializeField] private float _timeBetweenAsteroidSpawns;
        [SerializeField] private float _spawnZ;
        [SerializeField] private float _spawnY;
        [SerializeField] private float _spawnXOffset;

        private BetterTimer _spawnTimer;

        private const int MaxAttemptToSpawnAsteroid = 100;

        private IStaticDataService _staticDataService;
        private IInputService _inputService;
        private IGameFactory _gameFactory;

        #region Gizmoz
        private void OnDrawGizmos()
        {
            if (_drawSafeZone)
            {
                Gizmos.DrawCube(new(0, 0, _spawnZ), new(_safeZoneWeight, 20, 20));
            }

            if (_drawSpawnLine)
            {
                Gizmos.DrawLine(new(-_spawnXOffset, 0, _spawnZ), new(_spawnXOffset, 0, _spawnZ));
            }
        }
        #endregion
        private void Awake()
        {
            _inputService = ServiceLocator.Instance.Get<IInputService>();
            _staticDataService = ServiceLocator.Instance.Get<IStaticDataService>();
            _gameFactory = ServiceLocator.Instance.Get<IGameFactory>();
            _spawnTimer = new(_timeBetweenAsteroidSpawns, loop: true, playAwake: true);
        }

        private void OnEnable()
        {
            _spawnTimer.Completed += SpawnAsteroids;
            _inputService.BoostPerformed += OnBoostPerformed;
            _inputService.BoostReleased += OnBoostReleased;
        }

        private void OnDisable()
        {
            _spawnTimer.Completed -= SpawnAsteroids;
            _inputService.BoostPerformed -= OnBoostPerformed;
            _inputService.BoostReleased -= OnBoostReleased;

        }

        private void Update()
        {
            _spawnTimer.Tick();
        }

        private void SpawnAsteroids()
        {
            RecalculateSafeZonePosition();

            for (int i = 0; i < _spawnAsteroidsCount; i++)
            {
                if (GetAsteroidSpawnPosition(out Vector3 asteroidPosition))
                {
                    _gameFactory.CreateAsteroid(asteroidPosition);
                }
            }
        }

        private bool GetAsteroidSpawnPosition(out Vector3 spawnPosition)
        {
            spawnPosition = Vector3.zero;
            float x = 0;
            int attemptsToSpawn = 0;
            do
            {
                attemptsToSpawn++;
                x = Random.Range(-_spawnXOffset, _spawnXOffset);

                if (attemptsToSpawn > MaxAttemptToSpawnAsteroid)
                {
                    Debug.LogError("Impossible to spawn Asteroids");
                    return false;
                }

            } while (_asteroidSafeZoneX.x < x && _asteroidSafeZoneX.y > x);

            spawnPosition = new(x, _spawnY, _spawnZ);
            return true;
        }

        private void RecalculateSafeZonePosition()
        {
            float previousSafeZoneXPositon = (_asteroidSafeZoneX.x + _asteroidSafeZoneX.y) / 2;
            float newSafeZoneXPosition = previousSafeZoneXPositon + Random.Range(-_safeZoneWeight, _safeZoneWeight);
            _asteroidSafeZoneX = new(newSafeZoneXPosition - _safeZoneWeight,
                newSafeZoneXPosition + _safeZoneWeight);
        }

        private void OnBoostReleased()
        {
            _spawnTimer.SetSpeed(1);
        }

        private void OnBoostPerformed()
        {
            _spawnTimer.SetSpeed(_staticDataService.GetTimeData().BoostTimeSpeed);
        }
    }
}