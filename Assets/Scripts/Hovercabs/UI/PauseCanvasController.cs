using System;
using Hovercabs.Events;
using UnityEngine;
using Utils;

namespace Hovercabs.UI
{
    [RequireComponent(typeof(PauseCanvasView))]
    public class PauseCanvasController : MonoBehaviour
    {
        private PauseCanvasView _view;
        private Action _onQuitRace;
        private Action<bool> _onPaused;
        
        private void Awake()
        {
            _view = GetComponent<PauseCanvasView>();
        }

        private void Start()
        {
            _view.Init(this);
            _view.OnContinueClicked += OnContinue;
            _view.OnQuitClicked += OnQuit;
        }

        public void Init(Action onQuitRace, Action<bool> onPaused)
        {
            _onQuitRace = onQuitRace;
            _onPaused = onPaused;
        }
        
        public void Show(bool show)
        {
            gameObject.SetActive(show);
            _onPaused?.Invoke(true);
        }
        
        private void OnContinue()
        {
            gameObject.SetActive(false);
            _onPaused?.Invoke(false);
        }

        private void OnQuit()
        {
            _onQuitRace();
            _onPaused?.Invoke(false);
        }

        private void OnDestroy()
        {
            _view.OnContinueClicked -= OnContinue;
            _view.OnQuitClicked -= OnQuit;
        }
    }
}
