using System.Collections.Generic;
using UnityEngine;

namespace FactoryBots.Game.Services.Buildings
{
    public class BuildingManager : MonoBehaviour, IGameBuildings
    {
        [SerializeField] private List<StorageBuilding> _storages;
        [SerializeField] private List<FactoryBuilding> _factories;

        public void Initialize(BoxFactory boxFactory)
        {
            foreach (StorageBuilding storage in _storages)
            {
                storage.Initialize(boxFactory);
            }

            foreach (FactoryBuilding factory in _factories)
            {
                factory.Initialize(boxFactory);
            }
        }

        public void Cleanup()
        {

        }
    }
}
