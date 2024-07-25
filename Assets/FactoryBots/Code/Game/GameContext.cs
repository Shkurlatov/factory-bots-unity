using FactoryBots.App.Services;
using FactoryBots.App.Services.Assets;
using FactoryBots.App.Services.Audio;
using FactoryBots.App.Services.Progress;
using FactoryBots.UI;
using UnityEngine;

namespace FactoryBots.Game
{
    public class GameContext
    {
        public readonly IAppAssetProvider Assets;
        public readonly IAppData Data;
        public readonly IAppAudio Audio;

        public readonly HomeButton HomeButton;
        public readonly Transform UIRoot;
        public readonly GameMode GameMode;

        public GameContext(
            AppServiceContainer appContext,
            HomeButton homeButton,
            Transform uIRoot,
            GameMode gameMode)
        {
            Assets = appContext.Single<IAppAssetProvider>();
            Data = appContext.Single<IAppData>();
            Audio = appContext.Single<IAppAudio>();

            HomeButton = homeButton;
            UIRoot = uIRoot;
            GameMode = gameMode;
        }
    }
}
