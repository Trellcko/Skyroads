using System;
using System.Threading.Tasks;
using Trell.Skyroads.Infrastructure.Factories;
using Trell.Skyroads.UI;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure.States
{
    public class LooseState : BaseStateWithoutPayload
    {
        private readonly IGameFactory _gameFactory;
        private LosePopup _losePopup;

        public LooseState(StateMachine machine, IGameFactory gameFactory) : base(machine)
        {
            _gameFactory = gameFactory;
        }

        public override async void Enter()
        {
            try
            {
                _losePopup = await _gameFactory.CreateLosePopup();
                _losePopup.LevelRestartRequired +=  GoToState<LoadGameState>;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message + "\n" + e.StackTrace);
            }
        }

        public override void Exit()
        {
            if (_losePopup) 
                _losePopup.LevelRestartRequired -= GoToState<LoadGameState>;
        }
    }
}