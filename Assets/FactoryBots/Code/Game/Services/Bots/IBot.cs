using FactoryBots.Game.Services.Buildings;
using UnityEngine;

namespace FactoryBots.Game.Services.Bots
{
    public interface IBot
    {
        string Status { get; }

        bool IsCloseToBase();
        void Select();
        void Unselect();
        void ExecuteBaseCommand();
        void ExecutePositionCommand(Vector3 targetPosition, bool isModifiedCommand);
        void ExecuteDeliveryCommand(IBuilding targetBuilding, bool isModifiedCommand);
        void ExecutePreviousCommand();
        void Cleanup();
    }
}
