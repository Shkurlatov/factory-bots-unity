using FactoryBots.Game.Services.Buildings;
using System;
using UnityEngine;

namespace FactoryBots.Game.Services.Bots
{
    public interface IBot
    {
        string Status { get; }
        bool IsCloseToBase { get; }

        event Action TargetReachedAction;

        void Select();
        void Unselect();
        void MoveToPosition(Vector3 targetPosition);
        void MoveToBuilding(IBuilding targetBuilding);
        void MoveToBase();
        void ReturnToTarget();
    }
}
