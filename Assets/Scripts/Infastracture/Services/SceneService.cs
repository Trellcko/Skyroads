using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Trell.Skyroads.Infrastructure
{
    public class SceneService : ISceneService
    {
        private readonly ICoroutineRunnerService _coroutineRunner;
        public string CurrentScene => SceneManager.GetActiveScene().name;

        public SceneService(ICoroutineRunnerService coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        
        public void Load(string sceneName, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(sceneName, onLoaded));

        private IEnumerator LoadScene(string sceneName, Action onLoaded = null)
        {
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(sceneName);

            while (!waitNextScene.isDone)
            {
                yield return null;
            }

            onLoaded?.Invoke();
        }
    }
}