﻿using Trell.Skyroads.Infrastructure.AssetManagment;
using Trell.Skyroads.Infrastructure.Factories;
using Trell.Skyroads.Infrastructure.Input;

namespace Trell.Skyroads.Infrastructure.States
{
    public class BootstrapState : BaseStateWithoutPayload
    {
        private ISceneService _sceneService;

        private readonly ICoroutineRunnerService _coroutineRunner;
        private readonly ServiceLocator _serviceLocator;
        private readonly IStaticDataService _staticDataService;
        
        public BootstrapState(StateMachine machine, ServiceLocator serviceLocator, ICoroutineRunnerService coroutineRunner,
            IStaticDataService staticDataService) : base(machine)
        {
            _serviceLocator = serviceLocator;
            _coroutineRunner = coroutineRunner;
            _staticDataService = staticDataService;
            RegisterServices();
        }

        public override void Enter()
        {
            _sceneService.Load(Constants.SceneNames.BootstrapScene, GoToState<LoadGameState>);
        }

        private void RegisterServices()
        {
            _sceneService = new SceneService(_coroutineRunner);
            _serviceLocator.Register(_coroutineRunner);
            _serviceLocator.Register(_sceneService);
            _serviceLocator.Register(_staticDataService);
            AssetProvider assetProvider = new AssetProvider();
            _serviceLocator.Register<IAssetProvider>(assetProvider);
            _serviceLocator.Register<IGameFactory>(new GameFactory(assetProvider, _staticDataService));
            _serviceLocator.Register<IInputService>(new StandaloneInput());
        }
    }
}