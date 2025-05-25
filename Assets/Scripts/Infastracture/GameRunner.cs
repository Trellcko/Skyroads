using UnityEngine;
using UnityEngine.Serialization;

namespace Trell.Skyroads.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBehaviour _prefab;

        private void Awake()
        {
            GameBehaviour gameBehaviour = FindObjectOfType<GameBehaviour>();

            if (gameBehaviour)
                return;

            gameBehaviour = Instantiate(_prefab);
        }
    }
}