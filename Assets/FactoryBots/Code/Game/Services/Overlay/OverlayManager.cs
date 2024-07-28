using UnityEngine;

namespace FactoryBots.Game.Services.Overlay
{
    public class OverlayManager : MonoBehaviour, IGameOverlay
    {
        [SerializeField] private LeavePanel _leavePanel;
        [SerializeField] private BotStatusPanel _botStatusPanel;
        [SerializeField] private AlarmPanel _alarmPanel;

        public void Cleanup()
        {

        }
    }
}
