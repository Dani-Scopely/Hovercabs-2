using System;
using Hovercabs.Configurations.Gameplay.Vehicles;
using Hovercabs.Configurations.Vehicles;
using Hovercabs.Views;
using UnityEngine;

namespace Hovercabs.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(VehicleView))]
    public class VehicleController : MonoBehaviour
    {
        [SerializeField] private float speed = 1.0f;
        private VehicleConfig _vehicleConfig;
        private Rigidbody _rigidbody;
        private VehicleView _view;

        [SerializeField] private float currentSpeed = 0f;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _view = GetComponent<VehicleView>();
        }

        private void Start()
        {
            _view.Render();
        }

        public void Init(VehicleGameplayConfig config, VehicleConfig vehicleConfig)
        {
            _vehicleConfig = vehicleConfig;
            SetupModel(config);
        }

        private void SetupModel(VehicleGameplayConfig config)
        {
            var t = transform;
            
            t.position = config.initialPosition;
            t.localScale = config.initialScale;
        }
        
        private void FixedUpdate()
        {
            currentSpeed = _rigidbody.velocity.z;
            
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
    }
}