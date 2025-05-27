using System;

namespace Trell.Skyroads.Gameplay.Score
{
    public class Currency : ICurrency
    {
        public int CurrentValue { get; private set; }
        public event Action CurrencyChanged;
        public void Reset()
        {
            CurrentValue = 0;
        }

        public void AddCurrency(int amount)
        {
            CurrentValue += amount;
            CurrencyChanged?.Invoke();
        }

        public void SpendCurrency(int amount)
        {
            CurrentValue -= amount;
            CurrencyChanged?.Invoke();
        }
    }
}