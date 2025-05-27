using System.Threading.Tasks;
using Trell.Skyroads.Infrastructure.Saving;
using Trell.Skyroads.Infrastructure.States;

namespace Trell.Skyroads.Infrastructure.States
{
    public class LoadProgressState : BaseStateWithoutPayload
    {
        private readonly ISaveService _saveService;
        private readonly IPersistantPlayerProgressService _persistantPlayerProgress;

        public LoadProgressState(StateMachine machine, ISaveService saveService, IPersistantPlayerProgressService persistantPlayerProgress) : base(machine)
        {
            _persistantPlayerProgress = persistantPlayerProgress;
            _saveService = saveService;
        }

        public override void Enter()
        {
            _persistantPlayerProgress.SaveData = _saveService.Load() ?? InitNew();
            GoToState<LoadGameState>();
        }

        private static SaveData InitNew() => 
            new()
            {
                HighScore = 0
            };
    }
}