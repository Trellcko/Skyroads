using System.Collections.Generic;
using Trell.Skyroads.Extra;
using Trell.Skyroads.Gameplay.Ship;
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

        [SerializeField] private AsteroidSpawnerData _asteroidSpawnerData;
        
        [SerializeField] private Vector2 _asteroidSafeZoneX;
        [SerializeField] private float _safeZoneWeight;
        [SerializeField] private float _offsetBetweenSafeZones;
        [SerializeField] private float _spawnZ;
        [SerializeField] private float _spawnY;
        [SerializeField] private float _spawnXOffset;

        private BetterTimer _spawnTimer;
        private BetterTimer _changeSpawnTimeTimer;
        private BetterTimer _countChanceSpawnIncreasingTimer;

        private float _spawnTime;
        
        private const int MaxAttemptToSpawnAsteroid = 100;

        private IStaticDataService _staticDataService;
        private IInputService _inputService;
        private IGameFactory _gameFactory;

        private List<float> _countChanceSpawn = new();
        private ILosingNotifiedService _losingNotifiedService;

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
            
            _losingNotifiedService = ServiceLocator.Instance.Get<ILosingNotifiedService>();
            _losingNotifiedService.Lost += OnLost;
            
            _spawnTime = _asteroidSpawnerData.TimeBetweenAsteroidSpawnsMinMax.y;
            _spawnTimer = new(_spawnTime, loop: true, playAwake: true);
            _changeSpawnTimeTimer = new(_asteroidSpawnerData.TimeToChangeSpawnTimeBetweenAsteroids, loop: true,
                playAwake: true);
            _countChanceSpawnIncreasingTimer = new(_asteroidSpawnerData.TimeToChangCountChanceSpawn, loop: true,
                playAwake: true);
        }

        private void Start()
        {
            InitCountChanceSpawn();
        }

        private void OnEnable()
        {
            _losingNotifiedService.Lost -= OnLost;
            _losingNotifiedService.Lost += OnLost;
            _countChanceSpawnIncreasingTimer.Completed += OnCountChanceSpawnIncreasingTimerCompleted;
            _changeSpawnTimeTimer.Completed += OnChangeSpawnTimeTimerCompleted;
            _spawnTimer.Completed += SpawnAsteroids;
            _inputService.BoostPerformed += OnBoostPerformed;
            _inputService.BoostReleased += OnBoostReleased;
        }

        private void OnDisable()
        {
            _losingNotifiedService.Lost -= OnLost;
            _countChanceSpawnIncreasingTimer.Completed -= OnCountChanceSpawnIncreasingTimerCompleted;
            _spawnTimer.Completed -= SpawnAsteroids;
            _inputService.BoostPerformed -= OnBoostPerformed;
            _inputService.BoostReleased -= OnBoostReleased;

        }

        private void Update()
        {
            _countChanceSpawnIncreasingTimer.Tick();
            _changeSpawnTimeTimer.Tick();
            _spawnTimer.Tick();
        }

        private void InitCountChanceSpawn()
        {
            _countChanceSpawn = new() { 1f };
        
            for (int i = 1; i < _asteroidSpawnerData.AsteroidCountChanceSpawnIncreasing.Count; i++)
            {
                _countChanceSpawn.Add(0);
            }
        }

        private void SpawnAsteroids()
        {
            RecalculateSafeZonePosition();

            int needToSpawn = CalculateSpawnCount();
            for (int i = 0; i < needToSpawn; i++)
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

        private int CalculateSpawnCount()
        {
            for (int i = _countChanceSpawn.Count - 1; i >= 0 ; i--)
            {
                float chance = Random.Range(0f, 1f);
                if (chance < _countChanceSpawn[i])
                {
                    Debug.Log(chance + " " + _countChanceSpawn[i] + " "+ (i+1));
                    return i + 1;
                }
            }

            return 1;
        }

        private void RecalculateSafeZonePosition()
        {
            float previousSafeZoneXPositon = (_asteroidSafeZoneX.x + _asteroidSafeZoneX.y) / 2;
            float newSafeZoneXPosition = previousSafeZoneXPositon + Random.Range(-_safeZoneWeight, _safeZoneWeight);
            _asteroidSafeZoneX = new(newSafeZoneXPosition - _safeZoneWeight,
                newSafeZoneXPosition + _safeZoneWeight);
        }

        private void OnChangeSpawnTimeTimerCompleted()
        {
            _spawnTime = Mathf.Clamp(_spawnTime - _asteroidSpawnerData.SpawnIntervalStep,
                _asteroidSpawnerData.TimeBetweenAsteroidSpawnsMinMax.x,
                _asteroidSpawnerData.TimeBetweenAsteroidSpawnsMinMax.y);
            
            _spawnTimer.SetTime(_spawnTime);
        }

        private void OnCountChanceSpawnIncreasingTimerCompleted()
        {
            for (int i = 0; i < _countChanceSpawn.Count; i++)
            {
                _countChanceSpawn[i] += _asteroidSpawnerData.AsteroidCountChanceSpawnIncreasing[i];
            }
        }

        private void OnBoostReleased()
        {
            _countChanceSpawnIncreasingTimer.SetSpeed(1);
            _spawnTimer.SetSpeed(1);
            _changeSpawnTimeTimer.SetSpeed(1);
        }

        private void OnBoostPerformed()
        {
            _countChanceSpawnIncreasingTimer.SetSpeed(_staticDataService.GetTimeData().BoostTimeSpeed);
            _spawnTimer.SetSpeed(_staticDataService.GetTimeData().BoostTimeSpeed);
            
            _changeSpawnTimeTimer.SetSpeed(_staticDataService.GetTimeData().BoostTimeSpeed);
        }

        private void OnLost()
        {
            Debug.Log("Stop");
            _spawnTimer.Pause();
            _changeSpawnTimeTimer.Pause();
            _countChanceSpawnIncreasingTimer.Pause();
            gameObject.SetActive(false);
        }
    }
}