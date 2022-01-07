using System;
using Hovercabs.Configurations.Gameplay.Vehicles;
using Hovercabs.Events;
using Hovercabs.FSM;
using Hovercabs.FSM.States;
using Hovercabs.Managers;
using Hovercabs.Services;
using Hovercabs.UI;
using UnityEngine;
using Utils;

namespace Hovercabs.Controllers
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private GameplayCanvasController gameplayCanvasController;
        [SerializeField] private LevelController levelController;
        [Header("Configurations")]
        [SerializeField] private VehicleGameplayConfig vehicleGameplayConfig;
        
        public void Init(TrackManager trackManager, VehiclesService vehiclesService, Action onQuit)
        {
            gameplayCanvasController.Init(onQuit);
            
            InitLevel(trackManager, vehiclesService);
        }

        private void InitLevel(TrackManager trackManager, VehiclesService vehiclesService)
        {
            levelController.Init(trackManager, vehiclesService, vehicleGameplayConfig);
        }
    }
}