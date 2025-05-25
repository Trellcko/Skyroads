using Constants;

namespace Trell.Skyroads.Infrastructure.States
{
    public class LoadGameState : BaseStateWithoutPayload
    {
        private readonly ISceneService _sceneService;

        public LoadGameState(StateMachine machine, ISceneService sceneService) : base(machine)
        {
            _sceneService = sceneService;
        }

        public override void Enter()
        {
            _sceneService.Load(SceneNames.GameScene);
            GoToState<GameLoopState>();   
        }
    }
}