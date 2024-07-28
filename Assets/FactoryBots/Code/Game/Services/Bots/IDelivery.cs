using FactoryBots.Game.Services.Buildings;

namespace FactoryBots.Game.Services.Bots
{
    public interface IDelivery
    {
        bool TrySetBox(Box box, string buildingId);
        bool TryRetrieveBox(out Box box);
    }
}
