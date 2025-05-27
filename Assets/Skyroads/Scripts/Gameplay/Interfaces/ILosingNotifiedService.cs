using System;
using Trell.Skyroads.Infrastructure;

namespace Trell.Skyroads.Gameplay
{
    public interface ILosingNotifiedService : IService
    {
        event Action Lost;
        void InvokeLost();
    }
}