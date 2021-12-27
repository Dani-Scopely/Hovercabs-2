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
        
        private readonly Dictionary<string, GameObject> _vehicles;
        private readonly VehiclesLoader _vehiclesLoader;
        
        public OnVehiclesLoaded OnVehiclesLoaded { get; set; }
        
        public VehiclesService()
        {
            _vehicles = new Dictionary<string, GameObject>();
            _vehiclesLoader = new VehiclesLoader(OnVehicleLoaded);
        }

        public void Init()
        {
            _vehiclesLoader.Load();    
        }

        public GameObject GetVehicle(Vehicle vehicle)
        {
            var id = vehicle.Id;

            var result = _vehicles.TryGetValue(id, out var v);

            if (result)
            {
                return _vehicles[id];
            }

            throw new Exception($"Vehicle {vehicle.Id} cannot be loaded.");
        }

        private void AddVehicle(string id, GameObject vehicle)
        {
            EventBus.GetBus().Send(new OnResourceLoaded { ResourceName = vehicle.name });
            
            _vehicles.Add(id, vehicle);
            
            VehiclesCount++;
            
            if (!_vehiclesLoader.IsDone) return;
            
            OnVehiclesLoaded?.Invoke();
        }
        
        private void OnVehicleLoaded(GameObject vehicle)
        {
            AddVehicle(vehicle.name, vehicle);
        }
    }
}