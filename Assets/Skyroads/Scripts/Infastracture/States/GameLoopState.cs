using System.Threading.Tasks;
using Trell.Skyroads.Gameplay.Ship;
using Trell.Skyroads.Infrastructure.Factories;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure.States
{
    public class GameLoopState : BaseStateWithoutPayload
    {
        private readonly IGameFactory _gameFactory;
        private ShipFacade _ship;

        public GameLoopState(StateMachine machine, IGameFactory gameFactory) : base(machine)
        {
            _gameFactory = gameFactory;
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
            Debug.Log("OnAsteroidCollided");
            GoToState<LooseState>();
        }

        private void OnShipCreated(ShipFacade obj)
        {
            _ship = obj;
            _ship.ShipCollisionEventsInvoker.AsteroidCollided += OnAsteroidCollided;
        }
    }
}