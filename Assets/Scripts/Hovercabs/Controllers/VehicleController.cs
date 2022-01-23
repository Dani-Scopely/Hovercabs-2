using System;
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
    public class VehicleController : MonoBehaviour
    {
        [SerializeField] private float speed = 1.0f;
        private VehicleConfig _vehicleConfig;
        private VehicleGameplayConfig _vehicleGameplayConfig;
        private Rigidbody _rigidbody;
        private VehicleView _view;

        [SerializeField] private float currentSpeed = 0f;
        [SerializeField] private float distance = 0f;

        private PassengerController _passenger;
        
        public Action<float> OnDistanceChanged { get; set; }
        public Action<float> OnFuelChanged { get; set; }
        public Action<int> OnXenitsChanged { get; set; }
        public Action OnOutOfFuel { get; set; }

        private Passenger _currentPassenger;
        private float _currentFuel;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _view = GetComponent<VehicleView>();
        }

        private void Start()
        {
            _view.Render();
        }

        public void Init(VehicleGameplayConfig vehicleGameplayConfig, VehicleConfig vehicleConfig)
        {
            _vehicleGameplayConfig = vehicleGameplayConfig;
            _vehicleConfig = vehicleConfig;
            _currentFuel = 100f;
            
            SetupModel();
        }

        public void DropOnPassenger(Passenger passenger)
        {
            _currentPassenger = passenger;
            
            var ob = Instantiate(Resources.Load<GameObject>("Hovercabs/3D/Passengers/Prefabs/PassengerPortrait"),
                transform, false);
            
            _passenger = ob.GetComponent<PassengerController>();
            _passenger.Init(passenger);
        }

        public void DropOffPassenger()
        {
            var collectedXenits = _passenger.Collect();
            
            OnXenitsChanged?.Invoke(collectedXenits);
            
            Destroy(_passenger.gameObject);
        }
        
        private void SetupModel()
        {
            var t = transform;
            
            t.position = _vehicleGameplayConfig.initialPosition;
            t.localScale = _vehicleGameplayConfig.initialScale;
        }
        
        private void FixedUpdate()
        {
            currentSpeed = _rigidbody.velocity.z;
            
            distance = Math.Abs(Vector3.Distance(_vehicleGameplayConfig.initialPosition, transform.position));
            
            _currentFuel -= 1f * (currentSpeed/_vehicleConfig.fuelConsumption);

            OnDistanceChanged?.Invoke(distance);
            OnFuelChanged?.Invoke(Math.Max(_currentFuel,0));
            
            if (_currentFuel < 0)
            {
                OnOutOfFuel?.Invoke();
            }
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (currentSpeed > _vehicleConfig.maxSpeed) return;

                _rigidbody.AddForce(new Vector3(0,0,_vehicleConfig.maxAcceleration));
            }else if (Input.GetKey(KeyCode.DownArrow))
            {
                if (currentSpeed <= 0)
                {
                    _rigidbody.AddForce(Vector3.zero);
                    return;
                }
                
                _rigidbody.AddForce(new Vector3(0,0,-_vehicleConfig.breakStrength));

            }else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                transform.position += new Vector3(4, 0, 0);
            }else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-4, 0, 0);
            }
        }
    }
}