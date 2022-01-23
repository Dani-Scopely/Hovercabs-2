using System;
using Hovercabs.Configurations.Gameplay.Vehicles;
using Hovercabs.Configurations.Vehicles;
using Hovercabs.Managers;
using Hovercabs.Models;
using Hovercabs.Services;
using Hovercabs.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Hovercabs.Controllers
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private GameplayCanvasController gameplayCanvasController;
        [SerializeField] private LevelController levelController;

        private ProfileService _profileService;
        
        [Header("Configurations")]
        [SerializeField] private VehicleGameplayConfig vehicleGameplayConfig;

        [SerializeField] private VehiclesConfig vehiclesConfig;

        private Action<Result> OnGameOver { get; set; }

        private Result _raceResult;
        
        public void Init(TrackManager trackManager, VehiclesService vehiclesService, ProfileService profileService, Action onQuit, Action<Result> onGameOver)
        {
            _profileService = profileService;

            OnGameOver = onGameOver;
            
            gameplayCanvasController.Init(onQuit, profileService);

            _raceResult = new Result();
            
            InitLevel(trackManager, vehiclesService, profileService);
        }

        private void InitLevel(TrackManager trackManager, VehiclesService vehiclesService, ProfileService profileService)
        {
            levelController.OnDistanceChanged += OnDistanceChanged;
            levelController.OnXenitsChanged += OnXenitsChanged;
            levelController.OnFuelChanged += OnFuelChanged;
            levelController.OnOutOfFuel += OnOutOfFuel;
            
            levelController.Init(trackManager, vehiclesService, profileService, vehicleGameplayConfig, vehiclesConfig);
        }

        private void OnDestroy()
        {
            levelController.OnDistanceChanged -= OnDistanceChanged;
            levelController.OnXenitsChanged -= OnXenitsChanged;
            levelController.OnFuelChanged -= OnFuelChanged;
            levelController.OnOutOfFuel -= OnOutOfFuel;
        }

        private void OnDistanceChanged(float distance)
        {
            _raceResult.Distance = distance;
            
            gameplayCanvasController.SetDistance(distance);
        }

        private void OnFuelChanged(float fuel)
        {
            _raceResult.RemainingFuel = fuel;
            
            gameplayCanvasController.SetFuel(fuel);
        }
        
        private void OnXenitsChanged(int xenits)
        {
            _raceResult.Xenits += xenits;
            
            _profileService.Profile.Xenits+=xenits;
            
            _profileService.SaveProfile();
            
            gameplayCanvasController.SetXenits(_profileService.Profile.Xenits);
        }

        private void OnOutOfFuel()
        {
            OnGameOver?.Invoke(_raceResult);
        }
    }
}