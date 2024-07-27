using FactoryBots.Game.Services.Bots;
using UnityEngine;

namespace FactoryBots.Game.Services.Buildings
{
    public abstract class BaseBuilding : MonoBehaviour, IBuilding
    {
        protected BoxFactory BoxFactory { get; private set; }

        public void Initialize(BoxFactory boxFactory)
        {
            BoxFactory = boxFactory;
        }

        public abstract void Interact(Bot bot);
    }
}
