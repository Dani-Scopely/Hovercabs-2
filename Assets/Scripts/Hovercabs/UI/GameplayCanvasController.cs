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
        [SerializeField] private DistanceCanvasController distanceCanvasController;
        
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

        public void SetDistance(float distance)
        {
            distanceCanvasController.SetDistance(distance);    
        }
        
        private void OnPauseClick()
        {
            pauseCanvasController.Show(true);
        }
    }
}
