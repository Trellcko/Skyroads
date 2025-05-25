using UnityEngine;

namespace Trell.Skyroads.Gameplay.Environment
{
    [CreateAssetMenu(fileName = "FloorTextureUpdaterData", menuName = "Environment/FloorTextureUpdater", order = 1)]
    public class FloorTextureUpdaterData : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float BoostSpeed { get; private set; }
        [field: SerializeField] public Vector2 Direction { get; private set; }
    }
}