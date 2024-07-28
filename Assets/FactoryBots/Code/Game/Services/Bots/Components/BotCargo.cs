using FactoryBots.Game.Services.Buildings;
using UnityEngine;

namespace FactoryBots.Game.Services.Bots.Components
{
    public class BotCargo : MonoBehaviour
    {
        [SerializeField] private Transform _cargoPoint;

        private Box _box;

        public bool IsLoaded => _box != null;

        public void SetBox(Box box)
        {
            _box = box;
            _box.transform.SetParent(_cargoPoint);
            _box.transform.SetPositionAndRotation(_cargoPoint.position, _cargoPoint.rotation);
        }

        public bool TryRetrieveBox(out Box box)
        {
            box = _box;

            if (box == null)
            {
                return false;
            }

            _box = null;
            return true;
        }
    }
}
