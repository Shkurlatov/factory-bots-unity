using FactoryBots.App.Services;
using FactoryBots.Game.Services;
using FactoryBots.Game.Services.Overlay;

namespace FactoryBots.App.States
{
    public class GameState : IPayloadedState<GameServiceContainer>
    {
        private readonly IAppStateMachine _appStateMachine;
        private readonly AppServiceContainer _appContext;

        private GameServiceContainer _gameContext;

        public GameState(IAppStateMachine appStateMachine, AppServiceContainer appContext)
        {
            _appStateMachine = appStateMachine;
            _appContext = appContext;
        }

        public void Enter(GameServiceContainer gameContext)
        {
            _gameContext = gameContext;
            _gameContext.Single<IGameOverlay>().LeavePanel.LeaveGameAction += ReturnToMenu;
        }

        private void ReturnToMenu()
        {
            _gameContext.Single<IGameOverlay>().LeavePanel.LeaveGameAction -= ReturnToMenu;
            _appStateMachine.Enter<LaunchMenuState>();
        }

        public void Exit()
        {
            if (_gameContext != null)
            {
                _gameContext.Cleanup();
                _gameContext = null; 
            }
        }
    }
}
