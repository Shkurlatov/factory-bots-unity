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

        public List<IBot> CreateBots(List<Transform> botBasePoints)
        {
            List<IBot> bots = new List<IBot>();

            for (int i = 0; i < botBasePoints.Count; i++)
            {
                GameObject botBase = _assets.Instantiate(AssetPath.BOT_BASE, botBasePoints[i].position, botBasePoints[i]);

                Bot bot = _assets.Instantiate(AssetPath.BOT, botBasePoints[i].position, _parent).GetComponent<Bot>();
                bot.Initialize($"Bot {i + 1}", botBase);
                bots.Add(bot);
            }

            return bots;
        }
    }
}
