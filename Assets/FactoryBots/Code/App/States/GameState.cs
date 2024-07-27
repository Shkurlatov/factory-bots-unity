using FactoryBots.App.Services;
using FactoryBots.Game;
using FactoryBots.Game.Services;

namespace FactoryBots.App.States
{
    public class GameState : IPayloadedState<GameServiceContainer>
    {
        private readonly IAppStateMachine _appStateMachine;
        private readonly AppServiceContainer _appContext;

        private GameController _gameController;
        private GameServiceContainer _gameContext;

        public GameState(IAppStateMachine appStateMachine, AppServiceContainer appContext)
        {
            _appStateMachine = appStateMachine;
            _appContext = appContext;
        }

        public void Enter(GameServiceContainer gameContext)
        {
            _gameContext = gameContext;
            //_gameContext.Single<IGameBuildings>().ClickOnTavernAction += OpenHeroShopScene;
        }

        public void EnterOld(GameController gameController)
        {
            _gameController = gameController;
            //_gameController.Initialize();
            //_gameController.LeaveGameAction += ReturnToMenu;
        }

        private void ReturnToMenu()
        {
            //_gameController.LeaveGameAction -= ReturnToMenu;

            _appStateMachine.Enter<LaunchMenuState>();
        }

        public void Exit()
        {
            //if (_gameController != null)
            //{
            //    _gameController.Cleanup();
            //    _gameController = null;
            //}

            if (_gameContext != null)
            {
                _gameContext.Cleanup();
                _gameContext = null; 
            }
        }
    }
}
