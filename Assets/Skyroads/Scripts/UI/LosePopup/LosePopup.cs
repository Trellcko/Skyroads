using System;
using UnityEngine;

namespace Trell.Skyroads.UI
{
    public class LosePopup :  MonoBehaviour
    {
        [SerializeField] private ButtonWrapper _buttonWrapper;

        public event Action LevelRestartRequired;

        private void OnEnable()
        {
            _buttonWrapper.Clicked += OnClicked;
        }

        private void OnDisable()
        {
            _buttonWrapper.Clicked -= OnClicked;
        }

        private void OnClicked()
        {
            LevelRestartRequired?.Invoke();
        }
    }
}