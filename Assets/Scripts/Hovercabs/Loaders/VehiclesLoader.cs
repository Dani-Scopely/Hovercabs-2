using System;
using System.Collections;
using System.Linq;
using Hovercabs.Configurations.Vehicles;
using Hovercabs.Models;
using Hovercabs.Models.DTO;
using Newtonsoft.Json;
using UnityEngine;
using Utils;

namespace Hovercabs.Loaders
{
    public class VehiclesLoader
    {
        private readonly VehiclesConfig _vehiclesConfig;
        private readonly Action<Vehicle> _onVehicleLoaded;

        public bool IsDone { get; private set; } = false;

        public VehiclesLoader(VehiclesConfig vehiclesConfig, Action<Vehicle> onVehicleLoaded)
        {
            _vehiclesConfig = vehiclesConfig;
            _onVehicleLoaded = onVehicleLoaded;
        }

        public void Load()
        {
            //var raw = Resources.Load<TextAsset>("vehicles").text;
            //_vehiclesDto = JsonConvert.DeserializeObject<VehiclesDto>(raw);

            UnityMainThreadDispatcher.Instance().Enqueue(LoadVehicles());
        }

        private IEnumerator LoadVehicles()
        {
            for (var i = 0; i < _vehiclesConfig.vehiclesConfig.Count; i++)
            {
                var vehicleName = _vehiclesConfig.vehiclesConfig[i].id;
                if (i % 1 == 0) yield return null;

                IsDone = i == _vehiclesConfig.vehiclesConfig.Count - 1;
                
                _onVehicleLoaded?.Invoke(new Vehicle(_vehiclesConfig.vehiclesConfig[i]));
            }

            yield return null;
        }
        
        /*
        private IEnumerator LoadVehicles2()
        {
            var sprites = Resources.LoadAll<Sprite>("Emblems/vehicle_emblems");

            for (var i = 0; i < _vehiclesDto.vehicles.Count; i++)
            {
                var vehicleData = _vehiclesDto.vehicles[i].Split(':');
                var vehicleName = vehicleData[0];
                var vehicleLevel = vehicleData[1];
                var path = $"Vehicles/{vehicleName}/{vehicleName}_high";
                var obj = Resources.Load<GameObject>(path);
                
                if (i % 1 == 0) { yield return null; }
                
                IsDone = i == _vehiclesDto.vehicles.Count - 1;

                var sprite = GetVehicleEmblem(sprites, vehicleName);
                
                _onVehicleLoaded?.Invoke(new Vehicle(vehicleName, Int32.Parse(vehicleLevel), obj, sprite));
            }
            
            yield return null;
        }

        private Sprite GetVehicleEmblem(Sprite[] sprites, string id)
        {
            foreach (var t in sprites)
            {
                if (t.name == id) return t;
            }

            throw new Exception("Emblem cannot be loaded.");
        }
        */
    }
}