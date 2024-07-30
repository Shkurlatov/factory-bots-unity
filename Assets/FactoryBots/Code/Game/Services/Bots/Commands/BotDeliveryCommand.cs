using FactoryBots.Game.Services.Buildings;
using UnityEngine;

namespace FactoryBots.Game.Services.Bots.Commands
{
    public class BotDeliveryCommand : IBotCommand
    {
        public readonly IBuilding TargetBuilding;

        public Vector3 TargetPosition => TargetBuilding.InteractionPosition;
        public string TargetId => TargetBuilding.Id;
        public bool IsTargetReached { get; private set; }

        public BotDeliveryCommand(IBuilding targetBuilding)
        {
            TargetBuilding = targetBuilding;
            IsTargetReached = false;
        }

        public void SetTargetReached(bool _isTargetReached) =>
            IsTargetReached = _isTargetReached;
    }
}
