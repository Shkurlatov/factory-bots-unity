using System;

namespace FactoryBots.Game
{
    public class GameController
    {
        private readonly GameContext _context;

        public event Action LeaveGameAction;

        public GameController(GameContext gameContext)
        {
            _context = gameContext;
        }

        public void Initialize()
        {
            _context.HomeButton.HomeAction += LeaveGame;
        }

        private void LeaveGame()
        {
            LeaveGameAction?.Invoke();
        }

        public void Cleanup()
        {
            _context.HomeButton.HomeAction -= LeaveGame;
        }
    }
}
