using System;
using System.Threading.Tasks;
using Constants;
using Trell.Skyroads.Infrastructure.Factories;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure.States
{
    public class LoadGameState : BaseStateWithoutPayload
    {
        private readonly ISceneService _sceneService;
        private IGameFactory _gameFactory;

        public LoadGameState(StateMachine machine, ISceneService sceneService, IGameFactory gameFactory) : base(machine)
        {
            _gameFactory = gameFactory;
            _sceneService = sceneService;
        }

        public override void Enter()
        {
            _sceneService.Load(SceneNames.GameScene, OnLoaded);
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