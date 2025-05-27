using UnityEngine;

namespace Trell.Skyroads.Gameplay.Score
{
    [CreateAssetMenu(menuName = "Gameplay/Score", fileName = "new ScoreData", order = 1)]
    public class ScoreData : ScriptableObject
    {
        [field: SerializeField] public int BaseScore { get; private set; } = 1;
        [field: SerializeField] public int ScoreForPassingAsteroids { get; private set; } = 5;
    }
}