using System;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryBots.UI
{
    public class LeaveButton : MonoBehaviour
    {
        [SerializeField] private Button _homeButton;

        public event Action HomeAction;

        private void OnEnable()
        {
            _homeButton.onClick.AddListener(OnHomeButtonClick);
        }

        private void OnDisable()
        {
            _homeButton.onClick.RemoveAllListeners();
        }

        private void OnHomeButtonClick()
        {
            HomeAction?.Invoke();
        }
    }

}
