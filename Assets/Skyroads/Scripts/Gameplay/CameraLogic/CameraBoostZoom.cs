using DG.Tweening;
using Trell.Skyroads.Infrastructure;
using Trell.Skyroads.Infrastructure.Input;
using UnityEngine;

namespace Trell.Skyroads.Gameplay.CameraLogic
{
    public class CameraBoostZoom : MonoBehaviour
    {
        [SerializeField] private CameraBoostZoomData _data;
        [SerializeField] private Camera _camera;

        private IInputService _inputService;

        private void Awake()
        {
            _inputService = ServiceLocator.Instance.Get<IInputService>();
        }

        private void OnEnable()
        {
            _inputService.BoostPerformed += ZoomIn;
            _inputService.BoostReleased += ZoomOut;
        }

        private void OnDisable()
        {
            _inputService.BoostPerformed -= ZoomIn;
            _inputService.BoostReleased -= ZoomOut;
        }

        private void ZoomOut()
        {
            float duration = (_data.BaseFieldOfView - _camera.fieldOfView) * _data.Delta;
            _camera.DOKill();
            _camera.transform.DOKill();
            _camera.transform.DOMoveZ(_data.BasePositionZ, duration);
            _camera.DOFieldOfView(_data.BaseFieldOfView, duration);
        }

        private void ZoomIn()
        {
            float duration = (_camera.fieldOfView - _data.BoostFieldOfView) * _data.Delta;
            _camera.DOKill();
            _camera.transform.DOKill();
            _camera.transform.DOMoveZ(_data.BoostPositionZ, duration);
            _camera.DOFieldOfView(_data.BoostFieldOfView, duration);
        }
    }
}