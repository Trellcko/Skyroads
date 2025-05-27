using System.Collections.Generic;
using UnityEngine;

namespace Trell.Skyroads.Gameplay.Asteroid
{
    [CreateAssetMenu(fileName = "new AsteroidSpawnerData", menuName = "Gameplay/Asteroid Spawner")]
    public class AsteroidSpawnerData : ScriptableObject
    {
        [field: SerializeField] public Vector2 TimeBetweenAsteroidSpawnsMinMax { get; private set; }
        [field: SerializeField] public float TimeToChangeSpawnTimeBetweenAsteroids { get; private set; }
        [field: SerializeField] public float SpawnIntervalStep { get; private set; }
        [field: SerializeField] public List<float> AsteroidCountChanceSpawnIncreasing { get; private set; }
        [field: SerializeField] public float TimeToChangCountChanceSpawn { get; private set; }
    }
}