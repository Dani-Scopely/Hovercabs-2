using System;
using Hovercabs.Enums;
using Hovercabs.Events;
using Hovercabs.Services;
using UnityEngine;
using Utils;

namespace Hovercabs.Controllers
{
    public class AudioController : MonoBehaviour, IObserver
    {
        [SerializeField] private AudioMode audioMode;
        [SerializeField] private AudioSource audioSource;
        private AudioService _audioService;
        
        private void Awake()
        {
            _audioService = new AudioService();
            var p = _audioService.Get(audioMode);
            audioSource.volume = p;
            
            EventBus.GetBus().Register(this, typeof(OnAudioChanged));
        }

        private void OnDestroy()
        {
            EventBus.GetBus().Unregister(this);
        }

        public void OnEvent(IEvent pEvent)
        {
            if (pEvent is OnAudioChanged)
            {
                audioSource.volume = _audioService.Get(audioMode);
            }
        }
    }
}