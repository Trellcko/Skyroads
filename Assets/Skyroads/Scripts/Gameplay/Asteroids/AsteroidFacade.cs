using UnityEngine;

namespace Trell.Skyroads.Gameplay.Asteroid
{
    public class AsteroidFacade : MonoBehaviour
    {
        [field: SerializeField] public AsteroidMovement AsteroidMovement { get; private set; }
        [field: SerializeField] public AsteroidReturnToThePool AsteroidReturnToThePool { get; private set; }
        [field: SerializeField] public AsteroidCollisionEventInvoker CollisionEventInvoker { get; private set; }
    }
}