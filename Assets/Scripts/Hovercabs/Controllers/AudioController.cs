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
        [SerializeField] public AudioSource audioSource;
        private AudioService _audioService;
        
        private void Awake()
        {
            _audioService = new AudioService();
        }

        private void Start()
        {
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
                if (audioSource == null) return;
                audioSource.volume = _audioService.Get(audioMode);
            }
        }

        public void PlayAudio(AudioClip clip)
        {
            audioSource.clip = clip;
            if (!audioSource.enabled) return;
            audioSource.Play();
        }
    }
}