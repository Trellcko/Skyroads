using UnityEngine;

namespace Trell.Skyroads.Gameplay.Ship
{
    public class ShipFacade : MonoBehaviour
    {
        [field: SerializeField] public ShipCollisionEventsInvoker ShipCollisionEventsInvoker { get; private set; }
        [field: SerializeField] public ShipMovement ShipMovement { get; private set; }
    }
}