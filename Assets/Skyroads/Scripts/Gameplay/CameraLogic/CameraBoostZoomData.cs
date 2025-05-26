using UnityEngine;

namespace Trell.Skyroads.Gameplay.CameraLogic
{
    [CreateAssetMenu(fileName = "new CameraBoostZoom", menuName = "Camera/BoostZoom", order = 1)]
    public class CameraBoostZoomData : ScriptableObject
    {
        [field: SerializeField] public float BaseFieldOfView { get; private set; }
        [field: SerializeField] public float BoostFieldOfView { get; private set; }
        [field: SerializeField] public float BoostPositionZ { get; private set; }
        [field: SerializeField] public float BasePositionZ { get; private set; }
        [field: SerializeField] public float Delta { get; private set; }
    }
}