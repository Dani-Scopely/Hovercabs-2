using System;
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
        private VehiclesService _vehiclesService;
        
        public void Init(TrackManager trackManager, VehiclesService vehiclesService, Action onQuit)
        {
            _vehiclesService = vehiclesService;
            
            gameplayCanvasController.Init(onQuit);
            
            trackManager.Init();
            
            Debug.Log($"We want to play with: {_vehiclesService.GetCurrentVehicle().Id}");
        }
    }
}