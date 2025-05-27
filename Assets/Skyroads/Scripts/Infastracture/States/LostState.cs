using System;
using System.Threading.Tasks;
using Trell.Skyroads.Gameplay;
using Trell.Skyroads.Infrastructure.Factories;
using Trell.Skyroads.UI;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure.States
{
    public class LostState : BaseStateWithPayLoad<bool>
    {
        private readonly IGameFactory _gameFactory;
        private LosePopup _losePopup;
        private readonly ILosingNotifiedService _losingNotifiedService;

        public LostState(StateMachine machine, IGameFactory gameFactory, ILosingNotifiedService losingNotifiedService) : base(machine)
        {
            _losingNotifiedService = losingNotifiedService;
            _gameFactory = gameFactory;
        }

        public override async void Enter(bool beatTheRecord)
        {
            try
            {
                _losePopup = await _gameFactory.CreateLosePopup();
                _losePopup.LevelRestartRequired +=  GoToState<LoadGameState>;
                if (beatTheRecord)
                {
                    _losePopup.ShowCongratulationText();
                }
                _losingNotifiedService.InvokeLost();
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