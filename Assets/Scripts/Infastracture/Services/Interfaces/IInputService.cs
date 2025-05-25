using System;
using Trell.Skyroads.Infrastructure;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure
{
    public interface IInputService : IService
    {
        event Action<Vector2> AxisUpdated;
        event Action BoostPressed;
        event Action BoostReleased;
    }
}