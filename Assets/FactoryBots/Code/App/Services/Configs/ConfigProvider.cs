using FactoryBots.SO;
using UnityEngine;

namespace FactoryBots.App.Services.Configs
{
    public class ConfigProvider : IAppConfigProvider
    {
        public BotConfig GetBotConfig() =>
            Resources.Load<BotConfig>(ConfigPath.BOT_CONFIG);

        public void Cleanup() { }
    }
}
