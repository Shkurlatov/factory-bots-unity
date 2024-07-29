using FactoryBots.App.Services.Progress;
using FactoryBots.Game;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryBots.Menu
{
    public class MenuPanel : MonoBehaviour
    {
        [SerializeField] private Slider _gameModeSlider;
        [SerializeField] private Image _sliderImage;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _exitButton;

        private IAppData _data;
        private int _gameModeValue;
        private float _sliderImageFillFactor = 1.0f;

        public event Action<GameMode> StartAction;
        public event Action ExitAction;

        private void OnEnable()
        {
            _gameModeSlider.onValueChanged.AddListener(OnGameModeValueChanged);
            _startButton.onClick.AddListener(OnStartButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnDisable()
        {
            _gameModeSlider.onValueChanged.RemoveAllListeners();
            _startButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

        public async void Initialize(IAppData data)
        {
            _data = data;

            SettingsData settingsData = await _data.LoadSettingsAsync();

            _gameModeSlider.value = settingsData.BotAmount;
            _gameModeValue = settingsData.BotAmount;
            _sliderImageFillFactor = 1 / (_gameModeSlider.maxValue - _gameModeSlider.minValue + 1);

            UpdateSliderImage();
        }

        private void OnGameModeValueChanged(float value)
        {
            _gameModeValue = (int)value;
            UpdateSliderImage();
        }

        private async void OnStartButtonClick()
        {
            StartAction?.Invoke(new GameMode(_gameModeValue));

            await _data.SaveSettingsAsync(new SettingsData(_gameModeValue));
        }

        private void OnExitButtonClick() => 
            ExitAction?.Invoke();

        private void UpdateSliderImage() => 
            _sliderImage.fillAmount = _gameModeSlider.value * _sliderImageFillFactor;
    }
}
