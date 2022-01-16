using System;
using Hovercabs.Controllers.Popups.Base;
using Hovercabs.Enums;
using Hovercabs.Events;
using Hovercabs.Services;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Hovercabs.Controllers.Popups.Settings
{
    public class SettingsPopupController : PopupController
    {
        [SerializeField] private Button btnBack;
        [SerializeField] private Slider soundSlider;
        [SerializeField] private Slider musicSlider;
        
        private Action _onBack;
        private AudioService _audioService;
        
        private void Awake()
        {
            _audioService = new AudioService();
            
            btnBack.onClick.AddListener(OnBackClick);
            

            soundSlider.value = _audioService.Get(AudioMode.Sound);
            musicSlider.value = _audioService.Get(AudioMode.Music);
            
            soundSlider.onValueChanged.AddListener(OnSoundSliderChanged);
            musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        }
        
        public override void Show(PopupType popupType, Action onBack)
        {
            _onBack = onBack;
            
            if (popupType != PopupType.Settings)
            {
                gameObject.SetActive(false);
                return;
            }
            
            gameObject.SetActive(true);
        }

        private void OnBackClick()
        {
            gameObject.SetActive(false);
            _onBack?.Invoke();
        }

        private void OnSoundSliderChanged(float value)
        {
            _audioService.Set(AudioMode.Sound, value);
            NotifyAudioChanges();
        }

        private void OnMusicSliderChanged(float value)
        {
            _audioService.Set(AudioMode.Music, value);
            NotifyAudioChanges();
        }

        private void NotifyAudioChanges()
        {
            EventBus.GetBus().Send(new OnAudioChanged());
        }
        
        private void OnDestroy()
        {
            btnBack.onClick.RemoveAllListeners();
            soundSlider.onValueChanged.RemoveAllListeners();
            musicSlider.onValueChanged.RemoveAllListeners();
        }
    }
}