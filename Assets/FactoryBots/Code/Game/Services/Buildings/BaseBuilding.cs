using FactoryBots.Game.Services.Bots;
using UnityEngine;

namespace FactoryBots.Game.Services.Buildings
{
    public abstract class BaseBuilding : MonoBehaviour, IBuilding
    {
        [SerializeField] protected Transform _deliveryPoint;
        [SerializeField] protected Transform _insidePoint;

        [SerializeField] private Transform _interactionPoint;

        protected readonly float _conveyorSpeed = 1f;

        public string Id { get; private set; }

        protected BoxFactory BoxFactory { get; private set; }

        public Vector3 InteractionPosition => _interactionPoint.position;

        public virtual void Initialize(string buildingId, BoxFactory boxFactory)
        {
            Id = buildingId;
            BoxFactory = boxFactory;
        }

        public abstract void Interact(IDelivery delivery);
    }
}
