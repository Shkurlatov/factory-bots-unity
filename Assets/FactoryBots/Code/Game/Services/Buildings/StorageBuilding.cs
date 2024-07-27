using FactoryBots.Game.Services.Bots;
using UnityEngine;

namespace FactoryBots.Game.Services.Buildings
{
    public class StorageBuilding : BaseBuilding
    {
        public override void Initialize(string buildingId, BoxFactory boxFactory)
        {
            base.Initialize(buildingId, boxFactory);
            BoxFactory.GetBox(_deliveryPoint.position);
        }

        public override void Interact(Bot bot)
        {
            Debug.Log("Interact with storage.");
        }
    }
}
