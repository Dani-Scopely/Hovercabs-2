using System;
using Hovercabs.Events;
using Hovercabs.Services;
using UnityEngine;
using Utils;

namespace Hovercabs.UI
{
    [RequireComponent(typeof(GameplayCanvasView))]
    public class GameplayCanvasController : MonoBehaviour
    {
        private ProfileService _profileService;
        private GameplayCanvasView _view;

        [SerializeField] private FuelCanvasController fuelCanvasController;
        [SerializeField] private XenitsCanvasController xenitsCanvasController;
        [SerializeField] private PauseCanvasController pauseCanvasController;
        [SerializeField] private DistanceCanvasController distanceCanvasController;
        [SerializeField] private CountdownCanvasController countdownCanvasController;
        
        private void Awake()
        {
            _view = GetComponent<GameplayCanvasView>();
        }

        private void Start()
        {
            _view.Init(this);
            _view.OnPauseClick += OnPauseClick;
        }

        private void OnDestroy()
        {
            _view.OnPauseClick -= OnPauseClick;
        }

        public void Init(Action onQuitRace, Action<bool> onPause, Action onCountdownEnded, ProfileService profileService)
        {
            xenitsCanvasController.Init(profileService.Profile.Xenits);
            fuelCanvasController.Init();
            pauseCanvasController.Init(onQuitRace, onPause);
            countdownCanvasController.Init(onCountdownEnded);
        }

        public void SetDistance(float distance)
        {
            distanceCanvasController.SetDistance(distance);    
        }
        
        private void OnPauseClick()
        {
            pauseCanvasController.Show(true);
        }

        public void SetXenits(int xenits)
        {
            xenitsCanvasController.SetData(xenits);
        }

        public void SetFuel(float fuel)
        {
            fuelCanvasController.SetData(fuel);
        }

        public void SetCountdown(int from)
        {
            countdownCanvasController.SetData(from);
            countdownCanvasController.gameObject.SetActive(true);
        }
    }
}
