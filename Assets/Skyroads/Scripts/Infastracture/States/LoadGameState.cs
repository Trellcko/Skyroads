using System;
using Constants;
using Trell.Skyroads.Gameplay.Score;
using Trell.Skyroads.Infrastructure.Factories;
using Trell.Skyroads.Infrastructure.Saving;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure.States
{
    public class LoadGameState : BaseStateWithoutPayload
    {
        private readonly ISceneService _sceneService;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistantPlayerProgressService _persistantPlayerProgress;
        private IScoreContainer _scoreContainer;

        public LoadGameState(StateMachine machine, ISceneService sceneService, IGameFactory gameFactory,
            IScoreContainer scoreContainer, IPersistantPlayerProgressService persistantPlayerProgress) : base(machine)
        {
            _scoreContainer = scoreContainer;
            _gameFactory = gameFactory;
            _persistantPlayerProgress = persistantPlayerProgress;
            _sceneService = sceneService;
        }

        public override void  Enter()
        {
            _scoreContainer.Score.Reset();
            _scoreContainer.PassedMeteorites.Reset();
            _gameFactory.CleanUp();
            _sceneService.Load(SceneNames.GameScene, OnLoaded);
            _scoreContainer.Score.Read(_persistantPlayerProgress.SaveData);
        }

        private async void OnLoaded()
        {
            try
            {
                await _gameFactory.CreateShip();
                GoToState<GameLoopState>();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message + "\n" + e.StackTrace);
            }
        }
    }
}