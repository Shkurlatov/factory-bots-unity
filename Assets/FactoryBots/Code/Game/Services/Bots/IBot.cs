using FactoryBots.Game.Services.Bots.Commands;
using System;

namespace FactoryBots.Game.Services.Bots
{
    public interface IBot
    {
        event Action<string> StatusUpdatedAction;

        void Select();
        void Unselect();
        string GetStatus();
        bool IsCloseToBase();
        void ReturnToBase();
        void ClearAllAndExecuteCommand(IBotCommand command);
        void AddCommand(IBotCommand command);
        void ExecutePreviousCommand();
        void Cleanup();
    }
}
