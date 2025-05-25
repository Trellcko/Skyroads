using UnityEngine;

namespace Trell.Skyroads.Gameplay.Ship
{
    [CreateAssetMenu(fileName = "new ShipData", menuName = "Gameplay/ShipData", order = 1)]
    public class ShipData : ScriptableObject
    {
        [field: SerializeField] public float BaseSpeed { get; private set; }
        [field: SerializeField] public float BoostSpeed { get; private set; }
        [field: SerializeField] public float XLimit { get; private set; }
    }
}