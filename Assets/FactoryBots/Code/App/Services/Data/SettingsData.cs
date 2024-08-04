using System;
using UnityEngine;

namespace FactoryBots.App.Services.Progress
{
    [Serializable]
    public class SettingsData
    {
        [SerializeField] private int _botAmount;

        public int BotAmount => _botAmount;

        public SettingsData(int botAmount)
        {
            _botAmount = botAmount;
        }
    }
}
