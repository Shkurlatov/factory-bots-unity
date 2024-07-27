using FactoryBots.Game.Services.Bots;
using UnityEngine;

namespace FactoryBots.Game.Services.Buildings
{
    public class FactoryBuilding : BaseBuilding
    {
        public override void Interact(Bot bot)
        {
            Debug.Log("Interact with factory.");
        }
    }
}
