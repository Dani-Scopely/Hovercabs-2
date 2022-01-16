using System.Collections.Generic;
using UnityEngine;

namespace Hovercabs.Configurations.Vehicles
{
    [CreateAssetMenu(fileName = "VehiclesConfig", menuName = "Hovercabs/Vehicles/Configuration", order = 1)]
    public class VehiclesConfig : ScriptableObject
    {
        public string vehiclesPrefabPath = "Prefabs/Vehicles/";
        public List<VehicleConfig> vehiclesConfig = new List<VehicleConfig>();

        public VehicleConfig GetVehicleConfig(string id)
        {
            return vehiclesConfig.Find(p => p.id == id);
        }
    }
}