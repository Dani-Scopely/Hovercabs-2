using System;
using System.Collections.Generic;
using Hovercabs.Configurations.Vehicles;
using Hovercabs.Events;
using Hovercabs.Loaders;
using Hovercabs.Models;
using UnityEngine;
using Utils;

namespace Hovercabs.Services
{
    public delegate void OnVehiclesLoaded();
    
    public class VehiclesService
    {
        public int VehiclesCount { get; private set; }

        private readonly List<Vehicle> _vehicles;
        private int _currentVehicleIndex = 0;
        
        private readonly VehiclesLoader _vehiclesLoader;

        public OnVehiclesLoaded OnVehiclesLoaded { get; set; }
        
        public VehiclesService(VehiclesConfig vehiclesConfig)
        {
            _vehicles = new List<Vehicle>();
            _vehiclesLoader = new VehiclesLoader(vehiclesConfig, OnVehicleLoaded);
        }

        public void Init()
        {
            _vehiclesLoader.Load();    
        }

        public Vehicle GetVehicle(Vehicle vehicle)
        {
            return _vehicles.Find(p => p.Id == vehicle.Id);
        }

        public Vehicle GetVehicleByIndex(int index)
        {
            _currentVehicleIndex = index;

            var vehicle = _vehicles[_currentVehicleIndex];
            
            SetCurrentVehicle(vehicle);
            
            return _vehicles[_currentVehicleIndex];
        }

        private void SetCurrentVehicle(Vehicle vehicle)
        {
            PlayerPrefs.SetInt("currentVehicleIndex",_currentVehicleIndex);
            PlayerPrefs.Save();
        }

        public Vehicle GetCurrentVehicle()
        {
            _currentVehicleIndex = GetCurrentVehicleIndex();
            
            return _vehicles[_currentVehicleIndex];
        }

        public int GetCurrentVehicleIndex()
        {
            return PlayerPrefs.GetInt("currentVehicleIndex", 0);
        }
        
        private void AddVehicle(Vehicle vehicle)
        {
            EventBus.GetBus().Send(new OnResourceLoaded { ResourceName = vehicle.Id });
            
            _vehicles.Add(vehicle);

            VehiclesCount++;
            
            if (!_vehiclesLoader.IsDone) return;
            
            OnVehiclesLoaded?.Invoke();
        }
        
        private void OnVehicleLoaded(Vehicle vehicle)
        {
            AddVehicle(vehicle);
        }
    }
}