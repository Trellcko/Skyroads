using System;

namespace Trell.Skyroads.Gameplay
{
    public class LosingNotifiedService : ILosingNotifiedService
    {
        public event Action Lost;
        
        public void InvokeLost() => Lost?.Invoke();
    }
}