using FactoryBots.Game.Services.Buildings;

namespace FactoryBots.Game.Services.Bots.Commands
{
    public class BotDeliveryCommand : IBotCommand
    {
        public readonly IBuilding TargetBuilding;

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
