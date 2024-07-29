using UnityEngine;

namespace FactoryBots.Game.Services.Buildings
{
    public class Box : MonoBehaviour
    {
        public int Id { get; private set; }

        public void SetId(int boxId) => 
            Id = boxId;
    }
}
