namespace FactoryBots.Game.Services.Overlay
{
    public interface IGameOverlay : IGameService
    {
        LeavePanel LeavePanel { get; }
        BotStatusPanel BotStatusPanel { get; }
        AlarmPanel AlarmPanel { get; }
    }
}
