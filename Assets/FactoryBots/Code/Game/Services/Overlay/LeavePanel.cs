using System;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryBots.Game.Services.Overlay
{
    public class LeavePanel : MonoBehaviour
    {
        [SerializeField] private Button _leaveButton;

        public event Action LeaveGameAction;

        public void Initialize() => 
            _leaveButton.onClick.AddListener(OnLeaveButtonClick);

        private void OnLeaveButtonClick() => 
            LeaveGameAction?.Invoke();

        private void OnDestroy() => 
            _leaveButton.onClick.RemoveAllListeners();
    }
}
