using System;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryBots.Game.Services.Overlay
{
    public class AlarmPanel : MonoBehaviour
    {
        [SerializeField] private Button _alarmButton;
        [SerializeField] private Button _resumeButton;

        public event Action StartAlarmAction;
        public event Action CancelAlarmAction;

        public void Initialize()
        {
            _alarmButton.onClick.AddListener(OnAlarmButtonClick);
            _resumeButton.onClick.AddListener(OnResumeButtonClick);

            _resumeButton.gameObject.SetActive(false);
        }

        private void OnAlarmButtonClick()
        {
            _alarmButton.gameObject.SetActive(false);
            _resumeButton.gameObject.SetActive(true);

            StartAlarmAction?.Invoke();
        }
        
        private void OnResumeButtonClick()
        {
            _resumeButton.gameObject.SetActive(false);
            _alarmButton.gameObject.SetActive(true);

            CancelAlarmAction?.Invoke();
        }

        private void OnDestroy()
        {
            _alarmButton.onClick.RemoveAllListeners();
            _resumeButton.onClick.RemoveAllListeners();
        }
    }
}
