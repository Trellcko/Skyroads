using System;
using Trell.Skyroads.Infrastructure;

namespace Trell.Skyroads.Gameplay.Score
{
    public interface ICurrency : IService
    {
        int CurrentValue { get; }
        event Action CurrencyChanged;
        void Reset();
        void AddCurrency(int amount);
        void SpendCurrency(int amount);
    }
}