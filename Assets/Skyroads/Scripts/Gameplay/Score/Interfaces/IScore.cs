using System;
using Trell.Skyroads.Infrastructure.Saving;

namespace Trell.Skyroads.Gameplay.Score
{
    public interface IScore : ICurrency, ISavable
    {
        public int HighValue { get; }
        
        event Action HighValueChanged;
    }
}