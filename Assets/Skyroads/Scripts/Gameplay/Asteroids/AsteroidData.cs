using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Trell.Skyroads.Gameplay.Asteroid
{
    [CreateAssetMenu(fileName = "new AsteroidData", menuName = "Gameplay/AsteroidData")]
    public class AsteroidData : ScriptableObject
    {
        [field: SerializeField] public float BaseSpeed { get; private set; }
        [field: SerializeField] public float AngularSpeed { get; private set; }
        [SerializeField] private List<AssetReference> _assetReferences;
        
        public IReadOnlyList<AssetReference> AssetReferences => _assetReferences;
    }
}