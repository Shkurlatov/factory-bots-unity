using UnityEngine;

namespace FactoryBots.Game.Services.Overlay
{
    public class OverlayManager : MonoBehaviour, IGameOverlay
    {
        [SerializeField] private LeavePanel _leavePanel;
        [SerializeField] private BotStatusPanel _botStatusPanel;
        [SerializeField] private AlarmPanel _alarmPanel;

        public LeavePanel LeavePanel => _leavePanel;
        public BotStatusPanel BotStatusPanel => _botStatusPanel;
        public AlarmPanel AlarmPanel => _alarmPanel;

        public void Initialize()
        {
            _botStatusPanel.Initialize();
            _alarmPanel.Initialize();
        }

        public void Cleanup()
        {

        }
    }
}
