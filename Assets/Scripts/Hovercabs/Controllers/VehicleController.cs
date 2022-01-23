using System;
using DG.Tweening;
using Hovercabs.Configurations.Gameplay.Vehicles;
using Hovercabs.Configurations.Vehicles;
using Hovercabs.Models;
using Hovercabs.Views;
using UnityEngine;
using UnityEngine.Serialization;

namespace Hovercabs.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(VehicleView))]
    [RequireComponent(typeof(VehicleMovementComponent))]
    public class VehicleController : MonoBehaviour
    {
        private VehicleConfig _vehicleConfig;
        private VehicleGameplayConfig _vehicleGameplayConfig;
        private VehicleView _view;
        private VehicleMovementComponent _movementComponent;

        private PassengerController _passenger;
        
        public Action<float> OnDistanceChanged { get; set; }
        public Action<float> OnFuelChanged { get; set; }
        public Action<int> OnXenitsChanged { get; set; }
        public Action OnOutOfFuel { get; set; }
        
        public Action<Passenger,bool> OnPassengerDelivered { get; set; }

        private Passenger _currentPassenger;
       
        
        private void Awake()
        {
            _view = GetComponent<VehicleView>();
            _movementComponent = GetComponent<VehicleMovementComponent>();
        }

        private void Start()
        {
            _view.Render();
        }

        public void Init(VehicleGameplayConfig vehicleGameplayConfig, VehicleConfig vehicleConfig)
        {
            _vehicleGameplayConfig = vehicleGameplayConfig;
            _vehicleConfig = vehicleConfig;
            
            InitMovementController();
        }

        public void DropOnPassenger(Passenger passenger)
        {
            // Oh! You forgot to drop off your currentPassenger! We need to force the dropOff
            if (_currentPassenger != null)
            {
                DropOffPassenger(true);
                return;
            }
            
            _currentPassenger = passenger;
            
            var ob = Instantiate(Resources.Load<GameObject>("Hovercabs/3D/Passengers/Prefabs/PassengerPortrait"),
                transform, false);
            
            _passenger = ob.GetComponent<PassengerController>();
            _passenger.Init(passenger);
        }

        public void DropOffPassenger(bool isForced = false)
        {
            if (_passenger == null) return;
            
            var collectedXenits = _passenger.Collect();

            collectedXenits = isForced ? collectedXenits * -1 : collectedXenits;
            
            OnXenitsChanged?.Invoke(collectedXenits);
            
            OnPassengerDelivered?.Invoke(_currentPassenger, !isForced);
            
            Destroy(_passenger.gameObject);

            _currentPassenger = null;
            _passenger = null;
        }
        
        private void InitMovementController()
        {
            _movementComponent.Init(_vehicleGameplayConfig, _vehicleConfig, OnDistanceChanged, OnFuelChanged, OnOutOfFuel);
        }
    }
}