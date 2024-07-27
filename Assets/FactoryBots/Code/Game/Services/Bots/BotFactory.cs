using FactoryBots.App.Services.Assets;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryBots.Game.Services.Bots
{
    public class BotFactory
    {
        private readonly IAppAssetProvider _assets;
        private readonly Transform _parent;

        public BotFactory(IAppAssetProvider assets)
        {
            _assets = assets;
            _parent = new GameObject("Bots").transform;
        }

        public List<Bot> CreateBots(List<Transform> botBasePoints)
        {
            List<Bot> bots = new List<Bot>();

            foreach (Transform botBasePoint in botBasePoints)
            {
                GameObject botBase = _assets.Instantiate(AssetPath.BOT_BASE, botBasePoint.position, botBasePoint);

                Bot bot = _assets.Instantiate(AssetPath.BOT, botBasePoint.position, _parent).GetComponent<Bot>();
                bots.Add(bot);
            }

            return bots;
        }
    }
}
