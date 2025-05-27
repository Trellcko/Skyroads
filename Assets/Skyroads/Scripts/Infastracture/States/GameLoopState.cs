using System.Threading.Tasks;
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
        private ShipFacade _ship;

        public GameLoopState(StateMachine machine, IGameFactory gameFactory, ISaveService saveService) : base(machine)
        {
            _gameFactory = gameFactory;
            _saveService = saveService;
        }

        public override void Enter()
        {
            if (!_gameFactory.SpawnedShip)
                _gameFactory.ShipCreated += OnShipCreated;
            else
            {
                OnShipCreated(_gameFactory.SpawnedShip);
            }
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
            GoToState<LooseState>();
        }

        private void OnShipCreated(ShipFacade obj)
        {
            _ship = obj;
            _ship.ShipCollisionEventsInvoker.AsteroidCollided += OnAsteroidCollided;
        }
    }
}