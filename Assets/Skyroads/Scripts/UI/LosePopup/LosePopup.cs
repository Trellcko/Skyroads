using System;
using UnityEngine;

namespace Trell.Skyroads.UI
{
    public class LosePopup :  MonoBehaviour
    {
        [SerializeField] private ButtonWrapper _buttonWrapper;

        [SerializeField] private GameObject _congratulationText;
        
        public event Action LevelRestartRequired;

        private void OnEnable()
        {
            _buttonWrapper.Clicked += OnClicked;
        }

        private void OnDisable()
        {
            _buttonWrapper.Clicked -= OnClicked;
        }

        public void ShowCongratulationText()
        {
            _congratulationText.SetActive(true);
        }
        
        private void OnClicked()
        {
            LevelRestartRequired?.Invoke();
        }
    }
}