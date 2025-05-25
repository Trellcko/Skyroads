using Trell.Skyroads.Infrastructure.States;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure
{
    public class GameBehaviour : MonoBehaviour, ICoroutineRunnerService
    {
        private readonly StateMachine _stateMachine = new();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            InitGameStateMachine();
        }

        private void InitGameStateMachine()
        {
            Debug.ClearDeveloperConsole();
            _stateMachine.AddState(
                new BootstrapState(_stateMachine, ServiceLocator.Instance,this),
                new LoadGameState(_stateMachine, ServiceLocator.Instance.Get<ISceneService>()),
                new GameLoopState(_stateMachine));

            _stateMachine.SetState<BootstrapState>();
        }
    }
}
