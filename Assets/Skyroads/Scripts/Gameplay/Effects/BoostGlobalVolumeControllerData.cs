using UnityEngine;

namespace Trell.Skyroads.Gameplay.Effects
{
    [CreateAssetMenu(fileName = "new BoostGlobalVolumeeControllerData", menuName = "Effects/BoostGlobalVolumeControllerData", order = 0)]
    public class BoostGlobalVolumeControllerData : ScriptableObject
    {
        [field: SerializeField] public float MaxIntensity { get; private set; }
        [field: SerializeField] public float Delta { get; private set; }
    }
}