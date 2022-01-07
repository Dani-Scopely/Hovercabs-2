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
        
        public Action<float> OnDistanceChanged { get; set; }
        
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
            
            SetupModel();
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
            
            NotifyUI();
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
                transform.position += new Vector3(1, 0, 0);
            }else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }

        private void NotifyUI()
        {
            OnDistanceChanged?.Invoke(distance);
        }
    }
}