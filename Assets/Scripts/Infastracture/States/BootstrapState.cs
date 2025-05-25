namespace Trell.Skyroads.Infrastructure.States
{
    public class BootstrapState : BaseStateWithoutPayload
    {
        private ICoroutineRunnerService _coroutineRunner;
        private readonly ServiceLocator _serviceLocator;
        private ISceneService _sceneService;

        public BootstrapState(StateMachine machine, ServiceLocator serviceLocator, ICoroutineRunnerService coroutineRunner) : base(machine)
        {
            _serviceLocator = serviceLocator;
            _coroutineRunner = coroutineRunner;
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
            _serviceLocator.Register<IInputService>(new StandaloneInput());
        }
    }
}