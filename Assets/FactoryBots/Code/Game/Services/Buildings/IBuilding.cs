using FactoryBots.Game.Services.Bots;
using UnityEngine;

namespace FactoryBots.Game.Services.Buildings
{
    public interface IBuilding
    {
        string Id { get; }
        Vector3 InteractionPosition { get; }

        void Interact(IDelivery delivery);
    }
}
