using FactoryBots.Game.Services.Buildings;
using FactoryBots.Game.Services.Parking;
using System.Collections.Generic;

namespace FactoryBots.Game.Services.Bots
{
    public class BotManager : IGameBots
    {
        private readonly IGameParking _parking;
        private readonly IGameBuildings _buildings;
        private readonly BotFactory _botFactory;

        private List<Bot> _bots;

        public BotManager(IGameParking parking, IGameBuildings buildings, BotFactory botFactory)
        {
            _botFactory = botFactory;
            _parking = parking;
            _buildings = buildings;
        }

        public void Initialize()
        {
            CreateBots();
        }

        private void CreateBots()
        {
            _bots = _botFactory.CreateBots(_parking.BotBasePoints);
        }

        public void Cleanup()
        {

        }
    }
}
