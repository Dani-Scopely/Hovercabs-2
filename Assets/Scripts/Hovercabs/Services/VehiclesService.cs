using System;
using System.Collections.Generic;
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
        
        private readonly VehiclesLoader _vehiclesLoader;
        
        public OnVehiclesLoaded OnVehiclesLoaded { get; set; }
        
        public VehiclesService()
        {
            _vehicles = new List<Vehicle>();
            _vehiclesLoader = new VehiclesLoader(OnVehicleLoaded);
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
            return _vehicles[index];
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