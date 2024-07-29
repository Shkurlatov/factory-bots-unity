using FactoryBots.SO;

namespace FactoryBots.App.Services.Configs
{
    public interface IAppConfigProvider : IAppService
    {
        BotConfig GetBotConfig();
    }
}
