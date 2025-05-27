using Trell.Skyroads.Gameplay.Score;
using Trell.Skyroads.Infrastructure.Factories;
using Trell.Skyroads.Infrastructure.Saving;
using Trell.Skyroads.Infrastructure.States;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure
{
    public class GameBehaviour : MonoBehaviour, ICoroutineRunnerService
    {
        [SerializeField] private StaticDataService _staticDataService;
        
        private readonly StateMachine _stateMachine = new();

        private ISaveService _saveService;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            InitGameStateMachine();
        }

        private void OnApplicationQuit()
        {
            _saveService.Save();
        }

        private void InitGameStateMachine()
        {
            _stateMachine.AddState(
                new BootstrapState(_stateMachine, ServiceLocator.Instance,this, _staticDataService),
                new LoadProgressState(_stateMachine, ServiceLocator.Instance.Get<ISaveService>(),
                    ServiceLocator.Instance.Get<IPersistantPlayerProgressService>()) ,
                new LoadGameState(_stateMachine, ServiceLocator.Instance.Get<ISceneService>(), 
                    ServiceLocator.Instance.Get<IGameFactory>(), ServiceLocator.Instance.Get<IScoreContainer>().Score,
                    ServiceLocator.Instance.Get<IPersistantPlayerProgressService>()),
                new GameLoopState(_stateMachine, ServiceLocator.Instance.Get<IGameFactory>(), 
                    ServiceLocator.Instance.Get<ISaveService>()),
                new LooseState(_stateMachine, ServiceLocator.Instance.Get<IGameFactory>()));
            _saveService = ServiceLocator.Instance.Get<ISaveService>();
            _stateMachine.SetState<BootstrapState>();
        }
    }
    
}
