using UnityEngine;

namespace FactoryBots.Game.Services.Bots.Commands
{
    public interface IBotCommand
    {
        Vector3 TargetPosition { get; }
        public string TargetId { get; }
        public bool IsTargetReached { get; }

        public void SetTargetReached(bool _isTargetReached);
    }
}
