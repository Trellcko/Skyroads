using System;
using DG.Tweening;
using Trell.Skyroads.Infrastructure;
using Trell.Skyroads.Infrastructure.Input;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Trell.Skyroads.Gameplay.Effects
{
    public class BoostGlobalVolumeController : MonoBehaviour
    {
        [SerializeField] private Volume _volume;
        [SerializeField] private BoostGlobalVolumeControllerData _data;
        
        private IInputService _inputService;
        private ChromaticAberration _chromaticAberration;

        private Tween _tween;
        
        private void Awake()
        {
            _inputService = ServiceLocator.Instance.Get<IInputService>();
            _volume.profile.TryGet(out _chromaticAberration);
            
        }

        private void OnEnable()
        {
            _inputService.BoostPerformed += OnBoostPerformed;
            _inputService.BoostReleased += OnBoostReleased;
        }

        private void OnDisable()
        {
            _inputService.BoostPerformed += OnBoostPerformed;
            _inputService.BoostReleased += OnBoostReleased;
        }

        private void OnBoostReleased()
        {
            _tween?.Kill();

            float duration = _chromaticAberration.intensity.value * _data.Delta;

            _tween = DOTween.To(() => _data.MaxIntensity, x=> _chromaticAberration.intensity.value = x, 0, duration);
        }


        private void OnBoostPerformed()
        {
            _tween?.Kill();
            float duration = _data.MaxIntensity * _data.Delta;
            _tween = DOTween.To(() => 0, x=> _chromaticAberration.intensity.value = x, _data.MaxIntensity, duration);
        }
    }
}