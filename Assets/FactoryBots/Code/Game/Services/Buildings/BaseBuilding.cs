using UnityEngine;

namespace FactoryBots.Game.Services.Buildings
{
    public abstract class BaseBuilding : MonoBehaviour
    {
        protected BoxFactory BoxFactory { get; private set; }

        public void Initialize(BoxFactory boxFactory)
        {
            BoxFactory = boxFactory;
        }
    }
}
