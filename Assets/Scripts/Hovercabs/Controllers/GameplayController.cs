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
        
        [Header("Configurations")]
        [SerializeField] private VehicleGameplayConfig vehicleGameplayConfig;

        [SerializeField] private VehiclesConfig vehiclesConfig;
        
        public void Init(TrackManager trackManager, VehiclesService vehiclesService, Action onQuit)
        {
            gameplayCanvasController.Init(onQuit);
            
            InitLevel(trackManager, vehiclesService);
        }

        private void InitLevel(TrackManager trackManager, VehiclesService vehiclesService)
        {
            levelController.OnDistanceChanged += OnDistanceChanged;
            levelController.Init(trackManager, vehiclesService, vehicleGameplayConfig, vehiclesConfig);
        }

        private void OnDestroy()
        {
            levelController.OnDistanceChanged -= OnDistanceChanged;
        }

        private void OnDistanceChanged(float distance)
        {
            gameplayCanvasController.SetDistance(distance);
        }
    }
}