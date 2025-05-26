using UnityEngine;

[CreateAssetMenu(fileName = "new TimeData", menuName = "Gameplay/TimeData")]
public class TimeData : ScriptableObject
{
    [field: SerializeField] public float BoostTimeSpeed { get; private set; } = 2f;
}
