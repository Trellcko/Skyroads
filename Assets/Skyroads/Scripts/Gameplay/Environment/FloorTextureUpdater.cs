using Trell.Skyroads.Infrastructure;
using Trell.Skyroads.Infrastructure.Input;
using UnityEngine;

namespace Trell.Skyroads.Gameplay.Environment
{
    public class FloorTextureUpdater : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private FloorTextureUpdaterData _floorTextureUpdaterData;
        
        private IStaticDataService _staticDataService;
        private IInputService _inputService;
        private Material _floorMaterial;
        
        private Vector2 _offset;
        private float _speed;
        
        private const string Maintex = "_BaseMap";

        private void Awake()
        {
            _speed = _floorTextureUpdaterData.Speed;
            _floorMaterial = _meshRenderer.sharedMaterial;
            _staticDataService = ServiceLocator.Instance.Get<IStaticDataService>();
            _inputService = ServiceLocator.Instance.Get<IInputService>();
        }

        private void OnEnable()
        {
            _inputService.BoostPerformed += OnBoostPerformed;
            _inputService.BoostReleased += OnBoostReleased;
        }

        private void OnDisable()
        {
            _inputService.BoostPerformed -= OnBoostPerformed;
            _inputService.BoostReleased -= OnBoostReleased;
        }

        private void Update()
        {
            _offset += _floorTextureUpdaterData.Direction * _speed;
            _floorMaterial.SetTextureOffset(Maintex, _offset);
        }

        private void OnBoostReleased()
        {
            _speed = _floorTextureUpdaterData.Speed;
        }

        private void OnBoostPerformed()
        {
            _speed = _staticDataService.GetTimeData().BoostTimeSpeed * _floorTextureUpdaterData.Speed;
        }
    }
}