using UnityEngine;

namespace FactoryBots.Game.Services.Bots.Commands
{
    public class BotPositionCommand : IBotCommand
    {
        public Vector3 TargetPosition { get; private set; }
        public string TargetId { get; private set; }
        public bool IsTargetReached { get; private set; }

        public BotPositionCommand(Vector3 targetPosition)
        {
            TargetPosition = targetPosition;
            TargetId = $"(X: {(int)targetPosition.x}, Y: {(int)targetPosition.z})";
            IsTargetReached = false;
        }

        public void SetTargetReached(bool _isTargetReached) => 
            IsTargetReached = _isTargetReached;
    }
}
