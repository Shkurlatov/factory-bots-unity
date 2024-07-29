using FactoryBots.App.Bootstrap;
using FactoryBots.App.Services;
using FactoryBots.App.Services.Assets;
using FactoryBots.App.Services.Configs;
using FactoryBots.Game;
using FactoryBots.Game.Services;
using FactoryBots.Game.Services.Bots;
using FactoryBots.Game.Services.Buildings;
using FactoryBots.Game.Services.Input;
using FactoryBots.Game.Services.Overlay;
using FactoryBots.Game.Services.Parking;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FactoryBots.App.States
{
    public class LaunchGameState : IPayloadedState<GameMode>
    {
        private const string GAME_SCENE = "Game";

        private readonly IAppStateMachine _appStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AppServiceContainer _appContext;

        private GameMode _gameMode;
        private GameServiceContainer _gameContext;

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
            _gameContext = new GameServiceContainer();

            RegisterGameServices();

            _appStateMachine.Enter<GameState, GameServiceContainer>(_gameContext);
        }

        private void RegisterGameServices()
        {
            RegisterOverlay();
            RegisterInput();
            RegisterParking();
            RegisterBuildings();
            RegisterBots();
        }

        private void RegisterOverlay()
        {
            OverlayManager overlay = GetGameServiceFromScene<OverlayManager>();
            overlay.Initialize();
            _gameContext.RegisterSingle<IGameOverlay>(overlay);
        }

        private void RegisterInput()
        {
            Raycaster raycaster = new Raycaster(Camera.main);

            InputManager input = new InputManager(raycaster);
            input.Initialize();
            _gameContext.RegisterSingle<IGameInput>(input);
        }

        private void RegisterParking()
        {
            ParkingManager parking = GetGameServiceFromScene<ParkingManager>();
            parking.Initialize(_gameMode.BotAmount);
            _gameContext.RegisterSingle<IGameParking>(parking);
        }
        
        private void RegisterBuildings()
        {
            BoxFactory boxFactory = new BoxFactory(
                _appContext.Single<IAppAssetProvider>());

            BuildingManager buildings = Object.FindObjectOfType<BuildingManager>();
            buildings.Initialize(boxFactory);
            _gameContext.RegisterSingle<IGameBuildings>(buildings);
        }
        
        private void RegisterBots()
        {
            BotFactory botFactory = new BotFactory(
                _appContext.Single<IAppAssetProvider>(),
                _appContext.Single<IAppConfigProvider>());

            BotManager botManager = new BotManager(
                _gameContext.Single<IGameOverlay>(),
                _gameContext.Single<IGameInput>(),
                _gameContext.Single<IGameParking>(),
                botFactory);

            botManager.Initialize();
            _gameContext.RegisterSingle<IGameBots>(botManager);
        }

        private static TService GetGameServiceFromScene<TService>() where TService : MonoBehaviour, IGameService
        {
            TService gameService = Object.FindFirstObjectByType<TService>();

            if (gameService == null)
            {
                string errorMessage = $"Error: Could not find any object of type {typeof(TService).Name} in the scene. Ensure that an object of this type is present and active in the scene.";
                throw new NullReferenceException(errorMessage);
            }

            return gameService;
        }

        public void Exit() { }
    }
}
