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
            for (int i = 0; i < _storages.Count; i++)
            {
                _storages[i].Initialize($"Storage {i + 1}", boxFactory);
            }
            
            for (int i = 0; i < _factories.Count; i++)
            {
                _factories[i].Initialize($"Factory {i + 1}", boxFactory);
            }
        }

        public void Cleanup() { }
    }
}
