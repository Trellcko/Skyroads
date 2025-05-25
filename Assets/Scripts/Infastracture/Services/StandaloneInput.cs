using System;
using UnityEngine;

namespace Trell.Skyroads.Infrastructure
{
    public class StandaloneInput : IInputService
    {
        public event Action<Vector2> AxisUpdated;
        public event Action BoostPressed;
        public event Action BoostReleased;
    }
}