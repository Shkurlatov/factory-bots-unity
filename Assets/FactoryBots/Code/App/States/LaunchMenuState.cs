using FactoryBots.App.Bootstrap;
using FactoryBots.App.Services;
using FactoryBots.App.Services.Assets;
using FactoryBots.App.Services.Progress;
using FactoryBots.Menu;
using UnityEngine;

namespace FactoryBots.App.States
{
    public class LaunchMenuState : IState
    {
        private const string MENU_SCENE = "Menu";
        private const string UI_ROOT_TAG = "UIRoot";

        private readonly IAppStateMachine _appStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AppServiceContainer _appContext;

        public LaunchMenuState(IAppStateMachine appStateMachine, SceneLoader sceneLoader, AppServiceContainer appContext)
        {
            _appStateMachine = appStateMachine;
            _sceneLoader = sceneLoader;
            _appContext = appContext;
        }

        public void Enter() =>
            _sceneLoader.Load(MENU_SCENE, OnLoaded);

        private void OnLoaded()
        {
            IAppAssetProvider assets = _appContext.Single<IAppAssetProvider>();
            IAppData data = _appContext.Single<IAppData>();

            Transform uiRoot = InitUIRoot();
            MenuPanel menuPanel = InitMenuPanel(assets, data, uiRoot);

            _appStateMachine.Enter<MenuState, MenuPanel>(menuPanel);
        }

        private Transform InitUIRoot() =>
            GameObject.FindGameObjectWithTag(UI_ROOT_TAG).transform;

        private MenuPanel InitMenuPanel(IAppAssetProvider assets, IAppData data, Transform uiRoot)
        {
            MenuPanel menuPanel = assets.Instantiate(AssetPath.MENU_PANEL, uiRoot).GetComponent<MenuPanel>();
            menuPanel.Initialize(data);
            return menuPanel;
        }

        public void Exit() { }
    }
}
