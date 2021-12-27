﻿using Hovercabs.Models;
using UnityEngine;

namespace Hovercabs.Controllers
{
    public class ShowcaseController : MonoBehaviour
    {
        private GameObject _currentVehicle;
        private VehicleShowcaseController _vehicleShowcaseController;

        [SerializeField] private VehicleEmblemController vehicleEmblemController;

        public void SetVehicle(Vehicle vehicle)
        {
            if(_currentVehicle!=null) Destroy(_currentVehicle);
            
            _currentVehicle = Instantiate(vehicle.Model, transform);
            _currentVehicle.transform.localPosition = Vector3.zero;
            _vehicleShowcaseController = _currentVehicle.GetComponent<VehicleShowcaseController>();
            _vehicleShowcaseController.rotationSpeed = 50f;

            vehicleEmblemController.SetEmblem(vehicle.Emblem);
            
        }
    }
}