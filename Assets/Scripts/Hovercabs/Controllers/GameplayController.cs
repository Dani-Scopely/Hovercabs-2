using System;
using Hovercabs.Configurations.Gameplay.Vehicles;
using Hovercabs.Configurations.Vehicles;
using Hovercabs.Managers;
using Hovercabs.Services;
using Hovercabs.UI;
using UnityEngine;

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

        public void Init(TrackManager trackManager, VehiclesService vehiclesService, ProfileService profileService, Action onQuit)
        {
            _profileService = profileService;
            
            gameplayCanvasController.Init(onQuit, profileService);
            
            InitLevel(trackManager, vehiclesService, profileService);
        }

        private void InitLevel(TrackManager trackManager, VehiclesService vehiclesService, ProfileService profileService)
        {
            levelController.OnDistanceChanged += OnDistanceChanged;
            levelController.OnXenitsChanged += OnXenitsChanged;
            levelController.Init(trackManager, vehiclesService, profileService, vehicleGameplayConfig, vehiclesConfig);
        }

        private void OnDestroy()
        {
            levelController.OnDistanceChanged -= OnDistanceChanged;
        }

        private void OnDistanceChanged(float distance)
        {
            gameplayCanvasController.SetDistance(distance);
        }
        
        private void OnXenitsChanged(int xenits)
        {
            _profileService.Profile.Xenits+=xenits;
            
            _profileService.SaveProfile();
            
            gameplayCanvasController.SetXenits(_profileService.Profile.Xenits);
        }
    }
}