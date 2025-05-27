using System.Threading.Tasks;
using Trell.Skyroads.Gameplay;
using Trell.Skyroads.Gameplay.Score;
using Trell.Skyroads.Infrastructure.AssetManagment;
using Trell.Skyroads.Infrastructure.Factories;
using Trell.Skyroads.Infrastructure.Input;
using Trell.Skyroads.Infrastructure.Saving;
using UnityEngine;

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
            _sceneService.Load(Constants.SceneNames.BootstrapScene, GoToState<LoadProgressState>);
        }

        private void RegisterServices()
        {
            _sceneService = new SceneService(_coroutineRunner);
            _serviceLocator.Register(_coroutineRunner);
            _serviceLocator.Register(_sceneService);
            
            AssetProvider assetProvider = new();
            _serviceLocator.Register<IAssetProvider>(assetProvider);
            
            _serviceLocator.Register(_staticDataService);
            _serviceLocator.Register<IInputService>(new StandaloneInput());
            _serviceLocator.Register<IGameFactory>(new GameFactory(assetProvider, _staticDataService));

            PersistantPlayerProgressService persistantPlayerProgressService = new();
            _serviceLocator.Register<IPersistantPlayerProgressService>(persistantPlayerProgressService);

            ScoreContainer scoreContainer = new();
            _serviceLocator.Register<IScoreContainer>(scoreContainer);
            _serviceLocator.Register<ISaveService>(new SaveService(persistantPlayerProgressService, scoreContainer.Score));
            _serviceLocator.Register<ILosingNotifiedService>(new LosingNotifiedService());
        }
    }
}