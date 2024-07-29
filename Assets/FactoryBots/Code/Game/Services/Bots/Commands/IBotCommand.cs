namespace FactoryBots.Game.Services.Bots.Commands
{
    public interface IBotCommand
    {
        public string TargetId { get; }
        public bool IsTargetReached { get; }

        public void SetTargetReached(bool _isTargetReached);
    }
}
