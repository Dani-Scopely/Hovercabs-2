using System;
using Hovercabs.Events;
using UnityEngine;
using Utils;

namespace Hovercabs.UI
{
    [RequireComponent(typeof(GameplayCanvasView))]
    public class GameplayCanvasController : MonoBehaviour
    {
        private GameplayCanvasView _view;

        [SerializeField] private PauseCanvasController pauseCanvasController;
        
        private void Awake()
        {
            _view = GetComponent<GameplayCanvasView>();
        }

        private void Start()
        {
            _view.Init(this);
            _view.OnPauseClick += OnPauseClick;
            
        }
        
        public void Init(Action onQuitRace)
        {
            pauseCanvasController.Init(onQuitRace);
            
        }
        
        private void OnPauseClick()
        {
            pauseCanvasController.Show(true);
        }
    }
}
