namespace Trell.Skyroads.Infrastructure.States
{
    public class InitState : BaseStateWithoutPayload
    {
        private ICoroutineRunnerService _coroutineRunner;
        private StateMachine _machine;
        private readonly ServiceLocator _serviceLocator;

        public InitState(StateMachine machine, ServiceLocator serviceLocator, ICoroutineRunnerService coroutineRunner) : base(machine)
        {
            _machine = machine;
            _serviceLocator = serviceLocator;
            _coroutineRunner = coroutineRunner;
            RegisterServices();
        }

        public override void Enter()
        {
            GoToState<LoadGameState>();
        }

        private void RegisterServices()
        {
            _serviceLocator.Register(_coroutineRunner);
            _serviceLocator.Register<ISceneService>(new SceneService(_coroutineRunner));
        }
    }
}