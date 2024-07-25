using FactoryBots.App.Bootstrap;
using FactoryBots.App.Services;
using FactoryBots.App.Services.Assets;
using FactoryBots.App.Services.Audio;
using FactoryBots.App.Services.Progress;
using FactoryBots.App.Services.Randomizer;
using FactoryBots.Game;
using FactoryBots.UI;
using UnityEngine;

namespace FactoryBots.App.States
{
    public class LaunchGameState : IPayloadedState<GameMode>
    {
        private const string GAME_SCENE = "Game";
        private const string UI_ROOT_TAG = "UIRoot";

        private readonly IAppStateMachine _appStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AppServiceContainer _appContext;

        private GameMode _gameMode;

        public LaunchGameState(IAppStateMachine appStateMachine, SceneLoader sceneLoader, AppServiceContainer appContext)
        {
            _appStateMachine = appStateMachine;
            _sceneLoader = sceneLoader;
            _appContext = appContext;
        }

        public void Enter(GameMode gameMode)
        {
            _gameMode = gameMode;
            _sceneLoader.Load(GAME_SCENE, OnLoaded);
        }

        private void OnLoaded()
        {
            IAppAssetProvider assets = _appContext.Single<IAppAssetProvider>();
            IAppRandomizer randomizer = _appContext.Single<IAppRandomizer>();
            IAppData data = _appContext.Single<IAppData>();
            IAppAudio audio = _appContext.Single<IAppAudio>();

            Transform uiRoot = InitUIRoot();
            HomeButton homeButton = InitHomeButton(assets, uiRoot);

            GameContext gameContext = new GameContext(
                _appContext,
                homeButton,
                uiRoot,
                _gameMode);

            GameController gameController = new GameController(gameContext);

            _appStateMachine.Enter<GameState, GameController>(gameController);
        }

        private Transform InitUIRoot() =>
            GameObject.FindGameObjectWithTag(UI_ROOT_TAG).transform;

        private HomeButton InitHomeButton(IAppAssetProvider assets, Transform uiRoot) =>
            assets.Instantiate(AssetPath.HOME_BUTTON, uiRoot).GetComponent<HomeButton>();

        public void Exit() { }
    }
}
