using Trell.Skyroads.Infrastructure;

namespace Trell.Skyroads.Infrastructure.States
{
    public abstract class BaseStateWithoutPayload : BaseState
    {
        protected BaseStateWithoutPayload(StateMachine machine) : base(machine)
        {

        }

        public virtual void Enter()
        {
        }

    }
}