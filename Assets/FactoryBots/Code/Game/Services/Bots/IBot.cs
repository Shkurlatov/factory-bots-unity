using FactoryBots.Game.Services.Buildings;
using UnityEngine;

namespace FactoryBots.Game.Services.Bots
{
    public interface IBot
    {
        string Status { get; }

        void MoveToPosition(Vector3 targetPosition);
        void MoveToBuilding(IBuilding targetBuilding);
    }
}
