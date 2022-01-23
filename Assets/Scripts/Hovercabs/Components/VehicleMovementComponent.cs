using System;
using DG.Tweening;
using Hovercabs.Configurations.Gameplay.Vehicles;
using Hovercabs.Configurations.Vehicles;
using UnityEngine;

namespace Hovercabs.Controllers
{
    public class VehicleMovementComponent : MonoBehaviour
    {
        private float _distance;
        private float _currentSpeed;
        private float _currentFuel;
        private bool _isTurningRight;
        private bool _isTurningLeft;
        private Sequence _sequence;
        private Rigidbody _rigidbody;
        private VehicleConfig _vehicleConfig;
        private VehicleGameplayConfig _vehicleGameplayConfig;
        private Action<float> _onDistanceChanged;
        private Action<float> _onFuelChanged;
        private Action _onOutOfFuel;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Init(VehicleGameplayConfig vehicleGameplayConfig, VehicleConfig vehicleConfig, 
            Action<float> onDistanceChanged, 
            Action<float> onFuelChanged,
            Action onOutOfFuel)
        {
            _onDistanceChanged = onDistanceChanged;
            _onFuelChanged = onFuelChanged;
            _onOutOfFuel = onOutOfFuel;
            
            _vehicleGameplayConfig = vehicleGameplayConfig;
            _vehicleConfig = vehicleConfig;
            
            var t = transform;
            
            t.position = _vehicleGameplayConfig.initialPosition;
            t.localScale = _vehicleGameplayConfig.initialScale;
            
            _currentFuel = 100f;
        }

        private void FixedUpdate()
        {
            _distance = Math.Abs(Vector3.Distance(_vehicleGameplayConfig.initialPosition, transform.position));
            
            _currentSpeed = _rigidbody.velocity.z;
            
            _currentFuel -= 1f * (_currentSpeed/_vehicleConfig.fuelConsumption);

            _onDistanceChanged?.Invoke(_distance);
            _onFuelChanged?.Invoke(Math.Max(_currentFuel,0));
            
            if (_currentFuel < 0)
            {
                _onOutOfFuel?.Invoke();
            }
        }
        
        private void Update()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (_currentSpeed > _vehicleConfig.maxSpeed) return;

                _rigidbody.AddForce(new Vector3(0,0,_vehicleConfig.maxAcceleration));
                
            }else if (Input.GetKey(KeyCode.DownArrow))
            {
                if (_currentSpeed <= 0)
                {
                    _rigidbody.AddForce(Vector3.zero);
                    return;
                }
                
                _rigidbody.AddForce(new Vector3(0,0,-_vehicleConfig.breakStrength));

            }else if (Input.GetKey(KeyCode.RightArrow))
            {
                TurnRight();
            }else if (Input.GetKey(KeyCode.LeftArrow))
            {
                TurnLeft();
            }
        }
        
        private void TurnRight()
        {
            if (_isTurningRight || _isTurningLeft ) return;

            var currentPosition = transform.position;
            var newPosition = currentPosition + new Vector3(4f, 0, 0);
            
            _sequence = DOTween.Sequence();
            _sequence.OnStart(() => { _isTurningRight = true; });
            _sequence.Append(transform.DORotate(new Vector3(0, 0, -45), 0.1f));
            _sequence.Append(transform.DOMoveX(newPosition.x, _vehicleConfig.timeToTurn));
            _sequence.Append(transform.DORotate(new Vector3(0, 0, 0), 0.1f));
            _sequence.OnComplete(() => { _isTurningRight = false; });
        }
        
        private void TurnLeft()
        {
            if (_isTurningLeft || _isTurningLeft) return;

            var currentPosition = transform.position;
            var newPosition = currentPosition + new Vector3(-4f, 0, 0);
            
            _sequence = DOTween.Sequence();
            _sequence.OnStart(() => { _isTurningLeft = true; });
            _sequence.Append(transform.DORotate(new Vector3(0, 0, 45), 0.1f));
            _sequence.Append(transform.DOMoveX(newPosition.x, _vehicleConfig.timeToTurn));
            _sequence.Append(transform.DORotate(new Vector3(0, 0, 0), 0.1f));
            _sequence.OnComplete(() => { _isTurningLeft = false; });
        }
    }
}