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

        public void Init(Action onQuitRace)
        {
            _onQuitRace = onQuitRace;
        }
        
        public void Show(bool show)
        {
            gameObject.SetActive(show);
            Time.timeScale = 0;
        }
        
        private void OnContinue()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        private void OnQuit()
        {
            _onQuitRace();
            Time.timeScale = 1;
        }

        private void OnDestroy()
        {
            _view.OnContinueClicked -= OnContinue;
            _view.OnQuitClicked -= OnQuit;
        }
    }
}
