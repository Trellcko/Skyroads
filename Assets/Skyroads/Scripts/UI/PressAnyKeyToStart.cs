using Trell.Skyroads.Gameplay.Asteroid;
using Trell.Skyroads.Gameplay.Score;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Trell.Skyroads.UI
{
    public class PressAnyKeyToStart : MonoBehaviour
    {
        [SerializeField] private AsteroidSpawner _asteroidSpawner;
        [SerializeField] private ScoreCounter _scoreCounter;

        private void Awake()
        {
            _asteroidSpawner.gameObject.SetActive(false);
            _scoreCounter.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                _asteroidSpawner.gameObject.SetActive(true);
                _scoreCounter.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}