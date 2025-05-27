using System;
using Trell.Skyroads.Infrastructure.Saving;
using UnityEngine;

namespace Trell.Skyroads.Gameplay.Score
{
    public class Score : IScore
    {
        public int CurrentValue { get; private set; }
        public int HighValue { get; private set; }
        
        public event Action CurrencyChanged;
        public event Action HighValueChanged;
        
        public void Reset()
        {
            CurrentValue = 0;
        }

        public void AddCurrency(int amount)
        {
            CurrentValue += amount;
            if (CurrentValue > HighValue)
            {
                HighValue = CurrentValue;
                HighValueChanged?.Invoke();
            }
            CurrencyChanged?.Invoke();
        }

        public void SpendCurrency(int amount)
        {
            CurrentValue = Mathf.Clamp(CurrentValue - amount, 0, int.MaxValue);
            CurrencyChanged?.Invoke();
        }

        public void Save(SaveData saveData)
        {
            saveData.HighScore = HighValue;
        }

        public void Read(SaveData saveData)
        {
            HighValue = saveData.HighScore;
            HighValueChanged?.Invoke();
        }
    }
}