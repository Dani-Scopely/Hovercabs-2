using System;
using Hovercabs.Configurations.Gameplay.Vehicles;
using Hovercabs.Configurations.Vehicles;
using Hovercabs.Managers;
using Hovercabs.Models;
using Hovercabs.Services;
using UnityEngine;

namespace Hovercabs.Controllers
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private CameraController cameraController;
        
        private TrackManager _trackManager;
        private VehiclesService _vehiclesService;
        private ProfileService _profileService;
        private Vehicle _currentVehicle;
        private VehicleController _vehicleController;

        private VehicleGameplayConfig _config;
        private VehiclesConfig _vehiclesConfig;

        public Action<float> OnDistanceChanged { get; set; }
        public Action<float> OnFuelChanged {get; set; }
        public Action<int> OnXenitsChanged { get; set; }
        public Action OnOutOfFuel { get; set; }
        public Action<Passenger,bool> OnPassengerDelivered { get; set; }
        
        public void Init(TrackManager trackManager, VehiclesService vehiclesService, ProfileService profileService, VehicleGameplayConfig config, VehiclesConfig vehiclesConfig)
        {
            _trackManager = trackManager;
            _vehiclesService = vehiclesService;
            _profileService = profileService;
            _config = config;
            _vehiclesConfig = vehiclesConfig;
            
            _currentVehicle = _vehiclesService.GetCurrentVehicle();
         
            SetupVehicle();
            SetupCamera();
            
            _trackManager.Init(new Track("tr1k1",true));
        }

        private void SetupVehicle()
        {
            var v = Instantiate(_currentVehicle.ModelLow, transform, true);
            _vehicleController = v.GetComponent<VehicleController>();
            _vehicleController.OnDistanceChanged += OnDistanceChanged;
            _vehicleController.OnFuelChanged += OnFuelChanged;
            _vehicleController.OnXenitsChanged += OnXenitsChanged;
            _vehicleController.OnOutOfFuel += OnOutOfFuel;
            _vehicleController.OnPassengerDelivered += OnPassengerDelivered;
            _vehicleController.Init(_config, _vehiclesConfig.GetVehicleConfig(_currentVehicle.Id));
            _trackManager.SetVehicleController(_vehicleController);
        }

        private void OnDestroy()
        {
            _vehicleController.OnDistanceChanged -= OnDistanceChanged;
            _vehicleController.OnFuelChanged -= OnFuelChanged;
            _vehicleController.OnXenitsChanged -= OnXenitsChanged;
            _vehicleController.OnOutOfFuel -= OnOutOfFuel;
            _vehicleController.OnPassengerDelivered -= OnPassengerDelivered;
        }

        private void SetupCamera()
        {
            cameraController.SetTarget(_vehicleController.transform);
        }
    }
}
