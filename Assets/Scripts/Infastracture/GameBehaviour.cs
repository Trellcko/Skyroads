using Trell.Skyroads.Infrastructure.States;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure
{
    public class GameBehaviour : MonoBehaviour
    {
        private readonly StateMachine _stateMachine = new();
        
        private void Awake()
        {
            _stateMachine.AddState(
                new InitState(_stateMachine)
                );
        }
    }

    public class InitState : BaseStateWithoutPayload
    {
        public InitState(StateMachine machine) : base(machine)
        {
            
        }
    }
}