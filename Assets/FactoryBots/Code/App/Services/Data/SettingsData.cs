using System;
namespace FactoryBots.App.Services.Progress
{
    [Serializable]
    public class SettingsData
    {
        public int BotAmount;

        public SettingsData(int botAmount)
        {
            BotAmount = botAmount;
        }
    }
}
