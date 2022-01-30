using System;
using System.Diagnostics;
using DG.Tweening;
using Hovercabs.Configurations.Gameplay.Vehicles;
using Hovercabs.Configurations.Vehicles;
using Hovercabs.Controllers;
using Hovercabs.Models;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Hovercabs.Components
{
    [RequireComponent(typeof(SwipeController))]
    public class VehicleMovementComponent : MonoBehaviour
    {
        private float _distance;
        private float _currentSpeed;
        private float _currentFuel;
        private bool _isTurningRight;
        private bool _isTurningLeft;
        private bool _canTurnLeft = true;
        private bool _canTurnRight = true;
        private Sequence _sequence;
        private Rigidbody _rigidbody;
        private VehicleConfig _vehicleConfig;
        private VehicleGameplayConfig _vehicleGameplayConfig;
        private SwipeController _swipeController;
        private TrackController _trackController;
        private Action<float> _onDistanceChanged;
        private Action<float> _onFuelChanged;
        private Action _onOutOfFuel;
        private bool _isUnlocked;

        private int _layerMask = 0;

        [SerializeField] private string currentTrackId;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _swipeController = GetComponent<SwipeController>();
            _layerMask = LayerMask.GetMask("Road");
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

        public void Unlock()
        {
            _isUnlocked = true;
        }

        public void SetTrackController(TrackController trackController)
        {
            _trackController = trackController;
        }

        private bool _isInRoad;
        
        private void CheckRoad()
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out var hit, 20, _layerMask))
            {
                _isInRoad = true;
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down)*hit.distance, Color.yellow);
                //Debug.Log($"I'm in {_trackController.name}");
            }
            else
            {
                _isInRoad = false;
            }

            currentTrackId = _isInRoad ? _trackController.name : "null";
        }
        
        private void FixedUpdate()
        {
            if (!_isUnlocked) return;
            
            CheckRoad();
            
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
            if (!_isUnlocked) return;
            
            ProcessMovementEditor();
            ProcessMovementMobile();
            
            if(_currentSpeed < _vehicleConfig.maxSpeed) Accelerate();
        }

        [Conditional("UNITY_EDITOR")]
        private void ProcessMovementEditor()
        {
            if(Input.GetKey(KeyCode.LeftArrow))  TurnLeft();
            if(Input.GetKey(KeyCode.RightArrow)) TurnRight();
            if (Input.GetKey(KeyCode.DownArrow)) Brake();
        }

        [Conditional("UNITY_ANDROID")]
        private void ProcessMovementMobile()
        {
            if (_swipeController.SwipeLeft)  TurnLeft();
            if (_swipeController.SwipeRight) TurnRight();
            if (_swipeController.SwipeDown)  Brake();
        }
        
        private void Accelerate()
        {
            if (_currentSpeed > _vehicleConfig.maxSpeed) return;

            _rigidbody.AddForce(new Vector3(0,0,_vehicleConfig.maxAcceleration*2f));
        }

        private void Brake()
        {
            if (_currentSpeed <= 0)
            {
                _rigidbody.AddForce(Vector3.zero);
                return;
            }
                
            _rigidbody.AddForce(new Vector3(0,0,-_vehicleConfig.breakStrength));
        }
        
        private void TurnRight()
        {
            if (_isTurningRight || _isTurningLeft || !_canTurnRight ) return;

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
            if (_isTurningLeft || _isTurningLeft || !_canTurnLeft) return;

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