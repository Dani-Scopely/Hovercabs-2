using System;
using System.Collections;
using Hovercabs.Models.DTO;
using Newtonsoft.Json;
using UnityEngine;
using Utils;

namespace Hovercabs.Loaders
{
    public class VehiclesLoader
    {
        private VehiclesDto _vehiclesDto;
        private readonly Action<GameObject> _onVehicleLoaded;

        public bool IsDone { get; private set; } = false;

        public VehiclesLoader(Action<GameObject> onVehicleLoaded)
        {
            _onVehicleLoaded = onVehicleLoaded;
        }

        public void Load()
        {
            var raw = Resources.Load<TextAsset>("vehicles").text;
            _vehiclesDto = JsonConvert.DeserializeObject<VehiclesDto>(raw);

            UnityMainThreadDispatcher.Instance().Enqueue(LoadVehicles());
        }

        private IEnumerator LoadVehicles()
        {
            for (var i = 0; i < _vehiclesDto.vehicles.Count; i++)
            {
                var path = $"Vehicles/{_vehiclesDto.vehicles[i]}/{_vehiclesDto.vehicles[i]}_high";
                var obj = Resources.Load<GameObject>(path);
                
                if (i % 1 == 0) { yield return null; }
                
                IsDone = i == _vehiclesDto.vehicles.Count - 1;
                
                _onVehicleLoaded?.Invoke(obj);
            }
            
            yield return null;
        }
    }
}