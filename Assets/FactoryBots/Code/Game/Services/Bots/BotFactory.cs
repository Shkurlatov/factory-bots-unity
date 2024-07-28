using FactoryBots.App.Services.Assets;
using FactoryBots.Game.Services.Bots.Components;
using System;
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

        public Dictionary<string, IBot> CreateBots(List<Transform> botBasePoints, Action onBotReachedTarget)
        {
            Dictionary<string, IBot> bots = new Dictionary<string, IBot>(botBasePoints.Count);

            for (int i = 0; i < botBasePoints.Count; i++)
            {
                string id = $"Bot {i + 1}";
                Bot bot = CreateBot(id, botBasePoints[i], onBotReachedTarget);
                bots.Add(id, bot);
            }

            return bots;
        }

        private Bot CreateBot(string id, Transform botBasePoint, Action onBotReachedTarget)
        {
            GameObject botObject = _assets.Instantiate(AssetPath.BOT, botBasePoint.position, _parent);
            GameObject botBase = _assets.Instantiate(AssetPath.BOT_BASE, botBasePoint.position, botBasePoint);
            BotRegistry botRegistry = botObject.GetComponent<BotRegistry>();
            BotAnimator botAnimator = botObject.GetComponent<BotAnimator>();
            BotMover botMover = botObject.GetComponent<BotMover>();
            BotEffects botEffects = botObject.GetComponent<BotEffects>();
            BotCargo botCargo = botObject.GetComponent<BotCargo>();

            botRegistry.Initialize(id);
            botAnimator.Initialize();
            botMover.Initialize();
            botEffects.Initialize();

            BotComponents botComponents = new BotComponents(botObject, botBase.transform, botRegistry, botAnimator, botMover, botEffects, botCargo);
            Bot bot = new Bot(botComponents, onBotReachedTarget);
            return bot;
        }
    }
}
