using FactoryBots.App.Services.Assets;
using FactoryBots.App.Services.Configs;
using FactoryBots.Game.Services.Bots.Components;
using FactoryBots.SO;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryBots.Game.Services.Bots
{
    public class BotFactory
    {
        private readonly IAppAssetProvider _assets;
        private readonly IAppConfigProvider _configs;
        private readonly Transform _parent;

        public BotFactory(IAppAssetProvider assets, IAppConfigProvider configs)
        {
            _assets = assets;
            _configs = configs;
            _parent = new GameObject("Bots").transform;
        }

        public Dictionary<string, IBot> CreateBots(List<Transform> botBasePoints, Action onBotReachedTarget)
        {
            Dictionary<string, IBot> bots = new Dictionary<string, IBot>(botBasePoints.Count);
            BotConfig botConfig = _configs.GetBotConfig();

            for (int i = 0; i < botBasePoints.Count; i++)
            {
                string botId = $"Bot {i + 1}";
                Bot bot = CreateBot(botId, botBasePoints[i], onBotReachedTarget, botConfig);
                bots.Add(botId, bot);
            }

            return bots;
        }

        private Bot CreateBot(string botId, Transform botBasePoint, Action onBotReachedTarget, BotConfig botConfig)
        {
            GameObject botObject = _assets.Instantiate(AssetPath.BOT, botBasePoint.position, _parent);
            GameObject botBase = _assets.Instantiate(AssetPath.BOT_BASE, botBasePoint.position, botBasePoint);
            BotRegistry botRegistry = botObject.GetComponent<BotRegistry>();
            BotAnimator botAnimator = botObject.GetComponent<BotAnimator>();
            BotMover botMover = botObject.GetComponent<BotMover>();
            BotEffects botEffects = botObject.GetComponent<BotEffects>();
            BotCargo botCargo = botObject.GetComponent<BotCargo>();

            botObject.name = botId;
            botRegistry.Initialize(botId);
            botAnimator.Initialize();
            botMover.Initialize(botConfig);
            botEffects.Initialize();

            BotComponents botComponents = new BotComponents(botObject, botBase.transform, botRegistry, botAnimator, botMover, botEffects, botCargo);
            Bot bot = new Bot(botComponents, onBotReachedTarget);
            return bot;
        }
    }
}
