using System;
using Trell.Skyroads.Infrastructure;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure.Input
{
    public interface IInputService : IService
    {
        event Action<Vector2> MovementPerformed;
        event Action BoostPerformed;
        event Action BoostReleased;
        event Action MovementCancelled;
        bool IsBoosted { get; }
        void EnableInput();
        void DisableInput();
    }
}