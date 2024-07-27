using FactoryBots.Game.Services.Buildings;

namespace FactoryBots.Game.Services.Bots
{
    public interface IDelivery
    {
        string TargetId { get; }

        bool TrySetBox(Box box);
    }
}
