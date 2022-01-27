using System;
using Hovercabs.Controllers;
using UnityEngine;

namespace Hovercabs.UI
{
    [RequireComponent(typeof(AudioController))]
    [RequireComponent(typeof(CountdownCanvasView))]
    public class CountdownCanvasController : MonoBehaviour
    {
        private CountdownCanvasView _view;
        private float _initTime;
        private int _from;
        private Action _onCountdownEnded;
        private bool _isEnded;
        private AudioController _audioController;
        private int _indexAudio = 0;

        [SerializeField] private AudioClip[] countdownAudioClips;
        
        /// <summary>
        /// TODO: THIS CODE IS A COMPLETELY SHIT. I HAVE TO REFACTOR IT
        /// </summary>
        /// <param name="onCountdownEnded"></param>
        public void Init(Action onCountdownEnded)
        {
            _view = GetComponent<CountdownCanvasView>();
            _audioController = GetComponent<AudioController>();
            
            _initTime = Time.realtimeSinceStartup;
            _onCountdownEnded = onCountdownEnded;
            _isEnded = false;

            _from = countdownAudioClips.Length - 1;
            
            _audioController.PlayAudio(countdownAudioClips[_indexAudio]);
            
            _view.SetData(_from);
            
            _indexAudio++;
        }
     
        private void Update()
        {
            if (_isEnded) return;
            
            if (_from == -1)
            {
                _onCountdownEnded.Invoke();
                gameObject.SetActive(false);
                return;
            }

            if (!(Time.realtimeSinceStartup > _initTime + 1.5f)) return;

            
            _audioController.PlayAudio(countdownAudioClips[Mathf.Min(_indexAudio,countdownAudioClips.Length-1)]);
            
            _from--;
            
            _initTime = Time.realtimeSinceStartup;
            
            _view.SetData(_from);
            
            _indexAudio++;
        }
    }
}
