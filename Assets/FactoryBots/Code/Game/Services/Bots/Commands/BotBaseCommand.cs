namespace FactoryBots.Game.Services.Bots.Commands
{
    public class BotBaseCommand : IBotCommand
    {
        public string TargetId { get; private set; }
        public bool IsTargetReached { get; private set; }

        public BotBaseCommand()
        {
            TargetId = $"Base";
            IsTargetReached = false;
        }

        public void SetTargetReached(bool _isTargetReached) => 
            IsTargetReached = _isTargetReached;
    }
}
