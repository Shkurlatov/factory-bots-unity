﻿using FactoryBots.App.Services;
using FactoryBots.App.States;

namespace FactoryBots.App.Bootstrap
{
    public class App
    {
        private readonly AppServiceContainer _appContext;
        private readonly IAppStateMachine _appStateMachine;

        public App(SceneLoader sceneLoader)
        {
            _appContext = new AppServiceContainer();
            _appStateMachine = new AppStateMachine(sceneLoader, _appContext);
            _appStateMachine.Enter<BootstrapState>();
        }

        public void OnApplicationQuit()
        {
            _appStateMachine.Cleanup();
            _appContext.Cleanup();
        }
    }
}
