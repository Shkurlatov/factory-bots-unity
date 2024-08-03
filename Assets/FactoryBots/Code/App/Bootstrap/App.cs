using FactoryBots.App.Services;
using FactoryBots.App.States;

namespace FactoryBots.App.Bootstrap
{
    public class App
    {
        private readonly IAppStateMachine _appStateMachine;

        public App(SceneLoader sceneLoader)
        {
            _appStateMachine = new AppStateMachine(sceneLoader, new AppServiceContainer());
            _appStateMachine.Enter<BootstrapState>();
        }

        public void OnApplicationQuit()
        {
            _appStateMachine.Cleanup();
        }
    }
}
