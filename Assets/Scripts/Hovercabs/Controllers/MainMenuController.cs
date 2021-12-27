using System;
using Hovercabs.Services;
using UnityEngine;

namespace Hovercabs.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        private VehiclesService _vehiclesService;
        private int _currentVehicleIndex = 0;
        private int _maxVehicles = 0;
        
        public void Init(VehiclesService vehiclesService)
        {
            _vehiclesService = vehiclesService;
            _maxVehicles = _vehiclesService.VehiclesCount;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _currentVehicleIndex++;
                _currentVehicleIndex %= _maxVehicles;
            
            }
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _currentVehicleIndex--;
                
                if (_currentVehicleIndex < 0) _currentVehicleIndex = _maxVehicles - 1;
            }
            
        }
    }
}
