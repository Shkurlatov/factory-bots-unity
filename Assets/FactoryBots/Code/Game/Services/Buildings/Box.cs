using UnityEngine;

namespace FactoryBots.Game.Services.Buildings
{
    public class Box : MonoBehaviour
    {
        public int BoxId { get; private set; }

        public void SetId(int boxId) => 
            BoxId = boxId;
    }
}
