using System;
using Core;
using Services.Stats;
using Settings;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameLogics.UILogics
{
    public class SlidersController : MonoBehaviour
    {
        [SerializeField]
        private Slider _speedSlider;
        [SerializeField]
        private Slider _radiusSlider;

        [Inject]
        public SettingsContainer _settingsContainer;
        [Inject]
        public IStatsUpdater _statsUpdater;
        
        private GameSettings _gameSettings;

        private void Awake()
        {
            _gameSettings = _settingsContainer.GameSettings;
            SetupSlider(_radiusSlider, _gameSettings.PlayerRadius, 1);
            SetupSlider(_speedSlider, _gameSettings.CockroachSpeedModifier, 1);
        }

        private void SetupSlider(Slider slider, MinMaxValue info, float currentValue)
        {
            slider.minValue = info.MinValue;
            slider.maxValue = info.MaxValue;
            slider.value = currentValue;
        }

        private void OnEnable()
        {
            _radiusSlider.onValueChanged.AddListener(_statsUpdater.UpdatePlayerRadius);
            _speedSlider.onValueChanged.AddListener(_statsUpdater.UpdateSpeedModifier);
        }

        private void OnDisable()
        {
            _radiusSlider.onValueChanged.RemoveListener(_statsUpdater.UpdatePlayerRadius);
            _speedSlider.onValueChanged.RemoveListener(_statsUpdater.UpdateSpeedModifier);
        }
    }
}