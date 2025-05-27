using System.Threading.Tasks;
using Trell.Skyroads.Gameplay.Score;
using Trell.Skyroads.Gameplay.Ship;
using Trell.Skyroads.Infrastructure.Factories;
using Trell.Skyroads.Infrastructure.Saving;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure.States
{
    public class GameLoopState : BaseStateWithoutPayload
    {
        private readonly IGameFactory _gameFactory;
        private readonly ISaveService _saveService;
        private readonly IScoreContainer _scoreContainer;
        private ShipFacade _ship;
        private bool _scoreBeated;

        public GameLoopState(StateMachine machine, IGameFactory gameFactory, ISaveService saveService, IScoreContainer scoreContainer) : base(machine)
        {
            _gameFactory = gameFactory;
            _saveService = saveService;
            _scoreContainer = scoreContainer;
        }

        public override void Enter()
        {
            if (!_gameFactory.SpawnedShip)
                _gameFactory.ShipCreated += OnShipCreated;
            else
            {
                OnShipCreated(_gameFactory.SpawnedShip);
            }
            _scoreContainer.Score.HighValueChanged += OnHighValueChanged;
        }

        private void OnHighValueChanged()
        {
            _scoreBeated = true;
        }

        public override void Exit()
        {
            _gameFactory.ShipCreated -= OnShipCreated;
            if (_ship)
                _ship.ShipCollisionEventsInvoker.AsteroidCollided -= OnAsteroidCollided;
        }

        private void OnAsteroidCollided()
        {
            _saveService.Save();
            bool temp = _scoreBeated;
            _scoreBeated = false;
            GoToState<LostState, bool>(temp);
        }

        private void OnShipCreated(ShipFacade obj)
        {
            _ship = obj;
            _ship.ShipCollisionEventsInvoker.AsteroidCollided += OnAsteroidCollided;
        }
    }
}