using FactoryBots.App.Bootstrap;
using FactoryBots.App.Services;
using FactoryBots.App.Services.Assets;
using FactoryBots.App.Services.Audio;
using FactoryBots.App.Services.Progress;

namespace FactoryBots.App.States
{
    public class BootstrapState : IState
    {
        private const string INITIAL_SCENE = "Boot";

        private readonly IAppStateMachine _appStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AppServiceContainer _appContext;

        public BootstrapState(IAppStateMachine appStateMachine, SceneLoader sceneLoader, AppServiceContainer appContext)
        {
            _appStateMachine = appStateMachine;
            _sceneLoader = sceneLoader;
            _appContext = appContext;
        }

        public void Enter() =>
            _sceneLoader.Load(INITIAL_SCENE, OnLoaded);

        private void OnLoaded()
        {
            RegisterAppServices();

            _appStateMachine.Enter<LaunchMenuState>();
        }

        private void RegisterAppServices()
        {
            IAppAssetProvider assets = RegisterAssetProvider();
            RegisterData();
            RegisterAudio(assets);
        }

        private IAppAssetProvider RegisterAssetProvider()
        {
            IAppAssetProvider assets = new AssetProvider();
            _appContext.RegisterSingle(assets);
            return assets;
        }

        private void RegisterData() =>
            _appContext.RegisterSingle<IAppData>(new PlayerPrefsSaveLoadManager());

        private void RegisterAudio(IAppAssetProvider assets)
        {
            IAppAudio audio = assets.Instantiate(AssetPath.AUDIO_PLAYER).GetComponent<AudioPlayer>();
            _appContext.RegisterSingle(audio);
        }

        public void Exit() { }
    }
}
