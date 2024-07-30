using FactoryBots.Game.Services.Bots.Commands;

namespace FactoryBots.Game.Services.Bots
{
    public interface IBot
    {
        string Status { get; }

        bool IsCloseToBase();
        void Select();
        void Unselect();
        void ExecuteBaseCommand();
        void ClearAllAndExecuteCommand(IBotCommand command);
        void AddCommand(IBotCommand command);
        void ExecutePreviousCommand();
        void Cleanup();
    }
}
